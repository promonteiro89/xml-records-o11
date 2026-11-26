# Runs the XmlRecords test suite on Windows with the .NET Framework 4.8 runtime, the same runtime
# OutSystems 11 uses. run-tests.sh is the macOS/Linux (mono) equivalent.
#
# Two targets (environment variable TARGET or -Target):
#   baseline (default)  tests\lib\baseline\OutSystems.NssXmlRecords.dll, the published extension, unchanged
#   source              the extension built from XmlRecords\Source\NET
#
# Needs: Visual Studio or its Build Tools (for Roslyn csc) and the .NET Framework 4.8 targeting pack.
# Optional environment variables (defaults in brackets):
#   EXT_SRC          extension source folder for TARGET=source [..\XmlRecords\Source\NET]
#   BASELINE_DLL     extension DLL for TARGET=baseline [tests\lib\baseline\OutSystems.NssXmlRecords.dll]
#   OS_PLATFORM_BIN  folder with the OutSystems platform assemblies [tests\lib if they are there,
#                    else ..\XmlRecords\Source\NET\Bin, where Integration Studio puts them]
#   NET48_API        .NET Framework 4.8 reference assemblies [Program Files (x86)\Reference Assemblies\...\v4.8]
#
# Usage: tests\run-tests.ps1 [-Target baseline|source] [suite-filter]
#        exit code 0 = no unexpected result, 1 = failures, 2 = build error
param([string]$Suite = "", [string]$Target = "")
$ErrorActionPreference = "Stop"

function Default([string]$value, [string]$fallback) { if ($value) { $value } else { $fallback } }

$here = $PSScriptRoot
$target = (Default $Target (Default $env:TARGET "baseline")).ToLowerInvariant()
$extSrc = Default $env:EXT_SRC (Join-Path $here "..\XmlRecords\Source\NET")
$baseline = Default $env:BASELINE_DLL (Join-Path $here "lib\baseline\OutSystems.NssXmlRecords.dll")
$platformDefault = Join-Path $here "lib"
if (-not (Test-Path (Join-Path $platformDefault "OutSystems.HubEdition.RuntimePlatform.dll"))) {
    $platformDefault = Join-Path $here "..\XmlRecords\Source\NET\Bin"
}
$platform = Default $env:OS_PLATFORM_BIN $platformDefault
$api = Default $env:NET48_API (Join-Path ${env:ProgramFiles(x86)} "Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8")
$out = Join-Path $here ".build"

function Fail([string]$message) { Write-Host $message; exit 2 }

if ($target -ne "baseline" -and $target -ne "source") { Fail "TARGET must be baseline or source, not '$target'" }
if (-not (Test-Path $api)) { Fail "missing .NET Framework 4.8 reference assemblies at $api (set NET48_API)" }
foreach ($f in "OutSystems.HubEdition.RuntimePlatform.dll", "OutSystems.RuntimeCommon.dll", "OutSystems.HubEdition.DatabaseAbstractionLayer.dll") {
    if (-not (Test-Path (Join-Path $platform $f))) { Fail "missing $platform\$f (set OS_PLATFORM_BIN)" }
}

# Roslyn csc from the newest Visual Studio / Build Tools installation
$vswhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"
if (-not (Test-Path $vswhere)) { Fail "vswhere.exe not found: install Visual Studio or its Build Tools" }
$vs = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
$csc = Join-Path $vs "MSBuild\Current\Bin\Roslyn\csc.exe"
if (-not (Test-Path $csc)) { Fail "Roslyn csc not found at $csc" }

if (Test-Path $out) { Remove-Item $out -Recurse -Force }
New-Item -ItemType Directory -Path $out | Out-Null
Copy-Item (Join-Path $platform "OutSystems.*.dll") $out

$fw = "mscorlib.dll", "System.dll", "System.Core.dll", "System.Data.dll", "System.Xml.dll", "System.Xml.Linq.dll",
      "System.Runtime.Serialization.dll", "Facades\netstandard.dll" |
      ForEach-Object { "-r:" + (Join-Path $api $_) }
$os = "OutSystems.HubEdition.RuntimePlatform.dll", "OutSystems.RuntimeCommon.dll",
      "OutSystems.HubEdition.DatabaseAbstractionLayer.dll" | ForEach-Object { "-r:" + (Join-Path $out $_) }

$extDll = Join-Path $out "OutSystems.NssXmlRecords.dll"
if ($target -eq "baseline") {
    if (-not (Test-Path $baseline)) { Fail "missing $baseline (set BASELINE_DLL)" }
    Copy-Item $baseline $extDll
}
else {
    # the extension: the source files Integration Studio compiles (not unittests\, which needs NUnit)
    $extFiles = Get-ChildItem -Path $extSrc -Filter *.cs -File | Where-Object { $_.Name -notlike "%Name%*" } | ForEach-Object { $_.FullName }
    & $csc -nologo -nowarn:1591,0168,0219,0162 -langversion:7.3 -target:library -nostdlib -noconfig "-out:$extDll" @fw @os @extFiles > (Join-Path $out "build-ext.log")
    if ($LASTEXITCODE -ne 0) { Get-Content (Join-Path $out "build-ext.log") | Where-Object { $_ -notmatch "warning" }; Fail "extension build failed" }
}

# the tests: C# 5 only, so they also compile under Windows PowerShell 5.1 Add-Type
$testFiles = Get-ChildItem -Path $here -Filter *.cs -File | ForEach-Object { $_.FullName }
$testExe = Join-Path $out "XmlRecords.Tests.exe"
& $csc -nologo -langversion:5 -target:exe -nostdlib -noconfig "-out:$testExe" @fw @os "-r:$extDll" @testFiles > (Join-Path $out "build-tests.log")
if ($LASTEXITCODE -ne 0) { Get-Content (Join-Path $out "build-tests.log") | Where-Object { $_ -notmatch "warning" }; Fail "test build failed" }

# the test program pins the culture to en-US; .NET Framework ignores TZ, and the checks with a time
# zone compute their expected value from the machine's zone
$env:XR_TARGET = $target
Push-Location $out
try {
    $arguments = @()
    if ($Suite) { $arguments += $Suite }
    & $testExe @arguments | Tee-Object -FilePath (Join-Path $out "test-output.txt")
    $code = $LASTEXITCODE
}
finally { Pop-Location }
exit $code
