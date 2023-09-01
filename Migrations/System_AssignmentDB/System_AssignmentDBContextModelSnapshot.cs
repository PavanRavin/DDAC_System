﻿// <auto-generated />
using System;
using DDAC_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDAC_System.Migrations.System_AssignmentDB
{
    [DbContext(typeof(System_AssignmentDBContext))]
    partial class System_AssignmentDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("DDAC_System.Models.ViewAssignment", b =>
                {
                    b.Property<int>("ViewAssignmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ViewAssignmentDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ViewAssignmentDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ViewAssignmentHandOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("ViewAssignmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ViewAssignmentID");

                    b.ToTable("ViewAssignment");
                });
#pragma warning restore 612, 618
        }
    }
}