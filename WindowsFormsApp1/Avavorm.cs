using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Avavorm : Form
    {
        private Button nuppPiltideVaatamine;
        private Button nuppMatemaatilineMang;
        private Button nuppSarnasedPildid;

        public Avavorm()
        {
            this.Text = "Kolm Rakendust - Avavorm";
            this.Size = new Size(540, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Taustavärv Bisque
            this.BackColor = Color.Bisque;

            // Nuppude loomine Sisseehitatud värvidega (Tan ja RosyBrown)
            nuppPiltideVaatamine = LooNupp("1. Pildi vaatamise programm", new Point(20, 50), Color.Tan, Color.RosyBrown);
            nuppPiltideVaatamine.Click += (s, e) => new VaatamiseForm().ShowDialog();

            nuppMatemaatilineMang = LooNupp("2. Matemaatiline mäng", new Point(180, 50), Color.Tan, Color.RosyBrown);
            nuppMatemaatilineMang.Click += (s, e) => new MatemaatilineMangForm().ShowDialog();

            nuppSarnasedPildid = LooNupp("3. Sarnaste piltide mäng", new Point(340, 50), Color.Tan, Color.RosyBrown);
            nuppSarnasedPildid.Click += (s, e) => new SarnastePiltideMangForm().ShowDialog();

            this.Controls.Add(nuppPiltideVaatamine);
            this.Controls.Add(nuppMatemaatilineMang);
            this.Controls.Add(nuppSarnasedPildid);
        }

        private Button LooNupp(string tekst, Point asukoht, Color pohivarv, Color pealeLiikumiseVarv)
        {
            Button nupp = new Button
            {
                Text = tekst,
                Location = asukoht,
                Size = new Size(150, 50),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                BackColor = pohivarv,
                ForeColor = Color.SaddleBrown, // Tume mesipruun tekst
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            nupp.FlatAppearance.BorderSize = 0;
            nupp.FlatAppearance.MouseOverBackColor = pealeLiikumiseVarv;

            return nupp;
        }
    }
}