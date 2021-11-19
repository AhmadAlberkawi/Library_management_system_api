﻿// <auto-generated />
using System;
using Bvs_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bvs_API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bvs.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passwort")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rolle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vorname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Bvs.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Anzahl")
                        .HasColumnType("int");

                    b.Property<string>("Autor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("B_foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isbn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kategorie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Verfuegbar")
                        .HasColumnType("int");

                    b.Property<string>("Verlag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("Bvs.Entities.Borrow", b =>
                {
                    b.Property<int?>("BooksId")
                        .HasColumnType("int");

                    b.Property<int?>("studentsId")
                        .HasColumnType("int");

                    b.HasIndex("BooksId");

                    b.HasIndex("studentsId");

                    b.ToTable("Borrow");
                });

            modelBuilder.Entity("Bvs.Entities.NumberOverview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnzahlAdmin")
                        .HasColumnType("int");

                    b.Property<int>("AnzahlBook")
                        .HasColumnType("int");

                    b.Property<int>("AnzahlBorrow")
                        .HasColumnType("int");

                    b.Property<int>("AnzahlStudent")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NumberOverview");
                });

            modelBuilder.Entity("Bvs.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BibNum")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MatrikelNum")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vorname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Bvs.Entities.Borrow", b =>
                {
                    b.HasOne("Bvs.Entities.Book", "Books")
                        .WithMany()
                        .HasForeignKey("BooksId");

                    b.HasOne("Bvs.Entities.Student", "students")
                        .WithMany()
                        .HasForeignKey("studentsId");

                    b.Navigation("Books");

                    b.Navigation("students");
                });
#pragma warning restore 612, 618
        }
    }
}