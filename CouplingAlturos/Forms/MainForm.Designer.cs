﻿namespace CouplingAlturos
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
			this.btnDetect = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelYoloInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBoxResult = new System.Windows.Forms.GroupBox();
			this.btnOpenVideo = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.yoloItemBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
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
			this.dataGridViewResult.Location = new System.Drawing.Point(12, 392);
			this.dataGridViewResult.Name = "dataGridViewResult";
			this.dataGridViewResult.ReadOnly = true;
			this.dataGridViewResult.Size = new System.Drawing.Size(643, 150);
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
			this.pic.Location = new System.Drawing.Point(13, 13);
			this.pic.Name = "pic";
			this.pic.Size = new System.Drawing.Size(503, 343);
			this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pic.TabIndex = 1;
			this.pic.TabStop = false;
			this.pic.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pic_LoadCompleted);
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(523, 13);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(75, 23);
			this.btnOpen.TabIndex = 2;
			this.btnOpen.Text = "Open";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnDetect
			// 
			this.btnDetect.Location = new System.Drawing.Point(522, 42);
			this.btnDetect.Name = "btnDetect";
			this.btnDetect.Size = new System.Drawing.Size(75, 23);
			this.btnDetect.TabIndex = 2;
			this.btnDetect.Text = "Detect";
			this.btnDetect.UseVisualStyleBackColor = true;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelYoloInfo});
			this.statusStrip1.Location = new System.Drawing.Point(0, 557);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(663, 22);
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
			this.btnOpenVideo.Location = new System.Drawing.Point(522, 85);
			this.btnOpenVideo.Name = "btnOpenVideo";
			this.btnOpenVideo.Size = new System.Drawing.Size(75, 23);
			this.btnOpenVideo.TabIndex = 2;
			this.btnOpenVideo.Text = "Open Video";
			this.btnOpenVideo.UseVisualStyleBackColor = true;
			this.btnOpenVideo.Click += new System.EventHandler(this.btnOpenVideo_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(522, 114);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Stop";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(522, 159);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(200, 100);
			this.tabControl1.TabIndex = 6;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(192, 74);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(192, 74);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(663, 579);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBoxResult);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnDetect);
			this.Controls.Add(this.btnOpenVideo);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.pic);
			this.Controls.Add(this.dataGridViewResult);
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
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnDetect;
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
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
	}
}

