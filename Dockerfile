#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Ictx.WebApp.Api/Ictx.WebApp.Api.csproj", "src/Ictx.WebApp.Api/"]
COPY ["src/Ictx.WebApp.Infrastructure/Ictx.WebApp.Infrastructure.csproj", "src/Ictx.WebApp.Infrastructure/"]
COPY ["src/Ictx.WebApp.Core/Ictx.WebApp.Core.csproj", "src/Ictx.WebApp.Core/"]
COPY ["src/Ictx.WebApp.Application/Ictx.WebApp.Application.csproj", "src/Ictx.WebApp.Application/"]
COPY ["src/Ictx.WebApp.Templates.Mail/Ictx.WebApp.Templates.Mail.csproj", "src/Ictx.WebApp.Templates.Mail/"]
RUN dotnet restore "src/Ictx.WebApp.Api/Ictx.WebApp.Api.csproj"
COPY . .
WORKDIR "/src/src/Ictx.WebApp.Api"
RUN dotnet build "Ictx.WebApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ictx.WebApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ictx.WebApp.Api.dll"]