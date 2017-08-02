 <# Get current directory path #>
$src = (Get-Item -Path ".\" -Verbose).FullName;

<# Iterate all directories present in the current directory path #>
Get-ChildItem $src -directory | where {$_.PsIsContainer} | Select-Object -Property Name | ForEach-Object {
    $cdProjectDir = [string]::Format("cd /d {0}\{1}",$src, $_.Name);

    <# Get project's startup file path #>    
    $projectDir = [string]::Format("{0}\{1}\Startup.cs",$src, $_.Name); 
    $fileExists = Test-Path $projectDir;
    $isAuthDir = $_.Name -eq "Authentication"
    
    <# Check project has startup file and isnt authentication dir #>
    if($fileExists -eq $true -and -not $isAuthDir){

        <# Start cmd process and execute 'dotnet build && dotnet run' #>
        $params=@("/C"; $cdProjectDir; " && dotnet build && dotnet run"; )
        Start-Process -Verb runas "cmd.exe" $params;
    }
} 