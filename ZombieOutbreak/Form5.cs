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
        int level = 1;
        Random randNum = new Random();

        private System.Windows.Forms.Timer shieldTimer = new System.Windows.Forms.Timer();
        private bool shieldActive = false;

        private System.Windows.Forms.Timer uziBurstTimer = new System.Windows.Forms.Timer();
        private int burstShotsFired = 0;

        bool zombiesFrozen = false;

        private System.Windows.Forms.Timer powerUpDropTimer = new System.Windows.Forms.Timer();

        private System.Windows.Forms.Timer startCountdownTimer = new System.Windows.Forms.Timer();
        private int countdownValue = 3;
        private Label countdownLabel = new Label();

        private bool gameStarted = false;

        private System.Windows.Forms.Timer ammoRegenTimer = new System.Windows.Forms.Timer();
        private bool ammoRegenActive = false;

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

            ammoRegenTimer.Interval = 10000;
            ammoRegenTimer.Tick += AmmoRegenTimer_Tick;

            countdownLabel.BringToFront();
        }

        private void PowerUpDropTimer_Tick(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                powerUpDropTimer.Start();
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
     StyleLabel(txtLevel);

     if (currentLanguage == "mk")
     {
         txtAmmo.Text = "Муниција: " + ammo;
         txtKills.Text = "Убиени: " + score;
         txtHealth.Text = "Живот:";
         txtLevel.Text = "Ниво: " + level;
     }
     else
     {
         txtAmmo.Text = "Ammo: " + ammo;
         txtKills.Text = "Kills: " + score;
         txtHealth.Text = "Health:";
         txtLevel.Text = "Level: " + level;
     }

     healthBar.ForeColor = Color.Green;
     healthBar.BackColor = Color.LightGray;
     healthBar.Width = 370;
     healthBar.Height = 50;
     healthBar.Location = new Point(1200, 50);
 }

 private void StyleLabel(Label label)
 {
     label.BackColor = Color.Transparent;
     label.ForeColor = Color.DarkRed;
     label.Font = new Font("Century Gothic", 22, FontStyle.Bold);
     label.AutoSize = true;
     txtAmmo.Location = new Point(550, 50);
     txtKills.Location = new Point(850, 50);
     txtHealth.Location = new Point(1050, 50);
     txtLevel.Location = new Point(350, 50);
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
                powerUpDropTimer.Stop();
                ammoRegenTimer.Stop();

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

            int newLeft = player.Left;
            int newTop = player.Top;

            if (goLeft && player.Left > 0) newLeft -= speed;
            if (goRight && player.Right < this.ClientSize.Width) newLeft += speed;
            if (goUp && player.Top > 45) newTop -= speed;
            if (goDown && player.Bottom < this.ClientSize.Height) newTop += speed;

            Rectangle futurePlayerRect = new Rectangle(newLeft, newTop, player.Width, player.Height);

            bool canMove = true;
            foreach (PictureBox zombie in zombiesList)
            {
                Rectangle zombieCollisionRect = new Rectangle(zombie.Location.X + 10, zombie.Location.Y + 10, zombie.Width - 20, zombie.Height - 20);

                if (futurePlayerRect.IntersectsWith(zombieCollisionRect))
                {
                    canMove = false;
                    break;
                }
            }

            if (canMove)
            {
                player.Left = newLeft;
                player.Top = newTop;
            }


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
            toRemove.Clear();
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
                shieldTimer.Dispose();
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
            if (!gameStarted || zombiesFrozen) return;

            string[] tagParts = zombie.Tag.ToString().Split(':');
            if (tagParts.Length != 2) return;

            string type = tagParts[1];
            string direction = "";
            Point currentPosition = zombie.Location;
            Point intendedNewPosition = zombie.Location;

            if (Math.Abs(zombie.Left - player.Left) > Math.Abs(zombie.Top - player.Top))
            {
                if (zombie.Left > player.Left)
                {
                    intendedNewPosition.X -= zombieSpeed;
                    direction = "left";
                }
                else if (zombie.Left < player.Left)
                {
                    intendedNewPosition.X += zombieSpeed;
                    direction = "right";
                }
            }
            else
            {
                if (zombie.Top > player.Top)
                {
                    intendedNewPosition.Y -= zombieSpeed;
                    direction = "up";
                }
                else if (zombie.Top < player.Top)
                {
                    intendedNewPosition.Y += zombieSpeed;
                    direction = "down";
                }
            }

            Rectangle intendedZombieBounds = new Rectangle(intendedNewPosition, zombie.Size);

            bool canMoveToNewPosition = true;

            Rectangle playerCollisionRect = new Rectangle(player.Location.X + 10, player.Location.Y + 10, player.Width - 20, player.Height - 20);
            if (intendedZombieBounds.IntersectsWith(playerCollisionRect))
            {
                canMoveToNewPosition = false;
            }

            if (canMoveToNewPosition)
            {
                foreach (PictureBox otherZombie in zombiesList)
                {
                    if (otherZombie == zombie) continue;

                    Rectangle otherZombieCollisionRect = new Rectangle(otherZombie.Location.X + 10, otherZombie.Location.Y + 10, otherZombie.Width - 20, otherZombie.Height - 20);

                    if (intendedZombieBounds.IntersectsWith(otherZombieCollisionRect))
                    {
                        canMoveToNewPosition = false;
                        break;
                    }
                }
            }

            if (canMoveToNewPosition)
            {
                zombie.Location = intendedNewPosition;
            }
            else
            {

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
                        if (score % 10 == 0)
                        {
                            LevelUp();
                        }
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

        private void LevelUp()
        {
            level++;
            txtLevel.Text = currentLanguage == "mk" ? "Ниво: " + level : "Level: " + level;

            int zombiesToAdd = 0;
            if (level <= 3)
            {
                zombiesToAdd = 1;
            }
            else if (level <= 6)
            {
                zombiesToAdd = 2;
            }
            else
            {
                zombiesToAdd = 3;
            }

            for (int i = 0; i < zombiesToAdd; i++)
            {
                MakeZombies();
            }

            zombieSpeed += 2;

            gameStarted = false;
            GameTimer.Stop();
            powerUpDropTimer.Stop();
            ammoRegenTimer.Stop();


            string levelUpText = currentLanguage == "mk" ? "НИВО " + level + "!" : "LEVEL " + level + "!";

            Label levelUpLabel = new Label
            {
                Text = levelUpText,
                Font = new Font("Century Gothic", 100, FontStyle.Bold),
                ForeColor = Color.Orange,
                BackColor = Color.Transparent,
                AutoSize = true
            };

            levelUpLabel.Location = new Point(
                (this.ClientSize.Width - levelUpLabel.PreferredWidth) / 2,
                (this.ClientSize.Height - levelUpLabel.PreferredHeight) / 2
            );

            this.Controls.Add(levelUpLabel);
            levelUpLabel.BringToFront();

            System.Windows.Forms.Timer levelUpTimer = new System.Windows.Forms.Timer();
            levelUpTimer.Interval = 2000;
            levelUpTimer.Tick += (s, e) =>
            {
                this.Controls.Remove(levelUpLabel);
                levelUpTimer.Stop();
                levelUpTimer.Dispose();

                gameStarted = true;
                GameTimer.Start();
                powerUpDropTimer.Start();
                if (ammo == 0 && !ammoRegenActive)
                {
                    ammoRegenTimer.Start();
                    ammoRegenActive = true;
                }
            };
            levelUpTimer.Start();
        }


        private void Form5_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameStarted || gameOver) return;

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

        private bool IsZombieInDirection(string direction)
        {
            Rectangle futureArea = player.Bounds;
            int checkDistance = 50;

            switch (direction)
            {
                case "left":
                    futureArea.X -= checkDistance;
                    break;
                case "right":
                    futureArea.X += checkDistance;
                    break;
                case "up":
                    futureArea.Y -= checkDistance;
                    break;
                case "down":
                    futureArea.Y += checkDistance;
                    break;
            }

            foreach (PictureBox zombie in zombiesList)
            {
                if (zombie.Bounds.IntersectsWith(futureArea))
                    return true;
            }

            return false;
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

            int attempts = 0;
            const int maxAttempts = 100;

            while (attempts < maxAttempts)
            {
                attempts++;

                int x = randNum.Next(0, this.ClientSize.Width - 170);
                int y = randNum.Next(50, this.ClientSize.Height - 170);

                Rectangle newZombieRect = new Rectangle(new Point(x, y), new Size(170, 170));

                bool intersects = false;

                if (newZombieRect.IntersectsWith(player.Bounds))
                {
                    intersects = true;
                }

                if (!intersects)
                {
                    foreach (PictureBox existingZombie in zombiesList)
                    {
                        if (newZombieRect.IntersectsWith(new Rectangle(existingZombie.Location, existingZombie.Size)))
                        {
                            intersects = true;
                            break;
                        }
                    }
                }


                if (!intersects)
                {
                    PictureBox zombie = new PictureBox
                    {
                        Tag = $"zombie:{type}",
                        Image = zombieImage,
                        Left = x,
                        Top = y,
                        Size = new Size(170, 170),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.Transparent,
                        Parent = this
                    };

                    zombiesList.Add(zombie);
                    this.Controls.Add(zombie);
                    zombie.BringToFront();
                    player.BringToFront();
                    return;
                }
            }
        }

        private void DropPowerUp()
        {
            string[] powerUpTypes = { "ammo_silver", "ammo_gold", "ammo_bronze", "health", "shield" };
            string selected = powerUpTypes[randNum.Next(powerUpTypes.Length)];

            PictureBox powerUp = new PictureBox
            {
                Size = new Size(200, 200),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Left = randNum.Next(10, this.ClientSize.Width - 250),
                Top = randNum.Next(60, this.ClientSize.Height - 250),
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

        private void AmmoRegenTimer_Tick(object sender, EventArgs e)
        {
            ammo += 5;
            if (currentLanguage == "mk")
            {
                txtAmmo.Text = "Муниција: " + ammo;
            }
            else
            {
                txtAmmo.Text = "Ammo: " + ammo;
            }
            ammoRegenTimer.Stop();
            ammoRegenActive = false;
        }

        private void StartCountdown()
        {
            countdownValue = 3;

            countdownLabel.Font = new Font("Century Gothic", 300, FontStyle.Bold);
            countdownLabel.ForeColor = Color.Red;
            countdownLabel.BackColor = Color.Transparent;
            countdownLabel.AutoSize = true;
            countdownLabel.Text = countdownValue.ToString();
            countdownLabel.Location = new Point((this.ClientSize.Width / 2) + 200, (this.ClientSize.Height / 2));
            countdownLabel.BringToFront();

            if (!this.Controls.Contains(countdownLabel))
                this.Controls.Add(countdownLabel);

            startCountdownTimer.Interval = 1000;
            startCountdownTimer.Tick -= StartCountdownTimer_Tick;
            startCountdownTimer.Tick += StartCountdownTimer_Tick;
            startCountdownTimer.Start();
        }

        private void StartCountdownTimer_Tick(object sender, EventArgs e)
        {
            countdownValue--;
            if (countdownValue > 0)
            {
                countdownLabel.Text = countdownValue.ToString();
            }
            else
            {
                startCountdownTimer.Stop();
                this.Controls.Remove(countdownLabel);
                gameStarted = true;
                GameTimer.Start();
            }
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
            level = 1;
            zombieSpeed = 4;

            if (currentLanguage == "mk")
            {
                txtAmmo.Text = "Муниција: " + ammo;
                txtKills.Text = "Убиени: " + score;
                txtHealth.Text = "Живот:";
                txtLevel.Text = "Ниво: " + level;
            }
            else
            {
                txtAmmo.Text = "Ammo: " + ammo;
                txtKills.Text = "Kills: " + score;
                txtHealth.Text = "Health:";
                txtLevel.Text = "Level: " + level;
            }

            powerUpDropTimer.Stop();
            ammoRegenTimer.Stop();
            ammoRegenActive = false;

            StartCountdown();
        }
    }
}
