# Creates a self-signed MSIX code-signing certificate (CN=TTU).
# Run in elevated PowerShell on the DEV PC.

param(
    [string]$OutputDir = "$PSScriptRoot\..\certs",
    [string]$Password = $(Read-Host "Enter PFX password" -AsSecureString | ForEach-Object {
        $bstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($_)
        [Runtime.InteropServices.Marshal]::PtrToStringBSTR($bstr)
    })
)

$ErrorActionPreference = "Stop"
New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null

$cert = New-SelfSignedCertificate `
    -Type Custom `
    -Subject "CN=TTU" `
    -KeyUsage DigitalSignature `
    -FriendlyName "Fiber Img App MSIX" `
    -CertStoreLocation "Cert:\CurrentUser\My" `
    -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3", "2.5.29.19={text}")

$pfxPath = Join-Path $OutputDir "FiberImgApp.pfx"
$cerPath = Join-Path $OutputDir "FiberImgApp.cer"
$secure = ConvertTo-SecureString -String $Password -Force -AsPlainText

Export-PfxCertificate -Cert $cert -FilePath $pfxPath -Password $secure | Out-Null
Export-Certificate -Cert $cert -FilePath $cerPath | Out-Null

Write-Host "Created:"
Write-Host "  $pfxPath  (keep private; use for signing / GitHub secret)"
Write-Host "  $cerPath  (install on DEV + operator PCs)"
Write-Host ""
Write-Host "Base64 for GitHub Actions secret MSIX_CERT_BASE64:"
[Convert]::ToBase64String([IO.File]::ReadAllBytes($pfxPath))
