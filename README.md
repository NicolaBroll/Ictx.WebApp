[![Continuous integration and deployment](https://github.com/NicolaBroll/Ictx.WebApp/actions/workflows/ci-dc.yaml/badge.svg)](https://github.com/NicolaBroll/Ictx.WebApp/actions/workflows/ci-dc.yaml)

# Ictx.WebApp

# Migrations
AppDbContext:
dotnet ef migrations add InitialAppDbContextMigration -c AppDbContext -o Data/Migrations/Application

dotnet ef migrations script | out-file ./script.sql


# Docker
docker build -t ictx.webapp.api:v1.0.0 -f ./Dockerfile .
docker run -p 80:80 --env ASPNETCORE_ENVIRONMENT=Development -d ictx.webapp.api:v1.0.0