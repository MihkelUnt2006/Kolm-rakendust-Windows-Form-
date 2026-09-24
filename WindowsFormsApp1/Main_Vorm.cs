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
            this.Size = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            
            nuppPiltideVaatamine = new Button
            {
                Text = "1. Pildi vaatamise programm",
                Location = new Point(100, 300),
                Size = new Size(250, 100)
            };
            nuppPiltideVaatamine.Click += (saatja, sündmus) => new PildiVaatamiseForm().ShowDialog();

            
            nuppMatemaatilineMang = new Button
            {
                Text = "2. Matemaatiline mäng",
                Location = new Point(350, 300),
                Size = new Size(250, 100)
            };
            nuppMatemaatilineMang.Click += (saatja, sündmus) => new MatemaatilineMangForm().ShowDialog();

            nuppSarnasedPildid = new Button
            {
                Text = "3. Sarnaste piltide mäng",
                Location = new Point(600, 300),
                Size = new Size(250, 100)
            };
            nuppSarnasedPildid.Click += (saatja, sündmus) => new SarnastePiltideMangForm().ShowDialog();


            this.Controls.Add(nuppPiltideVaatamine);
            this.Controls.Add(nuppMatemaatilineMang);
            this.Controls.Add(nuppSarnasedPildid);
        }
    }
}