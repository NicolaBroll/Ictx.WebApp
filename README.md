# Ictx.WebApp

# Migrations
AppDbContext:
dotnet ef migrations add InitialAppDbContextMigration -c AppDbContext -o Data/Migrations/Application

dotnet ef migrations script | out-file ./script.sql
