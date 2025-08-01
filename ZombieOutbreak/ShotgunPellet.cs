using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieOutbreak
{
    public class ShotgunPellet
    {
        public int speed = 20;
        public int range = 800;
        public string direction;
        public int distanceTraveled = 0;

        PictureBox pellet = new PictureBox();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int angleOffset;

        public ShotgunPellet(string direction, int angleOffset)
        {
            this.direction = direction;
            this.angleOffset = angleOffset;
        }

        public void Fire(Form form, int startX, int startY)
        {
            pellet.Tag = "bullet1";

            Image img;
            using (MemoryStream ms = new MemoryStream(Properties.Resources.bullet1))
            {
                img = Image.FromStream(ms);
            }

            if (direction == "up" || direction == "down")
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pellet.Size = new Size(55, 70);
            }
            else
            {
                pellet.Size = new Size(70, 55);
            }

            img = RotateImage(img, angleOffset); 

            pellet.Image = img;
            pellet.SizeMode = PictureBoxSizeMode.StretchImage;
            pellet.BackColor = Color.Transparent;

            pellet.Left = startX;
            pellet.Top = startY;
            pellet.BringToFront();

            form.Controls.Add(pellet);

            timer.Interval = 20;
            timer.Tick += (s, e) =>
            {
                MovePellet();

                distanceTraveled += speed;
                if (distanceTraveled > range)
                {
                    timer.Stop();
                    pellet.Dispose();
                }
            };
            timer.Start();
        }

        private void MovePellet()
        {
            double angle = 0;

            switch (direction)
            {
                case "up": angle = -90; break;
                case "down": angle = 90; break;
                case "left": angle = 180; break;
                case "right": angle = 0; break;
            }

            double rad = (angle + angleOffset) * (Math.PI / 180);
            int dx = (int)(Math.Cos(rad) * speed);
            int dy = (int)(Math.Sin(rad) * speed);

            pellet.Left += dx;
            pellet.Top += dy;
        }

        private Bitmap RotateImage(Image image, float angle)
        {
            int side = (int)(Math.Sqrt(image.Width * image.Width + image.Height * image.Height));
            Bitmap rotated = new Bitmap(side, side);
            rotated.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotated))
            {
                g.Clear(Color.Transparent);
                g.TranslateTransform(side / 2f, side / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-image.Width / 2f, -image.Height / 2f);
                g.DrawImage(image, new Point(0, 0));
            }

            return rotated;
        }
    }
}
