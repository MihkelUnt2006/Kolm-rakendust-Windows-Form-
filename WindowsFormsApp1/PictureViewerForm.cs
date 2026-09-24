using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class PildiVaatamiseForm : Form
    {
        private PictureBox pildiKast;
        private Button nuppAvaPilt;
        private Button nuppPuhasta;
        private Button nuppTaustaVarv;

        public PildiVaatamiseForm()
        {
            
            this.Text = "Pildi vaatamise programm";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            
            pildiKast = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(560, 380),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            
            nuppAvaPilt = new Button
            {
                Text = "Ava pilt",
                Location = new Point(10, 400),
                Size = new Size(100, 35)
            };
            nuppAvaPilt.Click += NuppAvaPilt_Click;

            
            nuppPuhasta = new Button
            {
                Text = "Puhasta",
                Location = new Point(120, 400),
                Size = new Size(100, 35)
            };
            nuppPuhasta.Click += (saatja, sündmus) => pildiKast.Image = null;

           
            nuppTaustaVarv = new Button
            {
                Text = "Taustavärv",
                Location = new Point(230, 400),
                Size = new Size(100, 35)
            };
            nuppTaustaVarv.Click += NuppTaustaVarv_Click;

            
            this.Controls.Add(pildiKast);
            this.Controls.Add(nuppAvaPilt);
            this.Controls.Add(nuppPuhasta);
            this.Controls.Add(nuppTaustaVarv);
        }

        
        private void NuppAvaPilt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog avamiseDialoog = new OpenFileDialog())
            {
                avamiseDialoog.Filter = "Pildid (*.jpg;*.png)|*.jpg;*.png";
                if (avamiseDialoog.ShowDialog() == DialogResult.OK)
                {
                    pildiKast.Image = Image.FromFile(avamiseDialoog.FileName);
                }
            }
        }

        
        private void NuppTaustaVarv_Click(object sender, EventArgs e)
        {
            using (ColorDialog varviDialoog = new ColorDialog())
            {
                if (varviDialoog.ShowDialog() == DialogResult.OK)
                {
                    pildiKast.BackColor = varviDialoog.Color;
                }
            }
        }
    }
}