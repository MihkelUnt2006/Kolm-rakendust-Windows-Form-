using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class VaatamiseForm : Form
    {
        private PictureBox pildiKast;
        private FlowLayoutPanel nupuPaneel;
        private Label siltInfo;

        private Button nuppAva;
        private Button nuppSalvesta;
        private Button nuppPooorata;
        private Button nuppPeegelda;
        private Button nuppMustValge;
        private Button nuppSepia;
        private Button nuppReziim;
        private Button nuppTühista;

        private Image originaalPilt;

        public VaatamiseForm()
        {
            this.Text = "Pildi vaatamine ja redigeerimine";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.Bisque;

            siltInfo = new Label
            {
                Text = "Ava pilt, et alustada redigeerimist",
                Dock = DockStyle.Top,
                Height = 35,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.SaddleBrown,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pildiKast = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Cornsilk,
                BorderStyle = BorderStyle.FixedSingle
            };

            nupuPaneel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(10),
                BackColor = Color.Wheat,
                AutoScroll = true
            };

            nuppAva = LooNupp("Ava pilt", Color.Tan, Color.SaddleBrown);
            nuppAva.Click += NuppAva_Click;

            nuppSalvesta = LooNupp("Salvesta", Color.BurlyWood, Color.SaddleBrown);
            nuppSalvesta.Click += NuppSalvesta_Click;

            nuppPooorata = LooNupp("Pööra 90°", Color.Cornsilk, Color.SaddleBrown);
            nuppPooorata.Click += (s, e) => PoooraPilti();

            nuppPeegelda = LooNupp("Peegelda", Color.Cornsilk, Color.SaddleBrown);
            nuppPeegelda.Click += (s, e) => PeegeldaPilti();

            nuppMustValge = LooNupp("Must-valge", Color.Cornsilk, Color.SaddleBrown);
            nuppMustValge.Click += (s, e) => RakendaMustValge();

            nuppSepia = LooNupp("Sepia filter", Color.Cornsilk, Color.SaddleBrown);
            nuppSepia.Click += (s, e) => RakendaSepia();

            nuppReziim = LooNupp("Vaate režiim", Color.Cornsilk, Color.SaddleBrown);
            nuppReziim.Click += (s, e) => MuudaVaateReziimi();

            nuppTühista = LooNupp("Taasta algne", Color.RosyBrown, Color.White);
            nuppTühista.Click += (s, e) => TaastaAlgnePilt();

            nupuPaneel.Controls.AddRange(new Control[] {
                nuppAva, nuppSalvesta, nuppPooorata, nuppPeegelda,
                nuppMustValge, nuppSepia, nuppReziim, nuppTühista
            });

            this.Controls.Add(pildiKast);
            this.Controls.Add(siltInfo);
            this.Controls.Add(nupuPaneel);

            UuendaNuppudeOlekut(false);
        }

        private Button LooNupp(string tekst, Color taust, Color tekstivarv)
        {
            Button nupp = new Button
            {
                Text = tekst,
                Size = new Size(110, 38),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                BackColor = taust,
                ForeColor = tekstivarv,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(3)
            };
            nupp.FlatAppearance.BorderColor = Color.Tan;
            return nupp;
        }

        private void UuendaNuppudeOlekut(bool kasPiltOnAvatud)
        {
            nuppSalvesta.Enabled = kasPiltOnAvatud;
            nuppPooorata.Enabled = kasPiltOnAvatud;
            nuppPeegelda.Enabled = kasPiltOnAvatud;
            nuppMustValge.Enabled = kasPiltOnAvatud;
            nuppSepia.Enabled = kasPiltOnAvatud;
            nuppReziim.Enabled = kasPiltOnAvatud;
            nuppTühista.Enabled = kasPiltOnAvatud;
        }

        private void NuppAva_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Pildifailid|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    originaalPilt = Image.FromFile(dialog.FileName);
                    pildiKast.Image = (Image)originaalPilt.Clone();
                    siltInfo.Text = $"Pilt: {dialog.SafeFileName} ({pildiKast.Image.Width}x{pildiKast.Image.Height} px)";
                    UuendaNuppudeOlekut(true);
                }
            }
        }

        private void NuppSalvesta_Click(object sender, EventArgs e)
        {
            if (pildiKast.Image == null) return;

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PNG Pilt|*.png|JPEG Pilt|*.jpg|BMP Pilt|*.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pildiKast.Image.Save(dialog.FileName);
                    MessageBox.Show("Pilt edukalt salvestatud!", "Salvestamine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void PoooraPilti()
        {
            if (pildiKast.Image == null) return;
            pildiKast.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pildiKast.Refresh();
        }

        private void PeegeldaPilti()
        {
            if (pildiKast.Image == null) return;
            pildiKast.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pildiKast.Refresh();
        }

        private void RakendaMustValge()
        {
            if (pildiKast.Image == null) return;
            Bitmap bmp = new Bitmap(pildiKast.Image);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color p = bmp.GetPixel(x, y);
                    int hall = (int)(p.R * 0.3 + p.G * 0.59 + p.B * 0.11);
                    bmp.SetPixel(x, y, Color.FromArgb(p.A, hall, hall, hall));
                }
            }
            pildiKast.Image = bmp;
        }

        private void RakendaSepia()
        {
            if (pildiKast.Image == null) return;
            Bitmap bmp = new Bitmap(pildiKast.Image);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color p = bmp.GetPixel(x, y);
                    int r = (int)(0.393 * p.R + 0.769 * p.G + 0.189 * p.B);
                    int g = (int)(0.349 * p.R + 0.686 * p.G + 0.168 * p.B);
                    int b = (int)(0.272 * p.R + 0.534 * p.G + 0.131 * p.B);

                    bmp.SetPixel(x, y, Color.FromArgb(p.A, Math.Min(255, r), Math.Min(255, g), Math.Min(255, b)));
                }
            }
            pildiKast.Image = bmp;
        }

        private void MuudaVaateReziimi()
        {
            if (pildiKast.SizeMode == PictureBoxSizeMode.Zoom)
                pildiKast.SizeMode = PictureBoxSizeMode.StretchImage;
            else if (pildiKast.SizeMode == PictureBoxSizeMode.StretchImage)
                pildiKast.SizeMode = PictureBoxSizeMode.CenterImage;
            else
                pildiKast.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void TaastaAlgnePilt()
        {
            if (originaalPilt != null)
            {
                pildiKast.Image = (Image)originaalPilt.Clone();
            }
        }
    }
}