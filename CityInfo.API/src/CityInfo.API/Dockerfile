FROM microsoft/aspnetcore:1.0
ENTRYPOINT ["dotnet", "CityInfo.API.dll"]
ARG source=.
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
ENV LTTNG_UST_REGISTER_TIMEOUT 0
ENV SQLCONNSTR_DBConnection="Server=tcp:mdlr-sql-01.database.windows.net;Database=CityInfo_QA;User Id=userAdmin;Password=p@ssw0rd!."

EXPOSE 80
VOLUME /logs:/logs

COPY $source .
RUN mkdir logs
