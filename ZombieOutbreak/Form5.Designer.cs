namespace ZombieOutbreak
{
    partial class Form5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtAmmo = new Label();
            txtKills = new Label();
            txtHealth = new Label();
            healthBar = new ProgressBar();
            logoBox = new PictureBox();
            player = new PictureBox();
            GameTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            SuspendLayout();
            // 
            // txtAmmo
            // 
            txtAmmo.AutoSize = true;
            txtAmmo.Location = new Point(127, 28);
            txtAmmo.Name = "txtAmmo";
            txtAmmo.Size = new Size(78, 32);
            txtAmmo.TabIndex = 0;
            txtAmmo.Text = "label1";
            // 
            // txtKills
            // 
            txtKills.AutoSize = true;
            txtKills.Location = new Point(606, 28);
            txtKills.Name = "txtKills";
            txtKills.Size = new Size(78, 32);
            txtKills.TabIndex = 1;
            txtKills.Text = "label2";
            // 
            // txtHealth
            // 
            txtHealth.AutoSize = true;
            txtHealth.Location = new Point(1118, 28);
            txtHealth.Name = "txtHealth";
            txtHealth.Size = new Size(78, 32);
            txtHealth.TabIndex = 2;
            txtHealth.Text = "label3";
            // 
            // healthBar
            // 
            healthBar.Location = new Point(1230, 14);
            healthBar.Name = "healthBar";
            healthBar.Size = new Size(340, 55);
            healthBar.TabIndex = 3;
            // 
            // logoBox
            // 
            logoBox.BackgroundImageLayout = ImageLayout.Stretch;
            logoBox.Image = Properties.Resources.logoRound;
            logoBox.Location = new Point(1451, 774);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(200, 100);
            logoBox.TabIndex = 4;
            logoBox.TabStop = false;
            // 
            // player
            // 
            player.Image = Properties.Resources.uzi4;
            player.Location = new Point(205, 172);
            player.Name = "player";
            player.Size = new Size(670, 844);
            player.SizeMode = PictureBoxSizeMode.AutoSize;
            player.TabIndex = 5;
            player.TabStop = false;
            // 
            // GameTimer
            // 
            GameTimer.Tick += MainTimer;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1675, 900);
            Controls.Add(player);
            Controls.Add(logoBox);
            Controls.Add(healthBar);
            Controls.Add(txtHealth);
            Controls.Add(txtKills);
            Controls.Add(txtAmmo);
            Name = "Form5";
            Text = "Zombie Outbreak";
            Load += Form5_Load;
            KeyDown += Form5_KeyDown;
            KeyUp += Form5_KeyUp;
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtAmmo;
        private Label txtKills;
        private Label txtHealth;
        private ProgressBar healthBar;
        private PictureBox logoBox;
        private PictureBox player;
        private System.Windows.Forms.Timer GameTimer;
    }
}