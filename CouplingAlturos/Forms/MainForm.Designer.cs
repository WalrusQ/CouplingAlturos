namespace CouplingAlturos
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.confidenceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.widthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yoloItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pic = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelYoloInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.btnOpenVideo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.VideoPg = new System.Windows.Forms.TabPage();
            this.OpenVideoTxtBx = new System.Windows.Forms.RichTextBox();
            this.LogTxtBx = new System.Windows.Forms.RichTextBox();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.PlayBtn = new System.Windows.Forms.Button();
            this.PhotoPg = new System.Windows.Forms.TabPage();
            this.OpenPhotoTxtBx = new System.Windows.Forms.RichTextBox();
            this.picBx = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yoloItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.VideoPg.SuspendLayout();
            this.PhotoPg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBx)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.AllowUserToAddRows = false;
            this.dataGridViewResult.AllowUserToDeleteRows = false;
            this.dataGridViewResult.AutoGenerateColumns = false;
            this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeDataGridViewTextBoxColumn,
            this.confidenceDataGridViewTextBoxColumn,
            this.xDataGridViewTextBoxColumn,
            this.yDataGridViewTextBoxColumn,
            this.widthDataGridViewTextBoxColumn,
            this.heightDataGridViewTextBoxColumn});
            this.dataGridViewResult.DataSource = this.yoloItemBindingSource;
            this.dataGridViewResult.Location = new System.Drawing.Point(9, 384);
            this.dataGridViewResult.Name = "dataGridViewResult";
            this.dataGridViewResult.ReadOnly = true;
            this.dataGridViewResult.Size = new System.Drawing.Size(643, 148);
            this.dataGridViewResult.TabIndex = 0;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // confidenceDataGridViewTextBoxColumn
            // 
            this.confidenceDataGridViewTextBoxColumn.DataPropertyName = "Confidence";
            this.confidenceDataGridViewTextBoxColumn.HeaderText = "Confidence";
            this.confidenceDataGridViewTextBoxColumn.MaxInputLength = 4;
            this.confidenceDataGridViewTextBoxColumn.Name = "confidenceDataGridViewTextBoxColumn";
            this.confidenceDataGridViewTextBoxColumn.ReadOnly = true;
            this.confidenceDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // xDataGridViewTextBoxColumn
            // 
            this.xDataGridViewTextBoxColumn.DataPropertyName = "X";
            this.xDataGridViewTextBoxColumn.HeaderText = "X";
            this.xDataGridViewTextBoxColumn.Name = "xDataGridViewTextBoxColumn";
            this.xDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // yDataGridViewTextBoxColumn
            // 
            this.yDataGridViewTextBoxColumn.DataPropertyName = "Y";
            this.yDataGridViewTextBoxColumn.HeaderText = "Y";
            this.yDataGridViewTextBoxColumn.Name = "yDataGridViewTextBoxColumn";
            this.yDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // widthDataGridViewTextBoxColumn
            // 
            this.widthDataGridViewTextBoxColumn.DataPropertyName = "Width";
            this.widthDataGridViewTextBoxColumn.HeaderText = "Width";
            this.widthDataGridViewTextBoxColumn.Name = "widthDataGridViewTextBoxColumn";
            this.widthDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // heightDataGridViewTextBoxColumn
            // 
            this.heightDataGridViewTextBoxColumn.DataPropertyName = "Height";
            this.heightDataGridViewTextBoxColumn.HeaderText = "Height";
            this.heightDataGridViewTextBoxColumn.Name = "heightDataGridViewTextBoxColumn";
            this.heightDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // yoloItemBindingSource
            // 
            this.yoloItemBindingSource.DataSource = typeof(Alturos.Yolo.Model.YoloItem);
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic.Location = new System.Drawing.Point(9, 35);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(640, 315);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic.TabIndex = 1;
            this.pic.TabStop = false;
            this.pic.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pic_LoadCompleted);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(9, 6);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelYoloInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(661, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelYoloInfo
            // 
            this.toolStripStatusLabelYoloInfo.Name = "toolStripStatusLabelYoloInfo";
            this.toolStripStatusLabelYoloInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Location = new System.Drawing.Point(13, 362);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(503, 24);
            this.groupBoxResult.TabIndex = 4;
            this.groupBoxResult.TabStop = false;
            // 
            // btnOpenVideo
            // 
            this.btnOpenVideo.Location = new System.Drawing.Point(9, 6);
            this.btnOpenVideo.Name = "btnOpenVideo";
            this.btnOpenVideo.Size = new System.Drawing.Size(75, 23);
            this.btnOpenVideo.TabIndex = 2;
            this.btnOpenVideo.Text = "Open Video";
            this.btnOpenVideo.UseVisualStyleBackColor = true;
            this.btnOpenVideo.Click += new System.EventHandler(this.btnOpenVideo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(353, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.VideoPg);
            this.tabControl1.Controls.Add(this.PhotoPg);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(663, 564);
            this.tabControl1.TabIndex = 6;
            // 
            // VideoPg
            // 
            this.VideoPg.Controls.Add(this.OpenVideoTxtBx);
            this.VideoPg.Controls.Add(this.LogTxtBx);
            this.VideoPg.Controls.Add(this.pic);
            this.VideoPg.Controls.Add(this.PauseBtn);
            this.VideoPg.Controls.Add(this.PlayBtn);
            this.VideoPg.Controls.Add(this.button1);
            this.VideoPg.Controls.Add(this.btnOpenVideo);
            this.VideoPg.Location = new System.Drawing.Point(4, 22);
            this.VideoPg.Name = "VideoPg";
            this.VideoPg.Padding = new System.Windows.Forms.Padding(3);
            this.VideoPg.Size = new System.Drawing.Size(655, 538);
            this.VideoPg.TabIndex = 0;
            this.VideoPg.Text = "Видео";
            this.VideoPg.UseVisualStyleBackColor = true;
            // 
            // OpenVideoTxtBx
            // 
            this.OpenVideoTxtBx.Location = new System.Drawing.Point(91, 7);
            this.OpenVideoTxtBx.Name = "OpenVideoTxtBx";
            this.OpenVideoTxtBx.Size = new System.Drawing.Size(558, 22);
            this.OpenVideoTxtBx.TabIndex = 7;
            this.OpenVideoTxtBx.Text = "";
            // 
            // LogTxtBx
            // 
            this.LogTxtBx.Location = new System.Drawing.Point(9, 388);
            this.LogTxtBx.Name = "LogTxtBx";
            this.LogTxtBx.Size = new System.Drawing.Size(640, 140);
            this.LogTxtBx.TabIndex = 6;
            this.LogTxtBx.Text = "";
            // 
            // PauseBtn
            // 
            this.PauseBtn.Location = new System.Drawing.Point(272, 359);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(75, 23);
            this.PauseBtn.TabIndex = 5;
            this.PauseBtn.Text = "Pause";
            this.PauseBtn.UseVisualStyleBackColor = true;
            // 
            // PlayBtn
            // 
            this.PlayBtn.Location = new System.Drawing.Point(191, 359);
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.Size = new System.Drawing.Size(75, 23);
            this.PlayBtn.TabIndex = 5;
            this.PlayBtn.Text = "Play";
            this.PlayBtn.UseVisualStyleBackColor = true;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // PhotoPg
            // 
            this.PhotoPg.Controls.Add(this.OpenPhotoTxtBx);
            this.PhotoPg.Controls.Add(this.picBx);
            this.PhotoPg.Controls.Add(this.dataGridViewResult);
            this.PhotoPg.Controls.Add(this.btnOpen);
            this.PhotoPg.Location = new System.Drawing.Point(4, 22);
            this.PhotoPg.Name = "PhotoPg";
            this.PhotoPg.Padding = new System.Windows.Forms.Padding(3);
            this.PhotoPg.Size = new System.Drawing.Size(655, 538);
            this.PhotoPg.TabIndex = 1;
            this.PhotoPg.Text = "Фото";
            this.PhotoPg.UseVisualStyleBackColor = true;
            // 
            // OpenPhotoTxtBx
            // 
            this.OpenPhotoTxtBx.Location = new System.Drawing.Point(90, 6);
            this.OpenPhotoTxtBx.Name = "OpenPhotoTxtBx";
            this.OpenPhotoTxtBx.Size = new System.Drawing.Size(559, 22);
            this.OpenPhotoTxtBx.TabIndex = 8;
            this.OpenPhotoTxtBx.Text = "";
            // 
            // picBx
            // 
            this.picBx.BackColor = System.Drawing.Color.White;
            this.picBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBx.Location = new System.Drawing.Point(9, 35);
            this.picBx.Name = "picBx";
            this.picBx.Size = new System.Drawing.Size(640, 315);
            this.picBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBx.TabIndex = 3;
            this.picBx.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 562);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBoxResult);
            this.Controls.Add(this.statusStrip1);
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yoloItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.VideoPg.ResumeLayout(false);
            this.PhotoPg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.BindingSource yoloItemBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn confidenceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn heightDataGridViewTextBoxColumn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelYoloInfo;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.Button btnOpenVideo;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage VideoPg;
		private System.Windows.Forms.TabPage PhotoPg;
        private System.Windows.Forms.PictureBox picBx;
        private System.Windows.Forms.RichTextBox OpenVideoTxtBx;
        private System.Windows.Forms.RichTextBox LogTxtBx;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Button PlayBtn;
        private System.Windows.Forms.RichTextBox OpenPhotoTxtBx;
    }
}

