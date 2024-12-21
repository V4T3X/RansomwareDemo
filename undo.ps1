# Reset Backround
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
$windowsPath = [System.Environment]::GetFolderPath("Windows")
$wallpaperPath = Join-Path -Path $windowsPath -ChildPath "Web\Wallpaper\Windows\img0.jpg"
[Wallpaper]::SetWallpaper($wallpaperPath)

Remove-Item -Path "$env:TEMP\black.jpg" -Force

# Stop Decrypter
Stop-Process -Name "Decrypter" -Force
Start-Sleep 2

# Delete Decrypter
Remove-Item -Path "$HOME\Desktop\Decrypter.exe" -Force

# Undo Name Changes
#Get-ChildItem $HOME\Desktop\* | Rename-Item -NewName { $_.name.substring(0,$_.name.length-7) }




