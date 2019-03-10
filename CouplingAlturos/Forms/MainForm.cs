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
using CouplingAlturos.Core.Models;
using CouplingAlturos.Model;

namespace CouplingAlturos
{
    
    public partial class MainForm : Form
    {
        private YoloWrapper _yoloWrapper;

		public IImageDetector ImageDetector { get; }

		public IVideoDetector VideoDetector { get; }

		public MainForm(IImageDetector imageDetector, IVideoDetector videoDetector)
        {
	        ImageDetector = imageDetector;
	        VideoDetector = videoDetector;
            
            InitializeComponent();
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
                    pic.Image = Image.FromFile(ofd.FileName);
                    RecognitionOutput(ImageDetector.Process(pic.Image));

                }
            }
        }

        private void RecognitionOutput(RecognitionResult result)
        {
            dataGridViewResult.DataSource = result.Items;
            DrawBorder2Image(result);
            logToXml(result);
        }



        private void DrawBorder2Image(RecognitionResult result)
        {

            var image = result.Image;
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
            pic.Image = image;
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

			var progress = new Progress<RecognitionResult>(result =>
			{
                
				Debug.WriteLine("Eeee");
			});


			Task.Factory.StartNew(() =>
			{
				VideoDetector.Process("Resources/test.avi", progress); 
			});
	
   //         var reader = new VideoFileReader();
   //         reader.Open("test.avi"); 
 
   //         for (var i = 0; i < 10; i++)
   //         {
   //             var frame = reader.ReadVideoFrame();
   //             _curPic = frame;
   //             pic.Image = frame;

   //             Detect(frame);
			//	//frame.Dispose();
			//}

			//reader.Close();
        }

        private void logToXml(RecognitionResult result)
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
    }
}
