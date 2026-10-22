param(
    [Parameter(Mandatory)][string]$PackageFolder,
    [Parameter(Mandatory)][string]$Version
)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.IO.Compression.FileSystem
$expected = @('DWIS.API.DTO', 'DWIS.OPCUA.Configuration', 'DWIS.OPCUA.Vocabulary',
    'DWIS.SPARQL.Utils', 'DWIS.Vocabulary.ResourceIntegration', 'DWIS.OPCUA.Schemas')
$packages = @(Get-ChildItem -LiteralPath $PackageFolder -Filter '*.nupkg')
if ($packages.Count -ne $expected.Count) { throw "Expected six packages, found $($packages.Count)" }
foreach ($id in $expected) {
    $archive = [IO.Compression.ZipFile]::OpenRead((Join-Path $PackageFolder "$id.$Version.nupkg"))
    try {
        $entry = @($archive.Entries | Where-Object FullName -like '*.nuspec')
        if ($entry.Count -ne 1) { throw "Expected one nuspec in $id" }
        $reader = [IO.StreamReader]::new($entry[0].Open())
        try { [xml]$manifest = $reader.ReadToEnd() } finally { $reader.Dispose() }
        $metadata = $manifest.package.metadata
        if ($metadata.id -ne $id -or $metadata.version -ne $Version) { throw "Unexpected identity/version in $id" }
        foreach ($dependency in $metadata.SelectNodes('.//*[local-name()="dependency"]')) {
            $required = if ($dependency.id -in $expected) { $Version }
                elseif ($dependency.id -like 'DWIS.Vocabulary.*') { '1.0.176' }
                else { continue }
            if ($dependency.version -notin @($required, "[$required, )", "[$required]")) {
                throw "$id has unexpected dependency $($dependency.id) $($dependency.version); expected $required"
            }
        }
        Write-Output "Verified $id $Version"
    } finally { $archive.Dispose() }
}
