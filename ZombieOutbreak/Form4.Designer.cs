namespace ZombieOutbreak
{
    partial class Form4
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
            GameTimer = new System.Windows.Forms.Timer(components);
            player = new PictureBox();
            txtAmmo = new Label();
            txtKills = new Label();
            txtHealth = new Label();
            healthBar = new ProgressBar();
            logoBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();
            SuspendLayout();
            // 
            // GameTimer
            // 
            GameTimer.Tick += MainTimer;
            // 
            // player
            // 
            player.Image = Properties.Resources.shotgun4;
            player.Location = new Point(243, 107);
            player.Name = "player";
            player.Size = new Size(570, 888);
            player.SizeMode = PictureBoxSizeMode.AutoSize;
            player.TabIndex = 0;
            player.TabStop = false;
            // 
            // txtAmmo
            // 
            txtAmmo.AutoSize = true;
            txtAmmo.Location = new Point(61, 36);
            txtAmmo.Name = "txtAmmo";
            txtAmmo.Size = new Size(78, 32);
            txtAmmo.TabIndex = 1;
            txtAmmo.Text = "label1";
            // 
            // txtKills
            // 
            txtKills.AutoSize = true;
            txtKills.Location = new Point(655, 36);
            txtKills.Name = "txtKills";
            txtKills.Size = new Size(78, 32);
            txtKills.TabIndex = 2;
            txtKills.Text = "label2";
            // 
            // txtHealth
            // 
            txtHealth.AutoSize = true;
            txtHealth.Location = new Point(1235, 36);
            txtHealth.Name = "txtHealth";
            txtHealth.Size = new Size(78, 32);
            txtHealth.TabIndex = 3;
            txtHealth.Text = "label3";
            // 
            // healthBar
            // 
            healthBar.Location = new Point(1338, 22);
            healthBar.Name = "healthBar";
            healthBar.Size = new Size(293, 57);
            healthBar.TabIndex = 4;
            // 
            // logoBox
            // 
            logoBox.BackgroundImageLayout = ImageLayout.Stretch;
            logoBox.Image = Properties.Resources.logoRound;
            logoBox.Location = new Point(1450, 773);
            logoBox.Name = "logoBox";
            logoBox.Size = new Size(200, 100);
            logoBox.TabIndex = 5;
            logoBox.TabStop = false;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1680, 900);
            Controls.Add(logoBox);
            Controls.Add(healthBar);
            Controls.Add(txtHealth);
            Controls.Add(txtKills);
            Controls.Add(txtAmmo);
            Controls.Add(player);
            Name = "Form4";
            Text = "Zombie Outbreak";
            Load += Form4_Load;
            KeyDown += Form4_KeyDown;
            KeyUp += Form4_KeyUp;
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private PictureBox player;
        private Label txtAmmo;
        private Label txtKills;
        private Label txtHealth;
        private ProgressBar healthBar;
        private PictureBox logoBox;
    }
}