﻿using Accord.Video.FFMPEG;
using Alturos.Yolo;
using Alturos.Yolo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core;
using CouplingAlturos.Core.Converters;
using CouplingAlturos.Core.Models;
using CouplingAlturos.Model;
using System.Text;
using Newtonsoft.Json;

namespace CouplingAlturos
{

	public partial class MainForm : Form
	{
        //todo: Нужно его в отдельный файлик?
		#region Helper

		private class VideoRecognitionResults
		{
			public List<IRecognitionResult> Items = new List<IRecognitionResult>();

			public long Counter = 0L;

			public long LastFrame = 0L;
		}

        #endregion
        public string VideoFile;

		public IImageDetector ImageDetector { get; }

		public IVideoReader VideoReader { get; }

		public IVideoReaderThreadManager VideoReaderThreadManager { get; }

		public ILogger Logger { get; }

        public MainForm(IImageDetector imageDetector, IVideoReaderThreadManager videoReaderThreadManager, ILogger logger, IVideoReader videoReader)
        {
            //Сейчас приложение запускается только после инициализации Yolo, правильно ли это или нет, я хз
            // — Предлагаешь мне прелоадер сделать?
            //я хз, я не знаю как лучше
            ImageDetector = imageDetector;
            VideoReaderThreadManager = videoReaderThreadManager;
            Logger = logger;
            VideoReader = videoReader;

            InitializeComponent();

            toolStripStatusLabelYoloInfo.Text = $@"Detection system: {ImageDetector.YoloMetaInfo.DetectionSystem}";
        }

        private VideoRecognitionResults _videoRecognitionResults;

        private ImageLogger imageLogger;

		private void btnOpen_Click(object sender, EventArgs e)
		{ 
			using (var ofd = new OpenFileDialog() { Filter = @"Image files(*.png; *.jpg; *.jpeg *.bmp | *.png; *.jpg; *.jpeg *.bmp" })
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					picBx.Image = Image.FromFile(ofd.FileName);
					RecognitionOutput(ImageDetector.Process(picBx.Image), ofd.SafeFileName);
                    OpenPhotoTxtBx.Text = ofd.FileName;
				}
			}
		}

		private void RecognitionOutput(IRecognitionResult result, string imageName)
		{
			dataGridViewResult.DataSource = result.Items;
            picBx.Image = result.DrawBorder2Image();
            imageLogger = new ImageLogger(result, imageName);
            imageLogger.Save($@"Results/{imageName}.json");
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
            //Предположительное решение сохранения лога, или можно сделать автосейв через каждую строку
			if(_videoRecognitionResults != null)
            {
                Logger.Save($@"Results/{DateTime.Now.ToString("dd/MM/yy HH-mm-ss")}.txt");
            }
		}

        private void BtnOpenVideo_Click(object sender, EventArgs e)
        {

            using (var ofd = new OpenFileDialog() { Filter = @"Video File(*.avi | *.avi" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    OpenVideoTxtBx.Text = ofd.FileName;
                    VideoFile = ofd.FileName;
                    pic.BackColor = Color.Black;
                    PlayBtn.Enabled = true; //Можно лочить кнопки в определенных условиях, можно просто обрабатывать множественные нажатия, как лучше сделать?
                }
                //todo: Сохранение лога после окончания видео или же при остановке видео
                // — Или автосейв?
                //Будет плохо если ещё раз нажмут на кнопку и вылезет ашибочка, наверн
               
            }
        }

		private void OnImageDetected(VideoRecognitionResult result)
		{
			pic.Image = result.ImageBytes.ToImage();
			_videoRecognitionResults.Items.Add(result);
			if (_videoRecognitionResults.Items.Count > 15)
			{
				var validator = new FramesValidator(_videoRecognitionResults.Items);
				if (validator.IsValid)
				{
                    pic.Image = result.DrawBorder2Image();
					if (result.IndexFrame - _videoRecognitionResults.LastFrame > 13)
					{
						_videoRecognitionResults.Counter++;
						CouplingCounterLabel.Text = _videoRecognitionResults.Counter.ToString();
						foreach (var item in result.Items)
						{
                            LogMsg(item, result);
						}
					}
					_videoRecognitionResults.LastFrame = result.IndexFrame;

				}
				_videoRecognitionResults.Items.RemoveAt(0);
			}
		}
        
        private void LogMsg(YoloItem item, VideoRecognitionResult result)
        {
            var msg = new StringBuilder();
            msg.AppendLine("Номер кадра: " + result.IndexFrame.ToString());
            msg.AppendLine("Координата центра X: " + item.X);
            msg.AppendLine("Координата центра Y: " + item.Y);
            msg.AppendLine("Ширина: " + item.Width);
            msg.AppendLine("Высота: " + item.Height);
            Logger.WriteLine(msg.ToString());
            LogTxtBx.AppendText(Logger.Messages.Last().ToString());
            
        }

		private void PlayBtn_Click(object sender, EventArgs e)
		{
            _videoRecognitionResults = new VideoRecognitionResults();
            var progress = new Progress<VideoRecognitionResult>(OnImageDetected);
            PlayBtn.Enabled = false;
            VideoReaderThreadManager.Start(VideoFile, progress);
        }

		private void BtnOpenFolder_Click(object sender, EventArgs e)
		{
			using(var folderDialog = new FolderBrowserDialog())
			{
				folderDialog.SelectedPath = Application.StartupPath;
				if(folderDialog.ShowDialog(this) == DialogResult.OK)
				{
                    
					var path = new DirectoryInfo(folderDialog.SelectedPath);
                    OpenFolderPhotoTxtBx.Text = path.ToString();
                    var allFiles = path.GetFiles("*.*");

					var images = allFiles.Where(file => Regex.IsMatch(file.Name, @".jpg|.png|.jpeg|.bmp$")).ToList(); 

                    //todo: Думаю, что лучше потоком. Сделай, я не до конца понимаю.
					//todo: в images сейчас хранятся все изображения. А дальше вопрос, можно также потоком, а можно задачей (различие в том, что задачи это для коротких операций). Решай, я реализую, или можешь сам рискнуть
					
				} 
			}
		}

        private void BtnStopVideo_Click(object sender, EventArgs e)
        {
            VideoReaderThreadManager.Stop();
            Logger.Save($@"Results/{DateTime.Now.ToString("dd/MM/yy HH-mm-ss")}.txt");
            Logger.Clear();
            PlayBtn.Enabled = true;
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            //todo: Все таки нужно сделать паузу
        }
    }
}
