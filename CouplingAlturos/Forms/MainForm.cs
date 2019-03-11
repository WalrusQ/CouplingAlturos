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
using System.Threading.Tasks;
using System.Windows.Forms;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core;
using CouplingAlturos.Core.Converters;
using CouplingAlturos.Core.Models;
using CouplingAlturos.Model;

namespace CouplingAlturos
{
    
    public partial class MainForm : Form
    {
		public IImageDetector ImageDetector { get; }

		public IVideoThreadManager VideoThreadManager { get; }

		public MainForm(IImageDetector imageDetector, IVideoThreadManager videoThreadManager)
        {
            //Сейчас приложение запускается только после инициализации Yolo, правильно ли это или нет, я хз
	        ImageDetector = imageDetector;
	        VideoThreadManager = videoThreadManager;
            
            InitializeComponent();
		}

        private void btnOpen_Click(object sender, EventArgs e)
        { //Помню, ты что то против этого имел
            
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = @"Image files(*.png; *.jpg; *.jpeg *.bmp | *.png; *.jpg; *.jpeg *.bmp"  })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picBx.Image = Image.FromFile(ofd.FileName);
                    RecognitionOutput(ImageDetector.Process(picBx.Image));
                }
            }
        }

        private void RecognitionOutput(IRecognitionResult result)
        {
            //Функция только для изображений
            dataGridViewResult.DataSource = result.Items;
            picBx.Image = DrawBorder2Image(result);
            logToXml(result);
        }



        private Image DrawBorder2Image(IRecognitionResult result)
        {
            //Это тоже вынести в отдельный класс?
            var image = result.ImageBytes.ToImage();
            using (var canvas = Graphics.FromImage(image))
            {
                foreach (var item in result.Items)
                {
                    using (var overlayBrush = new SolidBrush(Color.FromArgb(150, 255, 255, 102)))
                    using (var pen = GetBrush(item.Confidence, image.Width))
                    {
                        canvas.DrawRectangle(pen, item.X, item.Y, item.Width, item.Height);
                        canvas.Flush();
                    }
                }
            }
            return image;
        }

        private Pen GetBrush(double confidence, int width)
        {
            //Это тоже вынести в отдельный класс?
            var size = width / 100;

            if (confidence > 0.5)
            {
                return new Pen(Brushes.GreenYellow, size);
            }
            else if (confidence > 0.2 && confidence <= 0.5)
            {
                return new Pen(Brushes.Orange, size);
            }

            return new Pen(Brushes.DarkRed, size);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //todo: Надо ли что то тут делать?
        }

        private void btnOpenVideo_Click(object sender, EventArgs e)
        {
            //todo: открытие видео из файла и вывод пути к нему в текстбокс
            //todo: Сохранение лога после окончания видео или же при остановке видео
            var log = new Logger(); 
			var list = new List<Item>();
            long couplingLastFrame = 0;
            var couplingCounter = 0;
			var progress = new Progress<VideoRecognitionResult>(result =>
			{
                pic.Image = result.ImageBytes.ToImage();
				list.Add(new Item(result));
				if(list.Count > 15)
				{
					var validator = new FramesValidator(list);
					if(validator.IsValid)
					{
                        pic.Image = DrawBorder2Image(result);
						if (result.IndexFrame - couplingLastFrame > 13)
                        {
                            couplingCounter++;
                            CouplingCounterLabel.Text = couplingCounter.ToString();
                            foreach(var item in result.Items)
                            {
                                //Делаю +- функционал, который потом нужно красиво оформить
                                var logmsg = "Номер кадра: " + result.IndexFrame.ToString() + ".\nКоординаты центра X: " + item.X + "; Y:  " + item.Y + ".\nШирина: " + item.Width + " Высота: " + item.Height;
                                log.WriteLine(logmsg);
                                LogTxtBx.AppendText(log.Messages.Last().Message);
                                
                                
                            }
                            

                        }
                        couplingLastFrame = result.IndexFrame;

					}

                    list.RemoveAt(0);

				}
                else
                {
                    pic.Image = result.ImageBytes.ToImage();
                }
				
			});


			VideoThreadManager.Start("Resources/test.avi", progress);
	
   //         var reader = new VideoFileReader();
   //         reader.Open("test.avi"); 
 
   //         for (var i = 0; i < 10; i++)
   //         {
   //             var frame = reader.ReadVideoFrame();
   //             _curPic = frame;
   //             pic.ImageBytes = frame;

   //             Detect(frame);
			//	//frame.Dispose();
			//}

			//reader.Close();
        }

        private void logToXml(IRecognitionResult result)
        {
         
            foreach(var item in result.Items)
            {
                var xc = item.X;
                var yc = item.Y;
                var w = item.Width;
                var h = item.Height;
                //todo: use ImageInfo to output image name (or smth)
                string xml = @"<Object>" + Environment.NewLine +
                    "  <Team Value=\"team6\" />" + Environment.NewLine +
                    "  <ImageName Value=" + "1.jpg" + " />" + Environment.NewLine +
                    "  <Region>" + Environment.NewLine +
                    "    <Main>" + Environment.NewLine +
                    "      <TopLeft X=" + (xc - w / 2) + " Y=" + (yc + h / 2) + " />" + Environment.NewLine +
                    "      <BotRight X=" + (xc + w / 2) + " Y=" + (yc - h / 2) + " />" + Environment.NewLine +
                    "    </Main>" + Environment.NewLine +
                    "    <Alternative>" + Environment.NewLine +
                    "      <Center X=" + xc + " Y=" + yc + "/>" + Environment.NewLine +
                    "      <Width Value=" + w + "/>" + Environment.NewLine +
                    "      <Height Value=" + h + "/>" + Environment.NewLine +
                    "    </Alternative>" + Environment.NewLine +
                    "    <EachPoint>" + Environment.NewLine +
                    "      <Point X=" + (xc - w / 2) + " Y=" + (yc + h / 2) + " />" + Environment.NewLine +
                    "      <!-- top-left -->" + Environment.NewLine +
                    "      <Point X=" + (xc + w / 2) + " Y=" + (yc + h / 2) + " />" + Environment.NewLine +
                    "      <!-- top-right -->" + Environment.NewLine +
                    "      <Point X=" + (xc + w / 2) + " Y=" + (yc - h / 2) + " />" + Environment.NewLine +
                    "      <!-- bottom-right -->" + Environment.NewLine +
                    "      <Point X=" + (xc - w / 2) + " Y=" + (yc - h / 2) + " />" + Environment.NewLine +
                    "      <!-- bottom-left -->" + Environment.NewLine +
                    "    </EachPoint>" + Environment.NewLine +
                    "  </Region>" + Environment.NewLine +
                    "</Object>" + Environment.NewLine;
                File.WriteAllText(@"D:\Desktop\CouplingAlturos-master\Result\" + "1.jpg" + ".xml", xml);
            }
           
        }

        private void pic_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            //Я хз какой магией там работет этот поток, и как его останавливать я тоже хз, собственно
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            //todo: Открыть папку и задетектить все картинки в ней и сохранить все в хмл в соседнюю папку

        }
    }
}
