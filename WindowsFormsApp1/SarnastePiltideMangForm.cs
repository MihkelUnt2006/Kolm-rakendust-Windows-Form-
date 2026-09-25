using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SarnastePiltideMangForm : Form
    {
        private TableLayoutPanel ruudustikPaneel;
        private Label siltOleks;
        private Button nuppUusMang;
        private Label esimeneValitud = null;
        private Label teineValitud = null;
        private Timer peitmisTaimer;
        private Random juhuslikArv = new Random();

        // Sisseehitatud beežikad värvid
        private readonly Color kaardiTaust = Color.Tan;
        private readonly Color kaardiAvas = Color.Cornsilk;

        private List<string> sumbolid = new List<string>
        {
            "c", "c", "b", "b", "N", "N", "z", "z",
            "!", "!", "v", "v", "w", "w", "k", "k"
        };

        public SarnastePiltideMangForm()
        {
            this.Text = "Sarnaste piltide mäng";
            this.Size = new Size(520, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.Bisque;

            siltOleks = new Label
            {
                Text = "Leia kõik sarnased paarid!",
                Dock = DockStyle.Top,
                Height = 45,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.SaddleBrown,
                TextAlign = ContentAlignment.MiddleCenter
            };

            ruudustikPaneel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 4,
                Padding = new Padding(12),
                BackColor = Color.Bisque
            };

            for (int i = 0; i < 4; i++)
            {
                ruudustikPaneel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                ruudustikPaneel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            nuppUusMang = new Button
            {
                Text = "Uus mäng",
                Dock = DockStyle.Bottom,
                Height = 45,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.Tan,
                ForeColor = Color.SaddleBrown,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            nuppUusMang.FlatAppearance.BorderSize = 0;
            nuppUusMang.Click += (s, e) => AlustaUutMangu();

            peitmisTaimer = new Timer { Interval = 750 };
            peitmisTaimer.Tick += PeitmisTaimer_Tick;

            this.Controls.Add(ruudustikPaneel);
            this.Controls.Add(siltOleks);
            this.Controls.Add(nuppUusMang);

            AlustaUutMangu();
        }

        private void AlustaUutMangu()
        {
            peitmisTaimer.Stop();
            esimeneValitud = null;
            teineValitud = null;
            siltOleks.Text = "Leia kõik sarnased paarid!";
            ruudustikPaneel.Controls.Clear();

            List<string> kopeeritudSumbolid = new List<string>(sumbolid);

            for (int i = 0; i < 16; i++)
            {
                int indeks = juhuslikArv.Next(kopeeritudSumbolid.Count);

                Label sildiRuut = new Label
                {
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Webdings", 44, FontStyle.Bold),
                    Text = kopeeritudSumbolid[indeks],
                    ForeColor = kaardiTaust,
                    BackColor = kaardiTaust,
                    Margin = new Padding(4),
                    Cursor = Cursors.Hand
                };

                sildiRuut.Click += SildiRuut_Click;
                kopeeritudSumbolid.RemoveAt(indeks);
                ruudustikPaneel.Controls.Add(sildiRuut);
            }
        }

        private void SildiRuut_Click(object sender, EventArgs e)
        {
            if (peitmisTaimer.Enabled) return;

            Label vajutatudSilt = sender as Label;

            if (vajutatudSilt != null)
            {
                if (vajutatudSilt.ForeColor == Color.SaddleBrown) return;

                if (esimeneValitud == null)
                {
                    esimeneValitud = vajutatudSilt;
                    esimeneValitud.ForeColor = Color.SaddleBrown;
                    esimeneValitud.BackColor = kaardiAvas;
                    return;
                }

                teineValitud = vajutatudSilt;
                teineValitud.ForeColor = Color.SaddleBrown;
                teineValitud.BackColor = kaardiAvas;

                if (esimeneValitud.Text == teineValitud.Text)
                {
                    esimeneValitud = null;
                    teineValitud = null;
                    KontrolliVoitu();
                    return;
                }

                peitmisTaimer.Start();
            }
        }

        private void PeitmisTaimer_Tick(object sender, EventArgs e)
        {
            peitmisTaimer.Stop();

            if (esimeneValitud != null)
            {
                esimeneValitud.ForeColor = kaardiTaust;
                esimeneValitud.BackColor = kaardiTaust;
            }
            if (teineValitud != null)
            {
                teineValitud.ForeColor = kaardiTaust;
                teineValitud.BackColor = kaardiTaust;
            }

            esimeneValitud = null;
            teineValitud = null;
        }

        private void KontrolliVoitu()
        {
            foreach (Control c in ruudustikPaneel.Controls)
            {
                Label l = c as Label;
                if (l != null && l.ForeColor != Color.SaddleBrown) return;
            }

            siltOleks.Text = "Palju õnne! Kõik paarid leitud!";
            MessageBox.Show("Oled mängu võitnud!", "Võit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}