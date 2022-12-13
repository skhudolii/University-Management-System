﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using University.Infrastructure.Data;

#nullable disable

namespace University.Infrastructure.Data.Migrations
{
    [DbContext(typeof(UniversityDbContext))]
    partial class UniversityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("University.Core.Entities.AcademicEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AcademicPosition")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("AcademicEmployees");
                });

            modelBuilder.Entity("University.Core.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("University.Core.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("University.Core.Entities.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("LectureDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LectureRoomId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("SheduleId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LectureRoomId");

                    b.HasIndex("SheduleId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("University.Core.Entities.LectureGroup", b =>
                {
                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("LectureId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("LecturesGroups");
                });

            modelBuilder.Entity("University.Core.Entities.LectureRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("LectureRooms");
                });

            modelBuilder.Entity("University.Core.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("University.Core.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("University.Core.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("University.Core.Entities.SubjectAcademicEmployee", b =>
                {
                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("AcademicEmployeeId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId", "AcademicEmployeeId");

                    b.HasIndex("AcademicEmployeeId");

                    b.ToTable("SubjectsAcademicEmployees");
                });

            modelBuilder.Entity("University.Core.Entities.SubjectGroup", b =>
                {
                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("SubjectsGroups");
                });

            modelBuilder.Entity("University.Core.Entities.AcademicEmployee", b =>
                {
                    b.HasOne("University.Core.Entities.Faculty", "Faculty")
                        .WithMany("AcademicEmployees")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("University.Core.Entities.Group", b =>
                {
                    b.HasOne("University.Core.Entities.Faculty", "Faculty")
                        .WithMany("Groups")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("University.Core.Entities.Lecture", b =>
                {
                    b.HasOne("University.Core.Entities.LectureRoom", "LectureRoom")
                        .WithMany()
                        .HasForeignKey("LectureRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University.Core.Entities.Schedule", "Schedule")
                        .WithMany("Lectures")
                        .HasForeignKey("SheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University.Core.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University.Core.Entities.AcademicEmployee", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LectureRoom");

                    b.Navigation("Schedule");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("University.Core.Entities.LectureGroup", b =>
                {
                    b.HasOne("University.Core.Entities.Group", "Group")
                        .WithMany("LecturesGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University.Core.Entities.Lecture", "Lecture")
                        .WithMany("LecturesGroups")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("University.Core.Entities.LectureRoom", b =>
                {
                    b.HasOne("University.Core.Entities.Faculty", "Faculty")
                        .WithMany("LectureRooms")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("University.Core.Entities.Schedule", b =>
                {
                    b.HasOne("University.Core.Entities.Faculty", "Faculty")
                        .WithMany("Schedules")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("University.Core.Entities.Student", b =>
                {
                    b.HasOne("University.Core.Entities.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("University.Core.Entities.Subject", b =>
                {
                    b.HasOne("University.Core.Entities.Faculty", "Faculty")
                        .WithMany("Subjects")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("University.Core.Entities.SubjectAcademicEmployee", b =>
                {
                    b.HasOne("University.Core.Entities.AcademicEmployee", "AcademicEmployee")
                        .WithMany("SubjectsAcademicEmployees")
                        .HasForeignKey("AcademicEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University.Core.Entities.Subject", "Subject")
                        .WithMany("SubjectsAcademicEmployees")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicEmployee");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("University.Core.Entities.SubjectGroup", b =>
                {
                    b.HasOne("University.Core.Entities.Group", "Group")
                        .WithMany("SubjectsGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University.Core.Entities.Subject", "Subject")
                        .WithMany("SubjectsGroups")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("University.Core.Entities.AcademicEmployee", b =>
                {
                    b.Navigation("SubjectsAcademicEmployees");
                });

            modelBuilder.Entity("University.Core.Entities.Faculty", b =>
                {
                    b.Navigation("AcademicEmployees");

                    b.Navigation("Groups");

                    b.Navigation("LectureRooms");

                    b.Navigation("Schedules");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("University.Core.Entities.Group", b =>
                {
                    b.Navigation("LecturesGroups");

                    b.Navigation("Students");

                    b.Navigation("SubjectsGroups");
                });

            modelBuilder.Entity("University.Core.Entities.Lecture", b =>
                {
                    b.Navigation("LecturesGroups");
                });

            modelBuilder.Entity("University.Core.Entities.Schedule", b =>
                {
                    b.Navigation("Lectures");
                });

            modelBuilder.Entity("University.Core.Entities.Subject", b =>
                {
                    b.Navigation("SubjectsAcademicEmployees");

                    b.Navigation("SubjectsGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
