$a = Get-Content .\project.json -raw | ConvertFrom-Json
$a.version = "0.2.*"
$a | ConvertTo-Json  | set-content .\project.json