using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ZombieOutbreak
{
    class Bullet
    {
        public string direction;
        public int bulletLeft;
        public int bulletTop;

        private int speed = 20;
        private PictureBox bullet = new PictureBox();
        private System.Windows.Forms.Timer bulletTimer = new System.Windows.Forms.Timer();

        private Point origin;  
        private int maxDistance = int.MaxValue; 
        public bool IsUziBullet = false; 

        public void MakeBullet(Form form)
        {
            bullet.Tag = "bullet";
            bullet.BackColor = Color.Transparent;

            Image img;
            using (MemoryStream ms = new MemoryStream(Properties.Resources.bullet1))
            {
                img = Image.FromStream(ms);
            }

            if (direction == "up" || direction == "down")
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                bullet.Size = new Size(15, 80);
            }
            else
            {
                bullet.Size = new Size(80, 15);
            }

            bullet.Image = img;
            bullet.SizeMode = PictureBoxSizeMode.StretchImage;

            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;

            origin = bullet.Location;

            form.Controls.Add(bullet);
            bullet.BringToFront();

            bulletTimer.Interval = 20;
            bulletTimer.Tick += BulletTimerEvent;
            bulletTimer.Start();
        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {
            switch (direction)
            {
                case "left":
                    bullet.Left -= speed;
                    break;
                case "right":
                    bullet.Left += speed;
                    break;
                case "up":
                    bullet.Top -= speed;
                    break;
                case "down":
                    bullet.Top += speed;
                    break;
            }

            int dx = bullet.Left - origin.X;
            int dy = bullet.Top - origin.Y;
            double dist = Math.Sqrt(dx * dx + dy * dy);

            if (IsUziBullet && dist > 450)
            {
                RemoveBullet();
                return;
            }

            if (bullet.Left < 0 || bullet.Left > Screen.PrimaryScreen.WorkingArea.Width ||
                bullet.Top < 0 || bullet.Top > Screen.PrimaryScreen.WorkingArea.Height)
            {
                RemoveBullet();
            }
        }

        private void RemoveBullet()
        {
            bulletTimer.Stop();
            bulletTimer.Tick -= BulletTimerEvent;
            bulletTimer.Dispose();
            bullet.Dispose();

            bullet = null;
            bulletTimer = null;
        }
    }
}
