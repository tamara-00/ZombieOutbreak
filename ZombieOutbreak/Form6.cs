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
    public partial class Form6 : Form
    {
        public string currentLanguage { get; set; }
        public string sourceForm { get; set; }
        public Form6(string language, string fromForm)
        {
            InitializeComponent();
            currentLanguage = language;
            sourceForm = fromForm;

            DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
           
            if (language == "mk")
            {
                this.Text = "Играта заврши";
                this.BackgroundImage = Properties.Resources.gameOverMKD;
                button1.Text = "Обиди се повторно";
                button2.Text = "Избери друго оружје";
                button3.Text = "Излез";
            } 
            else
            {
                button1.Text = "Retry";
                button2.Text = "Choose Another Weapon";
                button3.Text = "Quit";
            }

            StyleButton(button1, new Point(1090, 1250));
            StyleButton(button2, new Point(1090, 1400));
            StyleButton(button3, new Point(1090, 1550));
        }
        private void StyleButton(Button btn, Point location)
        {
            btn.Size = new Size(700, 100);
            btn.Location = location;
            btn.Font = new Font("Century Gothic", 18, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(160, 30, 30, 30); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form retryForm = null;

            switch (sourceForm)
            {
                case "Form3":
                    retryForm = new Form3(currentLanguage);
                    break;
                case "Form4":
                    retryForm = new Form4(currentLanguage);
                    break;
                case "Form5":
                    retryForm = new Form5(currentLanguage);
                    break;
                default:
                    MessageBox.Show("Unknown form. Cannot retry.");
                    return;
            }

            retryForm.WindowState = FormWindowState.Maximized;
            retryForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2(currentLanguage);
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
