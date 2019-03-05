using Accord.Video.FFMPEG;
using Alturos.Yolo;
using Alturos.Yolo.Model;
using CouplingAlturos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CouplingAlturos
{
    
    public partial class Form1 : Form
    {
        private YoloWrapper _yoloWrapper;
        private Image curPic;
        public Form1()
        {

            InitializeComponent();
            Task.Run(() => this.Initialize("."));
        }

        private void Initialize(string path)
        {
            var configurationDetector = new ConfigurationDetector();
            var config = configurationDetector.Detect(path);

            if (config == null)
            {
                return;
            }

            this.Initialize(config);
        }

        private void Initialize(YoloConfiguration config)
        {
            try
            {
                if (this._yoloWrapper != null)
                {
                    this._yoloWrapper.Dispose();
                }

                var sw = new Stopwatch();
                sw.Start();
                this._yoloWrapper = new YoloWrapper(config.ConfigFile, config.WeightsFile, config.NamesFile, 0, true);
                sw.Stop();

                var action = new MethodInvoker(delegate ()
                {
                    var detectionSystemDetail = string.Empty;
                    if (!string.IsNullOrEmpty(this._yoloWrapper.EnvironmentReport.GraphicDeviceName))
                    {
                        detectionSystemDetail = $"({this._yoloWrapper.EnvironmentReport.GraphicDeviceName})";
                    }
                   
                    this.toolStripStatusLabelYoloInfo.Text = $"Initialize Yolo in {sw.Elapsed.TotalMilliseconds:0} ms - Detection System:{this._yoloWrapper.DetectionSystem} {detectionSystemDetail} Weights:{config.WeightsFile}";
                });

                this.statusStrip1.Invoke(action);
                this.btnDetect.Invoke(new MethodInvoker(delegate () { this.btnDetect.Enabled = true; }));
                this.btnOpenVideo.Invoke(new MethodInvoker(delegate () { this.btnOpenVideo.Enabled = true; }));
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{nameof(Initialize)} - {exception}", "Error Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.png; *.jpg; *.jpeg| *.png; *.jpg; *.jpeg"  })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    curPic = Image.FromFile(ofd.FileName);
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
            if (this._yoloWrapper == null)
            {
                return;
            }
            
            var imageData = ImageToByteArray(img);

            var sw = new Stopwatch();
            sw.Start();
            List<YoloItem> items;
            items = this._yoloWrapper.Detect(imageData).ToList(); 
            sw.Stop();
            this.groupBoxResult.Text = $"Result [ processed in {sw.Elapsed.TotalMilliseconds:0} ms ]";

            this.dataGridViewResult.DataSource = items;
            this.DrawBorder2Image(items);
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            this.Detect(pic.Image);
        }

        private void DrawBorder2Image(List<YoloItem> items, YoloItem selectedItem = null)
        {
            var image = curPic;
           // string path = "E:\\Khakaton_Mallenom\\team6\\1_101.jpg";
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
                    using (var pen = this.GetBrush(item.Confidence, image.Width))
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

            var oldImage = this.pic.Image;
            this.pic.Image = image;
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
            this._yoloWrapper?.Dispose();
        }

        private void btnOpenVideo_Click(object sender, EventArgs e)
        {
            VideoFileReader reader = new VideoFileReader();
            reader.Open("test.avi");
 
            for (var i = 0; i < 10; i++)
            {
                Image videoframe = reader.ReadVideoFrame();
                curPic = videoframe;
                pic.Image = videoframe;

                Detect(videoframe);
                //videoframe.Dispose();
            }
               

               
               
            reader.Close();
        }

        private void pic_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            
        }
    }
}
