cd src
REM This tool assumes that Umbraco Templates is already installed on the target machine. Running the below line would overwrite anyone's current installed version, that sounds cheeky...
REM dotnet new install Umbraco.Templates::10.4.0
dotnet new umbraco -n FusionCacheTools.TestSite
dotnet add FusionCacheTools.TestSite\FusionCacheTools.TestSite.csproj reference FusionCacheTools\FusionCacheTools.csproj

echo "Done"
pause