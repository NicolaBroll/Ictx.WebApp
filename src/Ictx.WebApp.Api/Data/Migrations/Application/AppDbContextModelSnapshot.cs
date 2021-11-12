﻿// <auto-generated />
using System;
using Ictx.WebApp.Infrastructure.Data.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ictx.WebApp.Api.Data.Migrations.Application;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("Relational:MaxIdentifierLength", 128)
            .HasAnnotation("ProductVersion", "5.0.6")
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        modelBuilder.Entity("Ictx.WebApp.Core.Entities.Dipendente", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("CodiceFiscale")
                    .IsRequired()
                    .HasColumnType("char(16)");

                b.Property<string>("Cognome")
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnType("nvarchar(64)");

                b.Property<DateTime>("DataNascita")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("Inserted")
                    .HasColumnType("datetime2");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("bit");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnType("nvarchar(64)");

                b.Property<string>("Sesso")
                    .IsRequired()
                    .HasColumnType("char(1)");

                b.Property<DateTime>("Updated")
                    .HasColumnType("datetime2");

                b.HasKey("Id");

                b.ToTable("Dipendente");
            });
#pragma warning restore 612, 618
    }
}