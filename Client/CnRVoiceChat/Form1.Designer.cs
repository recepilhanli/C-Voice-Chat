namespace VoiceChat.Client.Forms
{
    partial class CnRVoiceMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CnRVoiceMain));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.XButton = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.statelabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.volumeSlider2 = new NAudio.Gui.VolumeSlider();
            this.retrybutton = new System.Windows.Forms.Button();
            this.GtaCheck = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XButton)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(369, 35);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox2.Location = new System.Drawing.Point(0, 156);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(369, 38);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // XButton
            // 
            this.XButton.BackColor = System.Drawing.Color.Black;
            this.XButton.BackgroundImage = global::CnRVoiceChat.Properties.Resources.xbutton;
            this.XButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.XButton.Location = new System.Drawing.Point(327, 7);
            this.XButton.Name = "XButton";
            this.XButton.Size = new System.Drawing.Size(30, 23);
            this.XButton.TabIndex = 2;
            this.XButton.TabStop = false;
            this.XButton.Click += new System.EventHandler(this.XButton_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.Black;
            this.Title.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Title.Location = new System.Drawing.Point(12, 11);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(182, 15);
            this.Title.TabIndex = 3;
            this.Title.Text = "SA:MP Türkiye CnR - Sesli Sohbet";
            // 
            // statelabel
            // 
            this.statelabel.AutoSize = true;
            this.statelabel.BackColor = System.Drawing.Color.Black;
            this.statelabel.ForeColor = System.Drawing.SystemColors.Control;
            this.statelabel.Location = new System.Drawing.Point(12, 167);
            this.statelabel.Name = "statelabel";
            this.statelabel.Size = new System.Drawing.Size(47, 15);
            this.statelabel.TabIndex = 4;
            this.statelabel.Text = "Durum:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 19);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Bas Konuş";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(12, 106);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(127, 19);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "Yakınlık Hassasiyeti";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // volumeSlider1
            // 
            this.volumeSlider1.Location = new System.Drawing.Point(233, 68);
            this.volumeSlider1.Name = "volumeSlider1";
            this.volumeSlider1.Size = new System.Drawing.Size(123, 16);
            this.volumeSlider1.TabIndex = 7;
            this.volumeSlider1.VolumeChanged += new System.EventHandler(this.Slider1Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(233, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Mikrofon Ses Seviyesi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Kullanıcı Ses Seviyesi:";
            // 
            // volumeSlider2
            // 
            this.volumeSlider2.Location = new System.Drawing.Point(233, 116);
            this.volumeSlider2.Name = "volumeSlider2";
            this.volumeSlider2.Size = new System.Drawing.Size(123, 16);
            this.volumeSlider2.TabIndex = 9;
            this.volumeSlider2.VolumeChanged += new System.EventHandler(this.Slider2Changed);
            // 
            // retrybutton
            // 
            this.retrybutton.Location = new System.Drawing.Point(250, 163);
            this.retrybutton.Name = "retrybutton";
            this.retrybutton.Size = new System.Drawing.Size(106, 23);
            this.retrybutton.TabIndex = 11;
            this.retrybutton.Text = "Tekrar Dene";
            this.retrybutton.UseVisualStyleBackColor = true;
            this.retrybutton.Click += new System.EventHandler(this.retrybutton_Click);
            // 
            // GtaCheck
            // 
            this.GtaCheck.Interval = 2000;
            this.GtaCheck.Tick += new System.EventHandler(this.Check);
            // 
            // CnRVoiceMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(368, 194);
            this.Controls.Add(this.retrybutton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.volumeSlider2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.volumeSlider1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.statelabel);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.XButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "CnRVoiceMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SA:MP Turkiye CnR - Sesli Sohbet";
            this.TransparencyKey = System.Drawing.Color.Turquoise;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CnRVoiceMain_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CnRVoiceMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CnRVoiceMain_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox XButton;
        private Label Title;
        private Label statelabel;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private NAudio.Gui.VolumeSlider volumeSlider1;
        private Label label1;
        private Label label2;
        private NAudio.Gui.VolumeSlider volumeSlider2;
        private Button retrybutton;
        private System.Windows.Forms.Timer GtaCheck;
    }
}