FROM microsoft/aspnetcore:1.0.1
ENTRYPOINT ["dotnet", "CityInfo.API.dll"]
ARG source=.
WORKDIR /app
EXPOSE 80
VOLUME /logs:/logs
COPY $source .
