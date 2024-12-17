using System;
using System.Windows.Forms;

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
            this.MinimizeBox = true;                                // Minimieren bleibt erlaubt
            this.TopMost = true;                                    // Fenster immer im Vordergrund halten

            // Window Text
            Label lblInfo = new Label();
            lblInfo.Text = "Hier könnte Ihre Werbung stehen!";
            lblInfo.AutoSize = true;
            lblInfo.Font = new System.Drawing.Font("Arial", 12);
            lblInfo.Location = new System.Drawing.Point(50, 50);
            this.Controls.Add(lblInfo);

            // Button 
            Button btnInfo = new Button();
            btnInfo.Text = "Click me!";
            btnInfo.Location = new System.Drawing.Point(50, 100);
            btnInfo.Click += new EventHandler(BtnInfo_Click);
            this.Controls.Add(btnInfo);
        }

        // Button-Click-Event
        private void BtnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Moin Meister", "WARNING");
        }

        // Avoids closure
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true; // Blocks closure
            MessageBox.Show("You can not close this window!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }
    }
}