﻿// <auto-generated />
using System;
using DDAC_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDAC_System.Migrations.System_ClassDB
{
    [DbContext(typeof(System_ClassDBContext))]
    partial class System_ClassDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DDAC_System.Models.AcademicClass", b =>
                {
                    b.Property<int>("Class_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ClassEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ClassStartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Class_Lecturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Class_Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Class_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Class_ID");

                    b.ToTable("AcademicClass");
                });



                    b.HasKey("AssignmentID");

                    b.HasIndex("Class_Name");

                    b.HasKey("ViewClass_ID");

            modelBuilder.Entity("DDAC_System.Models.AssignmentSubmission", b =>
                {
                    b.Property<int>("SubmitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssignmentID")
                        .HasColumnType("int");

                    b.Property<string>("SubmitName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubmitID");

                    b.HasIndex("AssignmentID");

                    b.ToTable("AssignmentSubmission");
                });

            modelBuilder.Entity("DDAC_System.Models.Assignment", b =>
                {
                    b.HasOne("DDAC_System.Models.AcademicClass", "AcademicClass")
                        .WithMany("Assignment")
                        .HasForeignKey("Class_Name")
                        .HasPrincipalKey("Class_Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DDAC_System.Models.AssignmentSubmission", b =>
                {
                    b.HasOne("DDAC_System.Models.Assignment", "Assignment")
                        .WithMany("AssignmentSubmission")
                        .HasForeignKey("AssignmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
