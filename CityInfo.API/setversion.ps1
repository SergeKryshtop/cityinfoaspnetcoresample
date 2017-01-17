[CmdletBinding()]
Param(
	[Parameter(Mandatory=$true, Position=1)]
	$version,
    [Parameter(Mandatory=$false, Position=2)]
	$filePath = ".\src\CityInfo.API\project.json"
)

$a = Get-Content $filePath -raw | ConvertFrom-Json
$a.version = $version
$a | ConvertTo-Json  | set-content $filePath