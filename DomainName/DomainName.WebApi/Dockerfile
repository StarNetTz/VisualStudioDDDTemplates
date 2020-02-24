FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
COPY dist/ .
ENTRYPOINT ["dotnet", "DomainName.WebApi.dll"]
