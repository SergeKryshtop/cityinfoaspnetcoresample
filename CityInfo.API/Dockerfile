FROM microsoft/aspnetcore:1.0.3

ARG source=publish
COPY $source /app

WORKDIR /app

ENV SQLCONNSTR_DBConnection="Data Source=.\mssqlserver2014;Initial Catalog=CityInfo;Integrated Security=True"
ENV ASPNETCORE_URLS http://*:5000
ENV LTTNG_UST_REGISTER_TIMEOUT 0
EXPOSE 5000

RUN mkdir logs

ENTRYPOINT ["dotnet", "CityInfo.API.dll"]
 




