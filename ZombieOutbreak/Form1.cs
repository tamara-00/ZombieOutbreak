using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace ZombieOutbreak
{
    public partial class Form1 : Form
    {
        private string currentLanguage = "en";

        private bool isMusicPlaying = true;
        public static SoundPlayer musicPlayer;
        private Image musicOnIcon;
        private Image musicOffIcon;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.Text = "Zombie Outbreak";

            this.KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Location = new Point(300, 500);
            button1.Size = new Size(480, 110);
            button1.BackColor = Color.FromArgb(255, 100, 0);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Font = new Font("Century Gothic", 24, FontStyle.Bold);
            button1.TextAlign = ContentAlignment.MiddleCenter;
            button1.Text = "Play";

            button2.Location = new Point(300, 650);
            button2.Size = new Size(480, 110);
            button2.BackColor = Color.FromArgb(180, 0, 0);
            button2.ForeColor = Color.White;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Font = new Font("Century Gothic", 24, FontStyle.Bold);
            button2.TextAlign = ContentAlignment.MiddleCenter;
            button2.Text = "Exit";

            label1.Location = new Point(280, 950);
            label1.Size = new Size(480, 110);
            label1.Font = new Font("Century Gothic", 24, FontStyle.Regular);
            label1.Text = "Language:";
            label1.BackColor = Color.Black;
            label1.ForeColor = Color.White;

            comboBox1.Location = new Point(292, 1060);
            comboBox1.Size = new Size(480, 110);
            comboBox1.Font = new Font("Century Gothic", 20, FontStyle.Regular);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.BackColor = Color.DimGray;
            comboBox1.ForeColor = Color.White;
            comboBox1.Items.Add("Македонски");
            comboBox1.Items.Add("English");
            if (currentLanguage == "mk")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            comboBox1.SelectedIndexChanged += LanguageBox_SelectedIndexChanged;

            label2.Location = new Point(280, 1230);
            label2.Size = new Size(480, 110);
            label2.Font = new Font("Century Gothic", 24, FontStyle.Regular);
            label2.Text = "Music: On";
            label2.BackColor = Color.Black;
            label2.ForeColor = Color.White;

            musicOnIcon = Properties.Resources.musicOn;
            using (MemoryStream ms = new MemoryStream(Properties.Resources.musicOff))
            {
                musicOffIcon = Image.FromStream(ms);
            }

            pictureBox1.Location = new Point(665, 1212);
            pictureBox1.Size = new Size(120, 120);
            pictureBox1.Image = musicOnIcon;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BackColor = Color.Black;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Click += pictureBox1_Click;

            try
            {
                MemoryStream ms = new MemoryStream(Properties.Resources.musicAudio);
                musicPlayer = new SoundPlayer(ms);
                musicPlayer.PlayLooping();
                isMusicPlaying = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при пуштање музика: " + ex.Message);
            }
        }

        private void LanguageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Македонски")
            {
                currentLanguage = "mk";
            }
            else
            {
                currentLanguage = "en";
            }

            if (currentLanguage == "mk")
            {
                button1.Text = "Играј";
                button2.Text = "Излез";
                label1.Text = "Јазик:";
                if (isMusicPlaying)
                {
                    label2.Text = "Музика: Да";
                }
                else
                {
                    label2.Text = "Музика: Не";
                }
            }
            else
            {
                button1.Text = "Play";
                button2.Text = "Exit";
                label1.Text = "Language:";
                if (isMusicPlaying)
                {
                    label2.Text = "Music: On";
                }
                else
                {
                    label2.Text = "Music: Off";
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 novaForma = new Form2(currentLanguage);
            novaForma.WindowState = FormWindowState.Maximized;
            novaForma.MinimizeBox = false;
            novaForma.MaximizeBox = false;
            novaForma.FormBorderStyle = FormBorderStyle.FixedDialog;
            novaForma.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isMusicPlaying)
            {
                musicPlayer.Stop();
                isMusicPlaying = false;
                pictureBox1.Image = musicOffIcon;
                if (currentLanguage == "mk")
                {
                    label2.Text = "Музика: Не";
                }
                else
                {
                    label2.Text = "Music: Off";
                }
            }
            else
            {
                musicPlayer.PlayLooping();
                isMusicPlaying = true;
                pictureBox1.Image = musicOnIcon;
                if (currentLanguage == "mk")
                {
                    label2.Text = "Музика: Да";
                }
                else
                {
                    label2.Text = "Music: On";
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
    }
}