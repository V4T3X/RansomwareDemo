using System.Windows.Forms;

namespace Decrypter
{
    partial class Mainform
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private PictureBox lock_img;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.lock_img = new System.Windows.Forms.PictureBox();
            this.clock = new System.Windows.Forms.Label();
            this.countdown = new System.Windows.Forms.Timer(this.components);
            this.timeLeftLabel = new System.Windows.Forms.Label();
            this.destroyed = new System.Windows.Forms.Label();
            this.deadline = new System.Windows.Forms.Label();
            this.about = new System.Windows.Forms.LinkLabel();
            this.info = new System.Windows.Forms.LinkLabel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btcAddress = new System.Windows.Forms.TextBox();
            this.copy = new System.Windows.Forms.Button();
            this.btc = new System.Windows.Forms.PictureBox();
            this.keyImport = new System.Windows.Forms.OpenFileDialog();
            this.select = new System.Windows.Forms.Button();
            this.decrypt_button = new System.Windows.Forms.Button();
            this.sendBitcoin = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.lock_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btc)).BeginInit();
            this.SuspendLayout();
            // 
            // lock_img
            // 
            this.lock_img.BackColor = System.Drawing.Color.Transparent;
            this.lock_img.Location = new System.Drawing.Point(85, 40);
            this.lock_img.Name = "lock_img";
            this.lock_img.Size = new System.Drawing.Size(250, 250);
            this.lock_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.lock_img.TabIndex = 0;
            this.lock_img.TabStop = false;
            this.lock_img.WaitOnLoad = true;
            // 
            // clock
            // 
            this.clock.AutoSize = true;
            this.clock.BackColor = System.Drawing.Color.Transparent;
            this.clock.Font = new System.Drawing.Font("LCDMono2", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clock.ForeColor = System.Drawing.Color.White;
            this.clock.Location = new System.Drawing.Point(49, 655);
            this.clock.Name = "clock";
            this.clock.Size = new System.Drawing.Size(252, 47);
            this.clock.TabIndex = 0;
            this.clock.Text = "00:00:00";
            this.clock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.clock.Click += new System.EventHandler(this.clock_Click);
            // 
            // countdown
            // 
            this.countdown.Enabled = true;
            this.countdown.Interval = 1000;
            this.countdown.Tick += new System.EventHandler(this.countdown_Tick);
            // 
            // timeLeftLabel
            // 
            this.timeLeftLabel.AutoSize = true;
            this.timeLeftLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLeftLabel.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLeftLabel.ForeColor = System.Drawing.Color.White;
            this.timeLeftLabel.Location = new System.Drawing.Point(155, 613);
            this.timeLeftLabel.Name = "timeLeftLabel";
            this.timeLeftLabel.Size = new System.Drawing.Size(116, 30);
            this.timeLeftLabel.TabIndex = 1;
            this.timeLeftLabel.Text = "Time left";
            this.timeLeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // destroyed
            // 
            this.destroyed.AutoSize = true;
            this.destroyed.BackColor = System.Drawing.Color.Transparent;
            this.destroyed.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destroyed.ForeColor = System.Drawing.Color.White;
            this.destroyed.Location = new System.Drawing.Point(68, 400);
            this.destroyed.Name = "destroyed";
            this.destroyed.Size = new System.Drawing.Size(297, 30);
            this.destroyed.TabIndex = 2;
            this.destroyed.Text = "Key will be destroyed on";
            this.destroyed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deadline
            // 
            this.deadline.AutoSize = true;
            this.deadline.BackColor = System.Drawing.Color.Transparent;
            this.deadline.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deadline.ForeColor = System.Drawing.Color.Firebrick;
            this.deadline.Location = new System.Drawing.Point(130, 433);
            this.deadline.Name = "deadline";
            this.deadline.Size = new System.Drawing.Size(150, 35);
            this.deadline.TabIndex = 3;
            this.deadline.Text = "DEADLINE";
            this.deadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // about
            // 
            this.about.ActiveLinkColor = System.Drawing.Color.IndianRed;
            this.about.AutoSize = true;
            this.about.BackColor = System.Drawing.Color.Transparent;
            this.about.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.about.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.about.Location = new System.Drawing.Point(54, 804);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(101, 18);
            this.about.TabIndex = 4;
            this.about.TabStop = true;
            this.about.Text = "About Bitcoin";
            this.about.VisitedLinkColor = System.Drawing.Color.Violet;
            this.about.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.about_LinkClicked);
            // 
            // info
            // 
            this.info.ActiveLinkColor = System.Drawing.Color.IndianRed;
            this.info.AutoSize = true;
            this.info.BackColor = System.Drawing.Color.Transparent;
            this.info.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.info.Location = new System.Drawing.Point(53, 845);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(173, 19);
            this.info.TabIndex = 5;
            this.info.TabStop = true;
            this.info.Text = "Important Information";
            this.info.VisitedLinkColor = System.Drawing.Color.Violet;
            this.info.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.info_LinkClicked);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Nirmala Text", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(444, 81);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(827, 531);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1173, 38);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(98, 32);
            this.comboBox1.TabIndex = 7;
            // 
            // btcAddress
            // 
            this.btcAddress.BackColor = System.Drawing.Color.White;
            this.btcAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btcAddress.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btcAddress.Location = new System.Drawing.Point(607, 711);
            this.btcAddress.Name = "btcAddress";
            this.btcAddress.ReadOnly = true;
            this.btcAddress.Size = new System.Drawing.Size(577, 43);
            this.btcAddress.TabIndex = 8;
            this.btcAddress.Text = " 1F4K3C0IN123456789000BTC9876";
            // 
            // copy
            // 
            this.copy.BackColor = System.Drawing.Color.White;
            this.copy.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copy.Location = new System.Drawing.Point(1190, 711);
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(62, 43);
            this.copy.TabIndex = 9;
            this.copy.Text = "Copy";
            this.copy.UseVisualStyleBackColor = false;
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // btc
            // 
            this.btc.BackColor = System.Drawing.Color.Transparent;
            this.btc.Location = new System.Drawing.Point(450, 633);
            this.btc.Name = "btc";
            this.btc.Size = new System.Drawing.Size(150, 135);
            this.btc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btc.TabIndex = 10;
            this.btc.TabStop = false;
            this.btc.WaitOnLoad = true;
            // 
            // keyImport
            // 
            this.keyImport.FileName = "privateKey";
            // 
            // select
            // 
            this.select.BackColor = System.Drawing.Color.White;
            this.select.Cursor = System.Windows.Forms.Cursors.Hand;
            this.select.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.select.Location = new System.Drawing.Point(444, 788);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(400, 47);
            this.select.TabIndex = 11;
            this.select.Text = "Select Key File";
            this.select.UseVisualStyleBackColor = false;
            this.select.Click += new System.EventHandler(this.button1_Click);
            // 
            // decrypt_button
            // 
            this.decrypt_button.BackColor = System.Drawing.Color.White;
            this.decrypt_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.decrypt_button.Enabled = false;
            this.decrypt_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decrypt_button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.decrypt_button.Location = new System.Drawing.Point(872, 788);
            this.decrypt_button.Name = "decrypt_button";
            this.decrypt_button.Size = new System.Drawing.Size(399, 47);
            this.decrypt_button.TabIndex = 12;
            this.decrypt_button.Text = "Decrypt";
            this.decrypt_button.UseVisualStyleBackColor = false;
            this.decrypt_button.Click += new System.EventHandler(this.decrypt_button_Click);
            // 
            // sendBitcoin
            // 
            this.sendBitcoin.AutoSize = true;
            this.sendBitcoin.BackColor = System.Drawing.Color.Transparent;
            this.sendBitcoin.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendBitcoin.ForeColor = System.Drawing.Color.Orange;
            this.sendBitcoin.Location = new System.Drawing.Point(600, 654);
            this.sendBitcoin.Name = "sendBitcoin";
            this.sendBitcoin.Size = new System.Drawing.Size(583, 35);
            this.sendBitcoin.TabIndex = 13;
            this.sendBitcoin.Text = "Send $250k worth of Bitcoin to this address:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(445, 844);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(825, 21);
            this.progressBar1.TabIndex = 14;
            this.progressBar1.Visible = false;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1300, 900);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.sendBitcoin);
            this.Controls.Add(this.decrypt_button);
            this.Controls.Add(this.select);
            this.Controls.Add(this.btc);
            this.Controls.Add(this.copy);
            this.Controls.Add(this.btcAddress);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.info);
            this.Controls.Add(this.about);
            this.Controls.Add(this.deadline);
            this.Controls.Add(this.destroyed);
            this.Controls.Add(this.timeLeftLabel);
            this.Controls.Add(this.clock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Decrypter 1.0";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Mainform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lock_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label clock;
        private Timer countdown;
        private Label timeLeftLabel;
        private Label destroyed;
        private Label deadline;
        private LinkLabel about;
        private LinkLabel info;
        private RichTextBox richTextBox1;
        private ComboBox comboBox1;
        private TextBox btcAddress;
        private Button copy;
        private PictureBox btc;
        private OpenFileDialog keyImport;
        private Button select;
        private Button decrypt_button;
        private Label sendBitcoin;
        private ProgressBar progressBar1;
    }
}

