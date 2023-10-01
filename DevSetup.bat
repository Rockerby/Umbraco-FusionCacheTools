cd src
REM This tool assumes that Umbraco Templates is already installed on the target machine. Running the below line would overwrite anyone's current installed version, that sounds cheeky...
REM dotnet new install Umbraco.Templates::10.4.0
dotnet new umbraco -n FusionCacheTools.TestSite --friendly-name "Administrator" --email "admin@example.com" --password "1234567890" --development-database-type SQLite
dotnet add FusionCacheTools.TestSite\FusionCacheTools.TestSite.csproj reference FusionCacheTools.BackOffice\FusionCacheTools.BackOffice.csproj

echo "Done"
pause