FROM microsoft/dotnet:1.0.0-preview2-sdk
RUN mkdir /app
COPY . /app

WORKDIR /app/src/CityInfo.DAL
RUN ["dotnet", "restore"]

WORKDIR /app/src/CityInfo.API
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "run"]

