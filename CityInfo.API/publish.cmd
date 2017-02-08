if exist %~dp0\publish del %~dp0\publish /y

dotnet restore
pushd .\src\CityInfo.Api\

dotnet publish -o %~dp0\publish\ -c Release

popd  
