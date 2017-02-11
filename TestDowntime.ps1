[CmdletBinding()]
Param(
    #Enter endpoint url
    [Parameter(Mandatory=$True)]
    [string]$EndpointUri

)


$continue = $true
while($continue)
{

    if ($Host.UI.RawUI.KeyAvailable)
    {
        echo "Toggle with Esc";
        $x =  $Host.UI.RawUI.ReadKey('NoEcho, IncludeKeyDown')

        switch ( $x.key)
        {
            Esc { $continue = $false }
        }
    } 
    else
    {
        $response = Invoke-RestMethod -Uri ($EndpointUri + "/api/info")
        $date = (Get-Date).TimeOfDay;

        Write-Host "[$date] Version: $($response.Version)"
        
        sleep 0.5
               
        
    }    
}
 
