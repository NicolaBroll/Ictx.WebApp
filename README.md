[![Continuous integration and deployment](https://github.com/NicolaBroll/Ictx.WebApp/actions/workflows/ci-dc.yaml/badge.svg)](https://github.com/NicolaBroll/Ictx.WebApp/actions/workflows/ci-dc.yaml)

# Ictx.WebApp

# Migrations
AppDbContext:
dotnet ef migrations add InitialAppDbContextMigration -c AppDbContext -o Data/Migrations/Application

dotnet ef migrations script | out-file ./script.sql
