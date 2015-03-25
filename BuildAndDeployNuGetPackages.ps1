# Define some parameters
$OutputPath = "BuildNugets"
$BuildConfiguration = "Debug"

If (Test-Path $OutputPath){
	Remove-Item $OutputPath -Force -recurse
}
New-Item -Path $OutputPath -ItemType directory

# Find all the *.nuspec files and deliberately omit NuGet packages
$specs = @(Get-ChildItem ".\" -Filter "*.nuspec" -Recurse | `
  ? { $PSItem.FullName -inotmatch "\\packages\\" } | `
  % { $PSItem.FullName } `
)
 
# Find relevant *.csproj files
foreach ($spec in $specs) {
  $folder = $(Split-Path $spec -Parent)
  $prjs = @(Get-ChildItem $folder -Filter "*.csproj" | % { $PSItem.FullName } )
  if ($prjs.Count -gt 0) {
    $prj = @($prjs)[0]
    Start-Process .nuget\NuGet.exe `
    -ArgumentList @("pack", "-IncludeReferencedProjects", "-o ""$OutputPath""", "-Properties ""Configuration=$BuildConfiguration""", "$prj") `
    -Wait
  }
}