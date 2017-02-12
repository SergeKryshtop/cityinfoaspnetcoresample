[CmdletBinding()]
Param(
    #Enter endpoint url
    [Parameter(Mandatory=$True)]
    [string]$EndpointUri

)

echo "Press Esc to exit..."

$continue = $true
$Host.UI.RawUI.FlushInputBuffer();

while($continue)
{

    if ($Host.UI.RawUI.KeyAvailable)
    {
        $x =  $Host.UI.RawUI.ReadKey('NoEcho, IncludeKeyUp')
        switch ($x.VirtualKeyCode)
        {
            27 { $continue = $false }
        }
    } 
    else
    {
        $response = Invoke-RestMethod -Uri ($EndpointUri + "/api/info")
        $date = (Get-Date).TimeOfDay;

        Write-Host "[$date] Version: $($response.Version)`tBuild Version: $($response.InformationVersion)"
        
        Start-Sleep -MilliSeconds 500 
               
        
    }    
}
 
