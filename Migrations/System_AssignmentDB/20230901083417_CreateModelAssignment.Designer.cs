﻿// <auto-generated />
using System;
using DDAC_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDAC_System.Migrations.System_AssignmentDB
{
    [DbContext(typeof(System_AssignmentDBContext))]
    [Migration("20230901083417_CreateModelAssignment")]
    partial class CreateModelAssignment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DDAC_System.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignmentDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AssignmentDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AssignmentHandOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("AssignmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AssignmentID");

                    b.ToTable("Assignment");
                });
#pragma warning restore 612, 618
        }
    }
}
