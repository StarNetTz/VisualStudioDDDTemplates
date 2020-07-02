FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app
COPY dist/ .
ENTRYPOINT ["dotnet", "$safeprojectname$.dll"]

