using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class MathQuizForm : Form
    {
        private Label siltAeg;
        private Button nuppAlusta;
        private Timer manguTaimer;
        private Random juhuslikArv = new Random();

        
        private Label siltLiitmineVasak, siltLiitmineMark, siltLiitmineParem, siltLiitmineVordus;
        private NumericUpDown sisendLiitmine;
        private int arvLiitmine1, arvLiitmine2;

       
        private Label siltLahutamineVasak, siltLahutamineMark, siltLahutamineParem, siltLahutamineVordus;
        private NumericUpDown sisendLahutamine;
        private int arvLahutamine1, arvLahutamine2;

        
        private Label siltKorrutamineVasak, siltKorrutamineMark, siltKorrutamineParem, siltKorrutamineVordus;
        private NumericUpDown sisendKorrutamine;
        private int arvKorrutamine1, arvKorrutamine2;

        
        private Label siltJagamineVasak, siltJagamineMark, siltJagamineParem, siltJagamineVordus;
        private NumericUpDown sisendJagamine;
        private int arvJagamine1, arvJagamine2;

        private int aegaJaanud;

        public MathQuizForm()
        {
           
            this.Text = "Matemaatiline mäng";
            this.Size = new Size(480, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Font peamineKirjastiil = new Font("Arial", 18, FontStyle.Regular);

       
            siltAeg = new Label
            {
                Text = "Aega jäänud: 60 sekundit",
                Location = new Point(110, 15),
                Size = new Size(340, 35),
                Font = new Font("Arial", 14, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter
            };

          
            siltLiitmineVasak = LooSilt("?", 50, 60, peamineKirjastiil);
            siltLiitmineMark = LooSilt("+", 130, 60, peamineKirjastiil);
            siltLiitmineParem = LooSilt("?", 180, 60, peamineKirjastiil);
            siltLiitmineVordus = LooSilt("=", 250, 60, peamineKirjastiil);
            sisendLiitmine = LooSisendVali(320, 60, peamineKirjastiil);

            siltLahutamineVasak = LooSilt("?", 50, 120, peamineKirjastiil);
            siltLahutamineMark = LooSilt("-", 130, 120, peamineKirjastiil);
            siltLahutamineParem = LooSilt("?", 180, 120, peamineKirjastiil);
            siltLahutamineVordus = LooSilt("=", 250, 120, peamineKirjastiil);
            sisendLahutamine = LooSisendVali(320, 120, peamineKirjastiil);

            siltKorrutamineVasak = LooSilt("?", 50, 180, peamineKirjastiil);
            siltKorrutamineMark = LooSilt("×", 130, 180, peamineKirjastiil);
            siltKorrutamineParem = LooSilt("?", 180, 180, peamineKirjastiil);
            siltKorrutamineVordus = LooSilt("=", 250, 180, peamineKirjastiil);
            sisendKorrutamine = LooSisendVali(320, 180, peamineKirjastiil);

            siltJagamineVasak = LooSilt("?", 50, 240, peamineKirjastiil);
            siltJagamineMark = LooSilt("÷", 130, 240, peamineKirjastiil);
            siltJagamineParem = LooSilt("?", 180, 240, peamineKirjastiil);
            siltJagamineVordus = LooSilt("=", 250, 240, peamineKirjastiil);
            sisendJagamine = LooSisendVali(320, 240, peamineKirjastiil);

          
            nuppAlusta = new Button
            {
                Text = "Alusta mängu",
                Location = new Point(150, 305),
                Size = new Size(160, 40),
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true
            };
            nuppAlusta.Click += NuppAlusta_Click;

       
            manguTaimer = new Timer { Interval = 1000 };
            manguTaimer.Tick += ManguTaimer_Tick;

        
            this.Controls.AddRange(new Control[] {
                siltAeg, nuppAlusta,
                siltLiitmineVasak, siltLiitmineMark, siltLiitmineParem, siltLiitmineVordus, sisendLiitmine,
                siltLahutamineVasak, siltLahutamineMark, siltLahutamineParem, siltLahutamineVordus, sisendLahutamine,
                siltKorrutamineVasak, siltKorrutamineMark, siltKorrutamineParem, siltKorrutamineVordus, sisendKorrutamine,
                siltJagamineVasak, siltJagamineMark, siltJagamineParem, siltJagamineVordus, sisendJagamine
            });
        }

     
        private Label LooSilt(string tekst, int x, int y, Font kirjastiil)
        {
            return new Label
            {
                Text = tekst,
                Location = new Point(x, y),
                Size = new Size(60, 40),
                Font = kirjastiil,
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

  
        private NumericUpDown LooSisendVali(int x, int y, Font kirjastiil)
        {
            return new NumericUpDown
            {
                Location = new Point(x, y),
                Width = 100,  
                Size = new Size(100, 35), 
                Font = kirjastiil,
                Maximum = 10000,
                Value = 0
            };
        }

      
        private void AlustaMangu()
        {
        
            arvLiitmine1 = juhuslikArv.Next(1, 51);
            arvLiitmine2 = juhuslikArv.Next(1, 51);
            siltLiitmineVasak.Text = arvLiitmine1.ToString();
            siltLiitmineParem.Text = arvLiitmine2.ToString();
            sisendLiitmine.Value = 0;

            arvLahutamine1 = juhuslikArv.Next(1, 51);
            arvLahutamine2 = juhuslikArv.Next(1, arvLahutamine1); 
            siltLahutamineVasak.Text = arvLahutamine1.ToString();
            siltLahutamineParem.Text = arvLahutamine2.ToString();
            sisendLahutamine.Value = 0;

            arvKorrutamine1 = juhuslikArv.Next(2, 11);
            arvKorrutamine2 = juhuslikArv.Next(2, 11);
            siltKorrutamineVasak.Text = arvKorrutamine1.ToString();
            siltKorrutamineParem.Text = arvKorrutamine2.ToString();
            sisendKorrutamine.Value = 0;

            arvJagamine2 = juhuslikArv.Next(2, 11); 
            int ajutineVastus = juhuslikArv.Next(2, 11); 
            arvJagamine1 = arvJagamine2 * ajutineVastus; 
            siltJagamineVasak.Text = arvJagamine1.ToString();
            siltJagamineParem.Text = arvJagamine2.ToString();
            sisendJagamine.Value = 0;

           
            aegaJaanud = 60;
            siltAeg.Text = "Aega jäänud: 60 sekundit";
            nuppAlusta.Enabled = false;
            manguTaimer.Start();
        }

       
        private bool KontrolliVastuseid()
        {
            return (sisendLiitmine.Value == arvLiitmine1 + arvLiitmine2) &&
                   (sisendLahutamine.Value == arvLahutamine1 - arvLahutamine2) &&
                   (sisendKorrutamine.Value == arvKorrutamine1 * arvKorrutamine2) &&
                   (sisendJagamine.Value == arvJagamine1 / arvJagamine2);
        }

        private void NuppAlusta_Click(object sender, EventArgs e)
        {
            AlustaMangu();
        }

        private void ManguTaimer_Tick(object sender, EventArgs e)
        {
            if (KontrolliVastuseid())
            {
                manguTaimer.Stop();
                MessageBox.Show("Sain kõik vastused õigesti!", "Palju õnne!");
                nuppAlusta.Enabled = true;
            }
            else if (aegaJaanud > 0)
            {
                aegaJaanud--;
                siltAeg.Text = $"Aega jäänud: {aegaJaanud} sekundit";
            }
            else
            {
                manguTaimer.Stop();
                siltAeg.Text = "Aeg sai otsa!";
                MessageBox.Show("Aeg sai otsa! Proovi uuesti.", "Kaotus");

        
                sisendLiitmine.Value = arvLiitmine1 + arvLiitmine2;
                sisendLahutamine.Value = arvLahutamine1 - arvLahutamine2;
                sisendKorrutamine.Value = arvKorrutamine1 * arvKorrutamine2;
                sisendJagamine.Value = arvJagamine1 / arvJagamine2;

                nuppAlusta.Enabled = true;
            }
        }
    }
}