using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieOutbreak
{
    public partial class Form5 : Form
    {
        private List<Image> backgroundImages = new List<Image>();
        private Dictionary<string, Image[]> zombieImages;
        private List<PictureBox> zombiesList = new List<PictureBox>();

        List<PictureBox> toRemove = new List<PictureBox>();

        private string currentLanguage;

        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 16;
        int ammo = 12;
        int zombieSpeed = 4;
        int score;
        Random randNum = new Random();

        private System.Windows.Forms.Timer shieldTimer = new System.Windows.Forms.Timer();
        private bool shieldActive = false;

        private System.Windows.Forms.Timer uziBurstTimer = new System.Windows.Forms.Timer();
        private int burstShotsFired = 0;

        bool zombiesFrozen = false;

        private System.Windows.Forms.Timer powerUpDropTimer = new System.Windows.Forms.Timer();
        public Form5(string language)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            currentLanguage = language;

            LoadBackgroundImages();
            LoadZombieImages();
            CustomizeHUD();
            RestartGame();
            SetupLogoBox();

            powerUpDropTimer.Interval = 15000; 
            powerUpDropTimer.Tick += PowerUpDropTimer_Tick;
        }

        private void PowerUpDropTimer_Tick(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                DropPowerUp();
            }
            else
            {
                powerUpDropTimer.Stop();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void SetupLogoBox()
        {
            logoBox.Width = 200;
            logoBox.Height = 150;
            logoBox.BackColor = Color.Transparent;
            logoBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoBox.Location = new Point(2600, 1550);
        }
        private void LoadBackgroundImages()
        {
            backgroundImages.Add(Image.FromStream(new MemoryStream(Properties.Resources.grass1)));
            backgroundImages.Add(Image.FromStream(new MemoryStream(Properties.Resources.grass2)));
            backgroundImages.Add(Image.FromStream(new MemoryStream(Properties.Resources.snowtown)));
            backgroundImages.Add(Image.FromStream(new MemoryStream(Properties.Resources.street)));
            backgroundImages.Add(Image.FromStream(new MemoryStream(Properties.Resources.trainstation)));
        }

        private void LoadZombieImages()
        {
            zombieImages = new Dictionary<string, Image[]>
            {
                { "z", new Image[] { Properties.Resources.z1, Properties.Resources.z2, Properties.Resources.z3, Properties.Resources.z4 } },
                { "m", new Image[] { Properties.Resources.m1, Properties.Resources.m2, Properties.Resources.m3, Properties.Resources.m4 } },
                { "b", new Image[] { Properties.Resources.b1, Properties.Resources.b2, Properties.Resources.b3, Properties.Resources.b4 } },
                { "ar", new Image[] { Properties.Resources.ar1, Properties.Resources.ar2, Properties.Resources.ar3, Properties.Resources.ar4 } }
            };
        }

        private void CustomizeHUD()
        {
            StyleLabel(txtAmmo);
            StyleLabel(txtKills);
            StyleLabel(txtHealth);

            if (currentLanguage == "mk")
            {
                txtAmmo.Text = "Муниција: " + ammo;
                txtKills.Text = "Убиени: " + score;
                txtHealth.Text = "Живот:";
            }
            else
            {
                txtAmmo.Text = "Ammo: " + ammo;
                txtKills.Text = "Kills: " + score;
                txtHealth.Text = "Health:";
            }

            healthBar.ForeColor = Color.Green;
            healthBar.BackColor = Color.LightGray;
            healthBar.Width = 370;
            healthBar.Height = 50;
            healthBar.Location = new Point(2000, 60);
        }

        private void StyleLabel(Label label)
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.DarkRed;
            label.Font = new Font("Century Gothic", 22, FontStyle.Bold);
            label.AutoSize = true;
            txtAmmo.Location = new Point(550, 50);
            txtKills.Location = new Point(1250, 50);
            txtHealth.Location = new Point(1750, 50);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            SetRandomBackgroundImage();
        }

        private void SetRandomBackgroundImage()
        {
            if (backgroundImages.Count == 0) return;

            int index = randNum.Next(backgroundImages.Count);
            this.BackgroundImage = backgroundImages[index];
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private int GetZombieImageIndexByDirection(string direction)
        {
            switch (direction)
            {
                case "down":
                    return 0;
                case "left":
                    return 1;
                case "up":
                    return 2;
                case "right":
                    return 3;
                default:
                    return 0;
            }
        }
        private void MainTimer(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                healthBar.Value = playerHealth;
                UpdateHealthBarColor();
            }
            else
            {
                gameOver = true;
                if (facing == "left")
                {
                    player.Image = Properties.Resources.dead1;
                    player.Width = 400;
                    player.Height = 270;
                }
                else if (facing == "right")
                {
                    player.Image = Properties.Resources.dead2;
                    player.Width = 400;
                    player.Height = 270;
                }
                else if (facing == "up")
                {
                    player.Image = Properties.Resources.dead3;
                    player.Width = 270;
                    player.Height = 400;
                }
                else if (facing == "down")
                {
                    player.Image = Properties.Resources.dead4;
                    player.Width = 270;
                    player.Height = 400;
                }
                else
                {
                    player.Image = Properties.Resources.dead1;
                    player.Width = 400;
                    player.Height = 270;
                }
                player.BackColor = Color.Transparent;
                GameTimer.Stop();

                System.Windows.Forms.Timer deathDelayTimer = new System.Windows.Forms.Timer();
                deathDelayTimer.Interval = 1000;
                deathDelayTimer.Tick += (s, args) =>
                {
                    deathDelayTimer.Stop();
                    deathDelayTimer.Dispose();

                    Form6 gameOverForm = new Form6(currentLanguage, "Form5");
                    gameOverForm.Show();
                    this.Hide();
                };
                deathDelayTimer.Start();
            }

            if (currentLanguage == "mk")
            {
                txtAmmo.Text = "Муниција: " + ammo;
                txtKills.Text = "Убиени: " + score;
            }
            else
            {
                txtAmmo.Text = "Ammo: " + ammo;
                txtKills.Text = "Kills: " + score;
            }

            if (goLeft && player.Left > 0) player.Left -= speed;
            if (goRight && player.Right < this.ClientSize.Width) player.Left += speed;
            if (goUp && player.Top > 45) player.Top -= speed;
            if (goDown && player.Bottom < this.ClientSize.Height) player.Top += speed;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox pic)
                {
                    if (pic.Tag != null && pic.Tag.ToString().StartsWith("zombie"))
                    {
                        if (player.Bounds.IntersectsWith(pic.Bounds) && !shieldActive)
                            playerHealth--;
                        MoveZombieTowardsPlayer(pic);
                        CheckBulletZombieCollision(pic);
                        continue;
                    }

                    if (player.Bounds.IntersectsWith(pic.Bounds))
                    {
                        switch ((string)pic.Tag)
                        {
                            case "ammo":
                                ammo += 5;
                                toRemove.Add(pic);
                                break;
                            case "ammo_silver":
                                ammo += 5;
                                toRemove.Add(pic);
                                break;
                            case "ammo_gold":
                                ammo += 10;
                                toRemove.Add(pic);
                                break;
                            case "ammo_bronze":
                                ammo += 3;
                                toRemove.Add(pic);
                                break;
                            case "health":
                                playerHealth += 20;
                                if (playerHealth > 100) playerHealth = 100;
                                toRemove.Add(pic);
                                break;
                            case "shield":
                                ActivateShield();
                                toRemove.Add(pic);
                                break;
                        }
                    }
                }
            }

            foreach (var pic in toRemove)
            {
                this.Controls.Remove(pic);
                pic.Dispose();
            }
        }

        private void ActivateShield()
        {
            shieldActive = true;
            zombiesFrozen = true;
            shieldTimer.Interval = 5000;
            shieldTimer.Tick += (s, e) =>
            {
                shieldActive = false;
                zombiesFrozen = false;
                shieldTimer.Stop();
            };
            shieldTimer.Start();
        }

        private void UpdateHealthBarColor()
        {
            if (playerHealth > 60) healthBar.ForeColor = Color.Green;
            else if (playerHealth > 30) healthBar.ForeColor = Color.Orange;
            else healthBar.ForeColor = Color.Red;
        }

        private void MoveZombieTowardsPlayer(PictureBox zombie)
        {
            if (zombiesFrozen) return;

            string[] tagParts = zombie.Tag.ToString().Split(':');
            if (tagParts.Length != 2) return;

            string type = tagParts[1];
            string direction = "";

            if (Math.Abs(zombie.Left - player.Left) > Math.Abs(zombie.Top - player.Top))
            {
                if (zombie.Left > player.Left)
                {
                    zombie.Left -= zombieSpeed;
                    direction = "left";
                }
                else if (zombie.Left < player.Left)
                {
                    zombie.Left += zombieSpeed;
                    direction = "right";
                }
            }
            else
            {
                if (zombie.Top > player.Top)
                {
                    zombie.Top -= zombieSpeed;
                    direction = "up";
                }
                else if (zombie.Top < player.Top)
                {
                    zombie.Top += zombieSpeed;
                    direction = "down";
                }
            }

            if (!string.IsNullOrEmpty(direction) && zombieImages.ContainsKey(type))
            {
                int imageIndex = GetZombieImageIndexByDirection(direction);
                zombie.Image = zombieImages[type][imageIndex];
            }
        }

        private void CheckBulletZombieCollision(PictureBox zombie)
        {
            foreach (Control j in this.Controls)
            {
                if (j is PictureBox bullet && (string)bullet.Tag == "bullet")
                {
                    if (zombie.Bounds.IntersectsWith(bullet.Bounds))
                    {
                        score++;
                        this.Controls.Remove(bullet);
                        bullet.Dispose();
                        this.Controls.Remove(zombie);
                        zombie.Dispose();
                        zombiesList.Remove(zombie);
                        MakeZombies();
                        if (randNum.Next(100) < 20)
                            DropPowerUp();
                        break; 
                    }
                }
            }
        }

        private void Form5_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameOver) return;

            switch (e.KeyCode)
            {
                case Keys.Left: goLeft = true; facing = "left"; player.Image = Properties.Resources.uzi3; player.Height = 200; player.Width = 240; break;
                case Keys.Right: goRight = true; facing = "right"; player.Image = Properties.Resources.uzi1; player.Height = 200; player.Width = 240; break;
                case Keys.Up: goUp = true; facing = "up"; player.Image = Properties.Resources.uzi4; player.Width = 200; player.Height = 240; break;
                case Keys.Down: goDown = true; facing = "down"; player.Image = Properties.Resources.uzi2; player.Width = 200; player.Height = 240; break;
            }

            if (e.KeyCode == Keys.Escape)
            {
                Form2 vtoraForma = new Form2(currentLanguage);
                vtoraForma.WindowState = FormWindowState.Maximized;
                vtoraForma.Show();
                this.Close();
            }
        }

        private void Form5_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: goLeft = false; break;
                case Keys.Right: goRight = false; break;
                case Keys.Up: goUp = false; break;
                case Keys.Down: goDown = false; break;

                case Keys.Space:
                    if (ammo > 0 && !gameOver)
                    {
                        ShootBullet(facing);
                    }
                    break;

                case Keys.Enter:
                    if (gameOver) RestartGame();
                    break;
            }
        }

        private void ShootBullet(string direction)
        {
            burstShotsFired = 0;
            int burstCount = randNum.Next(3, 6);

            uziBurstTimer.Interval = 100;

            EventHandler handler = null;

            handler = (s, e) =>
            {
                if (burstShotsFired < burstCount && ammo > 0)
                {
                    Bullet bullet = new Bullet
                    {
                        direction = direction,
                        bulletLeft = player.Left + (player.Width / 2),
                        bulletTop = player.Top + (player.Height / 2),

                        IsUziBullet = true
                    };
                    bullet.MakeBullet(this);
                    ammo--;
                    burstShotsFired++;
                }
                else
                {
                    uziBurstTimer.Stop();
                    uziBurstTimer.Tick -= handler;

                    if (ammo < 1)
                    {
                        DropPowerUp();
                    }
                }
            };

            uziBurstTimer.Tick += handler;
            uziBurstTimer.Start();
        }

        private void MakeZombies()
        {
            string[] types = { "z", "m", "b", "ar" };
            string type = types[randNum.Next(types.Length)];
            int frame = randNum.Next(4);
            Image zombieImage = zombieImages[type][frame];

            PictureBox zombie = new PictureBox
            {
                Tag = $"zombie:{type}",
                Image = zombieImage,
                Left = randNum.Next(0, 900),
                Top = randNum.Next(0, 800),
                Size = new Size(170, 170),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Parent = this
            };

            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            zombie.BringToFront();
            player.BringToFront();
        }

        private void DropPowerUp()
        {
            string[] powerUpTypes = { "ammo_silver", "ammo_gold", "ammo_bronze", "health", "shield" };
            string selected = powerUpTypes[randNum.Next(powerUpTypes.Length)];

            PictureBox powerUp = new PictureBox
            {
                Size = new Size(200, 200),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Left = randNum.Next(10, this.ClientSize.Width - 110),
                Top = randNum.Next(60, this.ClientSize.Height - 110),
                Tag = selected,
                BackColor = Color.Transparent,
                Parent = this
            };

            switch (selected)
            {
                case "ammo_silver":
                    powerUp.Image = Properties.Resources.ammo_silver;
                    break;
                case "ammo_gold":
                    powerUp.Image = Properties.Resources.amno_gold;
                    break;
                case "ammo_bronze":
                    powerUp.Image = Properties.Resources.ammo_bronze;
                    break;
                case "health":
                    powerUp.Image = Properties.Resources.heatlh;
                    break;
                case "shield":
                    powerUp.Image = Properties.Resources.shield;
                    break;
            }

            this.Controls.Add(powerUp);
            powerUp.BringToFront();
            player.BringToFront();

            if (!powerUpDropTimer.Enabled)
            {
                powerUpDropTimer.Start();
            }

            System.Windows.Forms.Timer removeTimer = new System.Windows.Forms.Timer();
            removeTimer.Interval = 15000; 
            removeTimer.Tick += (s, e) =>
            {
                if (this.Controls.Contains(powerUp))
                {
                    this.Controls.Remove(powerUp);
                    powerUp.Dispose();
                }
                removeTimer.Stop();
                removeTimer.Dispose();
            };
            removeTimer.Start();
        }

        private void RestartGame()
        {
            player.Image = Properties.Resources.uzi4;
            player.Size = new Size(200, 240);
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.BackColor = Color.Transparent;
            player.Left = (this.ClientSize.Width / 2) + 400;
            player.Top = (this.ClientSize.Height / 2) + 300;

            foreach (PictureBox i in zombiesList)
            {
                this.Controls.Remove(i);
            }
            zombiesList.Clear();

            for (int i = 0; i < 3; i++) MakeZombies();

            goUp = goDown = goLeft = goRight = false;
            gameOver = false;
            playerHealth = 100;
            score = 0;
            ammo = 12;
            GameTimer.Start();

            if (currentLanguage == "mk")
            {
                txtAmmo.Text = "Муниција: " + ammo;
                txtKills.Text = "Убиени: " + score;
                txtHealth.Text = "Живот:";
            }
            else
            {
                txtAmmo.Text = "Ammo: " + ammo;
                txtKills.Text = "Kills: " + score;
                txtHealth.Text = "Health:";
            }

            powerUpDropTimer.Stop();
        }
    }
}
