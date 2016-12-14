FROM microsoft/aspnetcore:1.0.1

ARG source=publish
COPY $source /app
WORKDIR /app

ENV ConnectionStrings:DBConnection="Data Source=.\mssqlserver2014;Initial Catalog=CityInfo;Integrated Security=True"
ENV ASPNETCORE_URLS http://*:5000
ENV LTTING_UST_REGISTER_TIMEOUT 0
EXPOSE 5000

ENTRYPOINT ["dotnet", "CityInfo.API.dll"]
 




