namespace ZombieOutbreak
{
    partial class Form3
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
            player = new PictureBox();
            GameTimer = new System.Windows.Forms.Timer(components);
            logoBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();
            SuspendLayout();
            // 
            // txtAmmo
            // 
            txtAmmo.AutoSize = true;
            txtAmmo.Location = new Point(56, 32);
            txtAmmo.Name = "txtAmmo";
            txtAmmo.Size = new Size(78, 32);
            txtAmmo.TabIndex = 0;
            txtAmmo.Text = "label1";
            // 
            // txtKills
            // 
            txtKills.AutoSize = true;
            txtKills.Location = new Point(633, 32);
            txtKills.Name = "txtKills";
            txtKills.Size = new Size(78, 32);
            txtKills.TabIndex = 1;
            txtKills.Text = "label2";
            // 
            // txtHealth
            // 
            txtHealth.AutoSize = true;
            txtHealth.Location = new Point(1159, 32);
            txtHealth.Name = "txtHealth";
            txtHealth.Size = new Size(78, 32);
            txtHealth.TabIndex = 2;
            txtHealth.Text = "label3";
            // 
            // healthBar
            // 
            healthBar.Location = new Point(1365, 32);
            healthBar.Name = "healthBar";
            healthBar.Size = new Size(200, 46);
            healthBar.TabIndex = 3;
            // 
            // player
            // 
            player.Image = Properties.Resources.rifle4;
            player.Location = new Point(204, 107);
            player.Name = "player";
            player.Size = new Size(578, 915);
            player.SizeMode = PictureBoxSizeMode.AutoSize;
            player.TabIndex = 4;
            player.TabStop = false;
            // 
            // GameTimer
            // 
            GameTimer.Enabled = true;
            GameTimer.Interval = 20;
            GameTimer.Tick += MainTimer;
            // 
            // logoBox
            // 
            logoBox.BackgroundImageLayout = ImageLayout.Stretch;
            logoBox.Image = Properties.Resources.logoRound;
            logoBox.Location = new Point(1463, 735);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(200, 100);
            logoBox.TabIndex = 5;
            logoBox.TabStop = false;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1675, 847);
            Controls.Add(logoBox);
            Controls.Add(player);
            Controls.Add(healthBar);
            Controls.Add(txtHealth);
            Controls.Add(txtKills);
            Controls.Add(txtAmmo);
            Name = "Form3";
            Text = "Zombie Outbreak";
            Load += Form3_Load;
            KeyDown += Form3_KeyDown;
            KeyUp += Form3_KeyUp;
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtAmmo;
        private Label txtKills;
        private Label txtHealth;
        private ProgressBar healthBar;
        private PictureBox player;
        private System.Windows.Forms.Timer GameTimer;
        private PictureBox logoBox;
    }
}