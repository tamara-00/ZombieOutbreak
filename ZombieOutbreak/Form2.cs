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
    public partial class Form2 : Form
    {
        private string currentLanguage;
        public Form2(string language)
        {
            InitializeComponent();

            DoubleBuffered = true;

            this.KeyPreview = true;

            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            currentLanguage = language;

            if(currentLanguage == "mk")
            {
                this.Text = "Избери оружје";
            } else
            {
                this.Text = "Choose a Gun";
            }
                
            SetBackgroundImageBasedOnLanguage();
            ApplyButtonStylesAndText();
        }

        private void SetBackgroundImageBasedOnLanguage()
        {
            if (currentLanguage == "mk")
            {
                using (MemoryStream ms = new MemoryStream(Properties.Resources.mkdChooseGUN))
                {
                    this.BackgroundImage = Image.FromStream(ms);
                }
            }
            else
            {
                this.BackgroundImage = Properties.Resources.englChooseGUN;
            }

            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Form1 prvaForma = new Form1();
                prvaForma.WindowState = FormWindowState.Maximized;
                prvaForma.Show();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(currentLanguage);
            form3.WindowState = FormWindowState.Maximized;
            form3.Show();
            Form1.musicPlayer.Stop();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(currentLanguage);
            form4.WindowState = FormWindowState.Maximized;
            form4.Show();
            Form1.musicPlayer.Stop();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(currentLanguage);
            form5.WindowState = FormWindowState.Maximized;
            form5.Show();
            Form1.musicPlayer.Stop();
            this.Close();
        }

        private void ApplyButtonStylesAndText()
        {
            button1.Location = new Point(880, 1660);
            button1.Size = new Size(170, 60);
            button1.BackColor = Color.FromArgb(180, 0, 0);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Font = new Font("Century Gothic", 14, FontStyle.Bold);
            button1.TextAlign = ContentAlignment.MiddleCenter;
            button1.Text = "Select";

            button2.Location = new Point(1325, 1660);
            button2.Size = new Size(170, 60);
            button2.BackColor = Color.FromArgb(180, 0, 0);
            button2.ForeColor = Color.White;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Font = new Font("Century Gothic", 14, FontStyle.Bold);
            button2.TextAlign = ContentAlignment.MiddleCenter;

            button3.Location = new Point(1770, 1660);
            button3.Size = new Size(170, 60);
            button3.BackColor = Color.FromArgb(180, 0, 0);
            button3.ForeColor = Color.White;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.Font = new Font("Century Gothic", 14, FontStyle.Bold);
            button3.TextAlign = ContentAlignment.MiddleCenter;

            if (currentLanguage == "mk")
            {
                button1.Text = "Избери";
                button2.Text = "Избери";
                button3.Text = "Избери";
            }
            else 
            {
                button1.Text = "Select";
                button2.Text = "Select";
                button3.Text = "Select";
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
