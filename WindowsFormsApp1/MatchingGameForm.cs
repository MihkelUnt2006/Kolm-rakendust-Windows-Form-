using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SarnastePiltideMangForm : Form
    {
        private List<string> sümbolid = new List<string> { "A", "A", "B", "B", "C", "C", "D", "D" };
        private Label esimeneValitud = null;
        private Label teineValitud = null;
        private Timer peitmisTaimer;

        public SarnastePiltideMangForm()
        {
            this.Text = "Sarnaste piltide mäng";
            this.Size = new Size(320, 350);
            this.StartPosition = FormStartPosition.CenterParent;

            TableLayoutPanel ruudustik = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 4,
                BackColor = Color.LightSkyBlue
            };

            for (int i = 0; i < 4; i++) ruudustik.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            for (int i = 0; i < 2; i++) ruudustik.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            Random juhuslikArv = new Random();

            for (int i = 0; i < 8; i++)
            {
                int indeks = juhuslikArv.Next(sümbolid.Count);
                Label sildiRuut = new Label
                {
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 24, FontStyle.Bold),
                    Text = sümbolid[indeks],
                    ForeColor = Color.LightSkyBlue, // Sümbol peidetakse taustavärvi taha
                    Margin = new Padding(3),
                    BorderStyle = BorderStyle.FixedSingle
                };
                sildiRuut.Click += SildiRuut_Click;
                sümbolid.RemoveAt(indeks);
                ruudustik.Controls.Add(sildiRuut);
            }

            peitmisTaimer = new Timer { Interval = 750 };
            peitmisTaimer.Tick += (saatja, sündmus) =>
            {
                peitmisTaimer.Stop();
                esimeneValitud.ForeColor = esimeneValitud.BackColor;
                teineValitud.ForeColor = teineValitud.BackColor;
                esimeneValitud = null;
                teineValitud = null;
            };

            this.Controls.Add(ruudustik);
        }

        private void SildiRuut_Click(object sender, EventArgs e)
        {
            if (peitmisTaimer.Enabled) return;

            Label vajutatudSilt = sender as Label;

            if (vajutatudSilt != null && vajutatudSilt.ForeColor == Color.LightSkyBlue)
            {
                vajutatudSilt.ForeColor = Color.Black; 

                if (esimeneValitud == null)
                {
                    esimeneValitud = vajutatudSilt;
                    return;
                }

                teineValitud = vajutatudSilt;

                if (esimeneValitud.Text == teineValitud.Text)
                {
                    esimeneValitud = null;
                    teineValitud = null;
                }
                else
                {
                    peitmisTaimer.Start();
                }
            }
        }
    }
}