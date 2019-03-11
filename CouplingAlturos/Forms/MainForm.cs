using Accord.Video.FFMPEG;
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

namespace CouplingAlturos
{

	public partial class MainForm : Form
	{
		#region Helper

		private class VideoRecognitionResults
		{
			public List<IRecognitionResult> Items = new List<IRecognitionResult>();

			public long Counter = 0L;

			public long LastFrame = 0L;
		}

		#endregion

		public IImageDetector ImageDetector { get; }

		public IVideoDetector VideoDetector { get; }

		public IThreadManager ThreadManager { get; }

		public ILogger Logger { get; }

		public MainForm(IImageDetector imageDetector, IThreadManager threadManager, ILogger logger, IVideoDetector videoDetector)
		{
			//Сейчас приложение запускается только после инициализации Yolo, правильно ли это или нет, я хз
			ImageDetector = imageDetector;
			ThreadManager = threadManager;
			Logger = logger;
			VideoDetector = videoDetector;

			InitializeComponent();
		}

		private VideoRecognitionResults _videoRecognitionResults;

		private void btnOpen_Click(object sender, EventArgs e)
		{ //Помню, ты что то против этого имел

			using (OpenFileDialog ofd = new OpenFileDialog() { Filter = @"Image files(*.png; *.jpg; *.jpeg *.bmp | *.png; *.jpg; *.jpeg *.bmp" })
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
			LogToXml(result);
		}



		private Image DrawBorder2Image(IRecognitionResult result)
		{
			//Это тоже вынести в отдельный класс?
			var image = result.ImageBytes.ToImage();
			using (var canvas = Graphics.FromImage(image))
			{
				foreach (var item in result.Items)
				{
					//overlayBrush Где используется?
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
			// — Пока нет
		}

		private void BtnOpenVideo_Click(object sender, EventArgs e)
		{
			//todo: открытие видео из файла и вывод пути к нему в текстбок
			// — Это мне делать?
			//todo: Сохранение лога после окончания видео или же при остановке видео
			// — Или автосейв?

			_videoRecognitionResults = new VideoRecognitionResults();
			var progress = new Progress<VideoRecognitionResult>(OnImageDetected);

			ThreadManager.Start(() => VideoDetector.Process("Resources/test.avi", progress));
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
					pic.Image = DrawBorder2Image(result);
					if (result.IndexFrame - _videoRecognitionResults.LastFrame > 13)
					{
						_videoRecognitionResults.Counter++;
						CouplingCounterLabel.Text = _videoRecognitionResults.Counter.ToString();
						foreach (var item in result.Items)
						{
							//Делаю +- функционал, который потом нужно красиво оформить
							//Выведи в отдельную функцию эту хрень, юзай StringBuilder
							var msg = "Номер кадра: " + result.IndexFrame.ToString() + ".\nКоординаты центра X: " + item.X + "; Y:  " + item.Y + ".\nШирина: " + item.Width + " Высота: " + item.Height;
							Logger.WriteLine(msg);
							LogTxtBx.AppendText(Logger.Messages.Last().Message);
						}
					}
					_videoRecognitionResults.LastFrame = result.IndexFrame;

				}

				_videoRecognitionResults.Items.RemoveAt(0);
			}
		}

		private static void LogToXml(IRecognitionResult result)
		{

			foreach (var item in result.Items)
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
			//todo: ThreadManager.Stop(); не работает?
		}

		private void BtnOpenFolder_Click(object sender, EventArgs e)
		{
			//todo: Открыть папку и задетектить все картинки в ней и сохранить все в хмл в соседнюю папку
			// — Есть трудности?
			using(var folderDialog = new FolderBrowserDialog())
			{
				folderDialog.SelectedPath = Application.StartupPath;
				if(folderDialog.ShowDialog(this) == DialogResult.OK)
				{
					var images = new List<FileInfo>();
					var path = new DirectoryInfo(folderDialog.SelectedPath);
					var allFiles = path.GetFiles("*.*"); 

					foreach (var file in allFiles)
					{
						if (Regex.IsMatch(file.Name, @".jpg|.png|.jpeg$"))//todo: не знаю какие форматы надо
							images.Add(file);
					}
					//todo: в images сейчас хранятся все изображения
				} 
			}
		}
	}
}
