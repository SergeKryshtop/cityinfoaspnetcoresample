dotnet restore
pushd .\src\CityInfo.Api\

dotnet publish -o %~dp0\publish\ -c Release

popd  
