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

		public IVideoReader VideoReader { get; }

		public IVideoReaderThreadManager VideoReaderThreadManager { get; }

		public ILogger Logger { get; }

		public MainForm(IImageDetector imageDetector, IVideoReaderThreadManager videoReaderThreadManager, ILogger logger, IVideoReader videoReader)
		{
			//Сейчас приложение запускается только после инициализации Yolo, правильно ли это или нет, я хз
			// — Предлагаешь мне прелоадер сделать?
			ImageDetector = imageDetector;
			VideoReaderThreadManager = videoReaderThreadManager;
			Logger = logger;
			VideoReader = videoReader;

			InitializeComponent();
		}

		private VideoRecognitionResults _videoRecognitionResults;

		private void btnOpen_Click(object sender, EventArgs e)
		{ //Помню, ты что то против этого имел
			// — А что?)
			using (var ofd = new OpenFileDialog() { Filter = @"Image files(*.png; *.jpg; *.jpeg *.bmp | *.png; *.jpg; *.jpeg *.bmp" })
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					picBx.Image = Image.FromFile(ofd.FileName);
					RecognitionOutput(ImageDetector.Process(picBx.Image), ofd.SafeFileName);
				}
			}
		}

		private void RecognitionOutput(IRecognitionResult result, string imageName)
		{
			//Функция только для изображений
			dataGridViewResult.DataSource = result.Items;
			picBx.Image = DrawBorder2Image(result);
			//todo: Внимательно, появилась такой екстеншен для результа
			result.SaveToXml($@"Results/{imageName}.xml", imageName);
		}



		private Image DrawBorder2Image(IRecognitionResult result)
		{
			//Это тоже вынести в отдельный класс?
			// — Да. Справишься? Справишься, я верю.
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
			//Будет плохо если ещё раз нажмут на кнопку и вылезет ашибочка, наверн
			_videoRecognitionResults = new VideoRecognitionResults();
			var progress = new Progress<VideoRecognitionResult>(OnImageDetected);

			VideoReaderThreadManager.Start("Resources/test.avi", progress);
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

		private void pic_LoadCompleted(object sender, AsyncCompletedEventArgs e)
		{

		}

		private void PlayBtn_Click(object sender, EventArgs e)
		{
			//Я хз какой магией там работет этот поток, и как его останавливать я тоже хз, собственно
			//todo: VideoReaderThreadManager.Stop(); не работает?
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
					var path = new DirectoryInfo(folderDialog.SelectedPath);
					var allFiles = path.GetFiles("*.*");

					var images = allFiles.Where(file => Regex.IsMatch(file.Name, @".jpg|.png|.jpeg$")).ToList(); //todo: не знаю какие форматы надо

					//todo: в images сейчас хранятся все изображения. А дальше вопрос, можно также потоком, а можно задачей (различие в том, что задачи это для коротких операций). Решай, я реализую, или можешь сам рискнуть
					//Далее ведь не составит проблем, да? Пишем что-то(выше), в прогрессе(IProgress<RecognitionResult>) получаем результат и сохраняем
					//Сохранялку в хмл сделал, дешевое решение, позже переделаю, сначала надо её todo: проверить
				} 
			}
		}
	}
}
