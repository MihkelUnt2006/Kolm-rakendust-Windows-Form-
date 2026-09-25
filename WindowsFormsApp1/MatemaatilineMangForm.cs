using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class MatemaatilineMangForm : Form
    {
        private Label siltUlesanne;
        private TextBox sisendVastus;
        private Button nuppKontrolli;
        private Button nuppUusUlesanne;
        private Label siltTulemus;
        private Label siltSkoor;
        private Label siltAeg;
        private ComboBox valikRaskus;
        private Timer taimer;

        private Random juhuslik = new Random();
        private int oikeaVastus;
        private int skoor = 0;
        private int kokkuUlesandeid = 0;
        private int aegaJaanud = 10;

        public MatemaatilineMangForm()
        {
            this.Text = "Matemaatiline Mäng";
            this.Size = new Size(450, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.Bisque;

            Label siltRaskus = new Label
            {
                Text = "Raskusaste:",
                Location = new Point(30, 20),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.SaddleBrown
            };

            valikRaskus = new ComboBox
            {
                Location = new Point(130, 18),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.Cornsilk
            };
            valikRaskus.Items.AddRange(new object[] { "Kerge (+, -)", "Keskmine (*)", "Raske (+, -, *, /)" });
            valikRaskus.SelectedIndex = 0;
            valikRaskus.SelectedIndexChanged += (s, e) => GenereriUlesanne();

            siltAeg = new Label
            {
                Text = "Aeg: 10s",
                Location = new Point(300, 18),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DarkRed
            };

            siltSkoor = new Label
            {
                Text = "Skoor: 0 / 0",
                Location = new Point(30, 55),
                Size = new Size(370, 30),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.SaddleBrown,
                TextAlign = ContentAlignment.MiddleCenter
            };

            siltUlesanne = new Label
            {
                Text = "5 + 5 = ?",
                Location = new Point(30, 95),
                Size = new Size(370, 50),
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.SaddleBrown
            };

            sisendVastus = new TextBox
            {
                Location = new Point(145, 155),
                Size = new Size(140, 35),
                Font = new Font("Segoe UI", 16),
                TextAlign = HorizontalAlignment.Center,
                BackColor = Color.Cornsilk
            };
            sisendVastus.KeyDown += SisendVastus_KeyDown;

            nuppKontrolli = LooNupp("Kontrolli", new Point(80, 210), Color.Tan);
            nuppKontrolli.Click += (s, e) => KontrolliVastust();

            nuppUusUlesanne = LooNupp("Uus ülesanne", new Point(225, 210), Color.BurlyWood);
            nuppUusUlesanne.Click += (s, e) => GenereriUlesanne();

            siltTulemus = new Label
            {
                Text = "",
                Location = new Point(30, 275),
                Size = new Size(370, 40),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            taimer = new Timer { Interval = 1000 };
            taimer.Tick += Taimer_Tick;

            this.Controls.AddRange(new Control[] {
                siltRaskus, valikRaskus, siltAeg, siltSkoor,
                siltUlesanne, sisendVastus, nuppKontrolli, nuppUusUlesanne, siltTulemus
            });

            GenereriUlesanne();
        }

        private Button LooNupp(string tekst, Point asukoht, Color taust)
        {
            Button nupp = new Button
            {
                Text = tekst,
                Location = asukoht,
                Size = new Size(130, 42),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = taust,
                ForeColor = Color.SaddleBrown,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            nupp.FlatAppearance.BorderSize = 0;
            return nupp;
        }

        private void GenereriUlesanne()
        {
            taimer.Stop();
            aegaJaanud = 10;
            siltAeg.Text = $"Aeg: {aegaJaanud}s";
            sisendVastus.Clear();
            siltTulemus.Text = "";
            sisendVastus.Focus();

            int a = 0, b = 0;
            int tehe = juhuslik.Next(0, 4);

            if (valikRaskus.SelectedIndex == 0)
            {
                a = juhuslik.Next(1, 20);
                b = juhuslik.Next(1, 20);
                tehe = juhuslik.Next(0, 2);
            }
            else if (valikRaskus.SelectedIndex == 1)
            {
                a = juhuslik.Next(2, 11);
                b = juhuslik.Next(2, 11);
                tehe = 2;
            }
            else
            {
                a = juhuslik.Next(5, 50);
                b = juhuslik.Next(2, 12);
            }

            switch (tehe)
            {
                case 0:
                    oikeaVastus = a + b;
                    siltUlesanne.Text = $"{a} + {b} = ?";
                    break;
                case 1:
                    oikeaVastus = a - b;
                    siltUlesanne.Text = $"{a} - {b} = ?";
                    break;
                case 2:
                    oikeaVastus = a * b;
                    siltUlesanne.Text = $"{a} × {b} = ?";
                    break;
                case 3:
                    oikeaVastus = a;
                    int korrutis = a * b;
                    siltUlesanne.Text = $"{korrutis} ÷ {b} = ?";
                    break;
            }

            taimer.Start();
        }

        private void KontrolliVastust()
        {
            taimer.Stop();

            if (int.TryParse(sisendVastus.Text, out int kasutajaVastus))
            {
                kokkuUlesandeid++;
                if (kasutajaVastus == oikeaVastus)
                {
                    skoor++;
                    siltTulemus.ForeColor = Color.DarkGreen;
                    siltTulemus.Text = "Õige vastus! :)";
                }
                else
                {
                    siltTulemus.ForeColor = Color.DarkRed;
                    siltTulemus.Text = $"Vale! >:( Õige vastus oli: {oikeaVastus}";
                }
            }
            else
            {
                siltTulemus.ForeColor = Color.SaddleBrown;
                siltTulemus.Text = "Palun sisesta number!";
            }

            UuendaSkoor();
        }

        private void Taimer_Tick(object sender, EventArgs e)
        {
            aegaJaanud--;
            siltAeg.Text = $"Aeg: {aegaJaanud}s";

            if (aegaJaanud <= 0)
            {
                taimer.Stop();
                kokkuUlesandeid++;
                siltTulemus.ForeColor = Color.DarkRed;
                siltTulemus.Text = $"Aeg sai läbi! Õige vastus: {oikeaVastus}";
                UuendaSkoor();
            }
        }

        private void UuendaSkoor()
        {
            siltSkoor.Text = $"Skoor: {skoor} / {kokkuUlesandeid}";
        }

        private void SisendVastus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                KontrolliVastust();
                e.SuppressKeyPress = true;
            }
        }
    }
}