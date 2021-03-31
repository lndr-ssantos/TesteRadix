FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY ./Events.API/ ./
RUN dotnet publish -c Release
ENTRYPOINT ["dotnet", "bin/Release/net5.0/publish/Events.API.dll"]