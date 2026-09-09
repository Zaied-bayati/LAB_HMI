# Trusts the Fiber Img App .cer on this machine (DEV or operator PC).
# Run in elevated PowerShell.

param(
    [Parameter(Mandatory = $true)]
    [string]$CerPath
)

$ErrorActionPreference = "Stop"
if (-not (Test-Path $CerPath)) {
    throw "Certificate not found: $CerPath"
}

Import-Certificate -FilePath $CerPath -CertStoreLocation "Cert:\LocalMachine\TrustedPeople" | Out-Null
Write-Host "Imported to LocalMachine\TrustedPeople: $CerPath"
Write-Host "If sideload still fails, also import to LocalMachine\Root (lab machines only)."
