# Builds the TravelGuide Windows installer.
#
#   1. publish the app self-contained (no .NET needed on the target machine)
#   2. compile the Inno Setup script
#
# Result: Installer\Output\TravelGuideSetup.exe

$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$iscc = "C:\Program Files (x86)\Inno Setup 6\ISCC.exe"
if (-not (Test-Path $iscc)) {
    $iscc = "C:\Program Files\Inno Setup 6\ISCC.exe"
}
if (-not (Test-Path $iscc)) {
    throw "Inno Setup 6 not found. Install it from https://jrsoftware.org/isdl.php"
}

Write-Host "Publishing self-contained build..." -ForegroundColor Cyan
Push-Location (Join-Path $root "TravelGuide")
try {
    dotnet publish -c Release -r win-x64 --self-contained true
    if ($LASTEXITCODE -ne 0) { throw "dotnet publish failed" }
}
finally {
    Pop-Location
}

Write-Host "Compiling the installer..." -ForegroundColor Cyan
& $iscc (Join-Path $PSScriptRoot "TravelGuideSetup.iss")
if ($LASTEXITCODE -ne 0) { throw "ISCC failed" }

$output = Join-Path $PSScriptRoot "Output\TravelGuideSetup.exe"
$size = [math]::Round((Get-Item $output).Length / 1MB, 1)
Write-Host "Done: $output ($size MB)" -ForegroundColor Green
