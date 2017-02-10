FROM microsoft/aspnetcore:1.0
ENTRYPOINT ["dotnet", "CityInfo.API.dll"]
ARG source=.
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
ENV LTTNG_UST_REGISTER_TIMEOUT 0
EXPOSE 80
VOLUME /logs:/logs
COPY $source .
