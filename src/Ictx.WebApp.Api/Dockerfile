#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Ictx.WebApp.Api/Ictx.WebApp.Api.csproj", "src/Ictx.WebApp.Api/"]
COPY ["src/Ictx.WebApp.Infrastructure/Ictx.WebApp.Infrastructure.csproj", "src/Ictx.WebApp.Infrastructure/"]
COPY ["src/Ictx.WebApp.Core/Ictx.WebApp.Core.csproj", "src/Ictx.WebApp.Core/"]
COPY ["src/Ictx.WebApp.Application/Ictx.WebApp.Application.csproj", "src/Ictx.WebApp.Application/"]
RUN dotnet restore "src/Ictx.WebApp.Api/Ictx.WebApp.Api.csproj"
COPY . .
WORKDIR "/src/src/Ictx.WebApp.Api"
RUN dotnet build "Ictx.WebApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ictx.WebApp.Api.csproj" -c Release -o /app/publish

FROM base AS final

# User run app.
RUN adduser \
  --disabled-password \
  --home /app \
  --gecos '' app \
  && chown -R app /app
USER app

WORKDIR /app
COPY --from=publish /app/publish .

# Environment.
ENV DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_URLS=http://+:8080

# Servicew port.
EXPOSE 8080

# Run.
ENTRYPOINT ["dotnet", "Ictx.WebApp.Api.dll"]
