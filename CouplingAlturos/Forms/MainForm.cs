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
using System.Threading.Tasks;
using System.Windows.Forms;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core;

namespace CouplingAlturos
{
    
    public partial class MainForm : Form
    {
        private YoloWrapper _yoloWrapper;
        private Image _curPic; // Не уверен в необходимости этой переменной

		public IImageDetector ImageDetector { get; }

		public IVideoDetector VideoDetector { get; }

		public MainForm(IImageDetector imageDetector, IVideoDetector videoDetector)
        {
	        ImageDetector = imageDetector;
	        VideoDetector = videoDetector;

	        // Хозяйничать можно?
            InitializeComponent();
            Task.Run(() => Initialize("Resources/")); //Зачем поток и в конструкторе
		}

        private void Initialize(string path)
        {
            var configurationDetector = new ConfigurationDetector();
            var config = configurationDetector.Detect(path); // ф-ия возвращает только тех, что заканчиваются на ".weights" такого файла не было

            if (config == null)
            {
                return;
            }

            Initialize(config);
        }

        private void Initialize(YoloConfiguration config)
        {
            try
            {
	            _yoloWrapper?.Dispose();

                var sw = new Stopwatch();
                sw.Start();
                _yoloWrapper = new YoloWrapper(config.ConfigFile, config.WeightsFile, config.NamesFile, 0, true);
                sw.Stop();

	            // Всё что ниже перепишу
                var action = new MethodInvoker(delegate
                {
                    var detectionSystemDetail = string.Empty;
                    if (!string.IsNullOrEmpty(_yoloWrapper.EnvironmentReport.GraphicDeviceName))
                    {
                        detectionSystemDetail = $"({_yoloWrapper.EnvironmentReport.GraphicDeviceName})";
                    }
                   
                    toolStripStatusLabelYoloInfo.Text = $@"Initialize Yolo in {sw.Elapsed.TotalMilliseconds:0} ms - Detection System:{_yoloWrapper.DetectionSystem} {detectionSystemDetail} Weights:{config.WeightsFile}";
                });

                statusStrip1.Invoke(action);
                btnDetect.Invoke(new MethodInvoker(delegate { btnDetect.Enabled = true; }));
                btnOpenVideo.Invoke(new MethodInvoker(delegate { btnOpenVideo.Enabled = true; }));
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"{nameof(Initialize)} - {exception}", @"Error Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = @"Image files(*.png; *.jpg; *.jpeg| *.png; *.jpg; *.jpeg"  })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _curPic = Image.FromFile(ofd.FileName);
                    pic.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        private void Detect(Image img)
        {
            if (_yoloWrapper == null)
            {
                return;
            }
            
            var imageData = ImageToByteArray(img);

            var sw = new Stopwatch();
            sw.Start();
            var items = _yoloWrapper.Detect(imageData).ToList(); 
            sw.Stop();
            groupBoxResult.Text = $@"Result [ processed in {sw.Elapsed.TotalMilliseconds:0} ms ]";

            dataGridViewResult.DataSource = items;
            DrawBorder2Image(items);
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            Detect(pic.Image);
        }

        private void DrawBorder2Image(IEnumerable<YoloItem> items, YoloItem selectedItem = null)
        {
            var image = _curPic;
            // string path = @"E:\\Khakaton_Mallenom\\team6\\1_101.jpg";
          //  var image = Image.FromFile(path);
            using (var canvas = Graphics.FromImage(image))
            {
                // Modify the image using g here... 
                // Create a brush with an alpha value and use the g.FillRectangle function
                foreach (var item in items)
                {
                    var x = item.X;
                    var y = item.Y;
                    var width = item.Width;
                    var height = item.Height;

                    using (var overlayBrush = new SolidBrush(Color.FromArgb(150, 255, 255, 102)))
                    using (var pen = GetBrush(item.Confidence, image.Width))
                    {
                        if (item.Equals(selectedItem))
                        {
                            canvas.FillRectangle(overlayBrush, x, y, width, height);
                        }
                        canvas.DrawRectangle(pen, x, y, width, height);
                        canvas.Flush();
                    }
                }
            }

            var oldImage = pic.Image; // unused variable
            pic.Image = image;
            //oldImage?.Dispose();
        }

        private Pen GetBrush(double confidence, int width)
        {
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
            _yoloWrapper?.Dispose();
        }

        private void btnOpenVideo_Click(object sender, EventArgs e)
        {
            var reader = new VideoFileReader();
            reader.Open("test.avi"); 
 
            for (var i = 0; i < 10; i++)
            {
                var frame = reader.ReadVideoFrame();
                _curPic = frame;
                pic.Image = frame;

                Detect(frame);
				//frame.Dispose();
			}

			reader.Close();
        }

        private void pic_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            
        }
    }
}
