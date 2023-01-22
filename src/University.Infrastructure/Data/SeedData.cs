using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using University.Core.Entities;
using University.Core.Enums;

namespace University.Infrastructure.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            UniversityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<UniversityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            // Faculties
            if (!context.Faculties.Any())
            {
                context.Faculties.AddRange(
                    new Faculty { Name = "C#/.NET", Logo = "images/logos/logo-1.png" },
                    new Faculty { Name = "Java Spring", Logo = "images/logos/logo-2.png" },
                    new Faculty { Name = "Android", Logo = "images/logos/logo-3.png" },
                    new Faculty { Name = "Python", Logo = "images/logos/logo-4.png" },
                    new Faculty { Name = "Node.js", Logo = "images/logos/logo-5.png" },
                    new Faculty { Name = "QA Automation", Logo = "images/logos/logo-6.png" }
                    );
                context.SaveChanges();
            }
            // Groups
            if (!context.Groups.Any())
            {
                context.Groups.AddRange(
                    new Group { Name = "Group 1", FacultyId = 1 },
                    new Group { Name = "Group 2", FacultyId = 1 },
                    new Group { Name = "Group 3", FacultyId = 1 },
                    new Group { Name = "Group 4", FacultyId = 2 },
                    new Group { Name = "Group 5", FacultyId = 2 },
                    new Group { Name = "Group 6", FacultyId = 2 },
                    new Group { Name = "Group 7", FacultyId = 3 },
                    new Group { Name = "Group 8", FacultyId = 3 },
                    new Group { Name = "Group 9", FacultyId = 4 },
                    new Group { Name = "Group 10", FacultyId = 4 },
                    new Group { Name = "Group 11", FacultyId = 5 },
                    new Group { Name = "Group 12", FacultyId = 5 },
                    new Group { Name = "Group 13", FacultyId = 6 },
                    new Group { Name = "Group 14", FacultyId = 6 }
                    );
                context.SaveChanges();
            }
            // AcademicEmployees
            if (!context.AcademicEmployees.Any())
            {
                context.AcademicEmployees.AddRange(
                    new AcademicEmployee
                    {
                        FullName = "Dmytro Lezhen",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.Prof,
                        FacultyId = 1
                    },
                    new AcademicEmployee
                    {
                        FullName = "Mykola Kuzub",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.Prof,
                        FacultyId = 1
                    },
                    new AcademicEmployee
                    {
                        FullName = "Andriy Vynnychuk",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.AssocProf,
                        FacultyId = 1
                    },
                    new AcademicEmployee
                    {
                        FullName = "Ievgenii Karpenko",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.SrRsch,
                        FacultyId = 1
                    },
                    new AcademicEmployee
                    {
                        FullName = "Anton Gusev",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.Prof,
                        FacultyId = 2
                    },
                    new AcademicEmployee
                    {
                        FullName = "Viktor Gogilchyn",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.AssocProf,
                        FacultyId = 2
                    },
                    new AcademicEmployee
                    {
                        FullName = "Tetiana Iefimenko",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/2922/2922561.png",
                        AcademicPosition = AcademicPosition.SrRsch,
                        FacultyId = 2
                    },
                    new AcademicEmployee
                    {
                        FullName = "Boryslav Kolomiiets",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.AsstProf,
                        FacultyId = 2
                    },
                    new AcademicEmployee
                    {
                        FullName = "Dan Gladshtein",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.Prof,
                        FacultyId = 3
                    },
                    new AcademicEmployee
                    {
                        FullName = "Yaroslav Rudnyk",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.AssocProf,
                        FacultyId = 3
                    },
                    new AcademicEmployee
                    {
                        FullName = "Natalia Kulbaka",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/2922/2922561.png",
                        AcademicPosition = AcademicPosition.AsstProf,
                        FacultyId = 3
                    },
                    new AcademicEmployee
                    {
                        FullName = "Serhii Tytarenko",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.Prof,
                        FacultyId = 4
                    },
                    new AcademicEmployee
                    {
                        FullName = "Vadym Serdiuk",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.AssocProf,
                        FacultyId = 4
                    },
                    new AcademicEmployee
                    {
                        FullName = "Eugene Chernyshov",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.SrRsch,
                        FacultyId = 4
                    },
                    new AcademicEmployee
                    {
                        FullName = "Nikita Galkin",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.Prof,
                        FacultyId = 5
                    },
                    new AcademicEmployee
                    {
                        FullName = "Pavel Koryagin",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.AssocProf,
                        FacultyId = 5
                    },
                    new AcademicEmployee
                    {
                        FullName = "Aleksei Chestnykh",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.SrRsch,
                        FacultyId = 5
                    },
                    new AcademicEmployee
                    {
                        FullName = "Ilyana Gurova",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/2922/2922561.png",
                        AcademicPosition = AcademicPosition.Prof,
                        FacultyId = 6
                    },
                    new AcademicEmployee
                    {
                        FullName = "Ivan Matiash",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.AssocProf,
                        FacultyId = 6
                    },
                    new AcademicEmployee
                    {
                        FullName = "Oleg Dereka",
                        Email = "test@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001758.png",
                        AcademicPosition = AcademicPosition.SrRsch,
                        FacultyId = 6
                    }
                    );
                context.SaveChanges();
            }
            // Students
            if (!context.Students.Any())
            {
                context.Students.AddRange(
                    new Student
                    {
                        FullName = "Test Student1",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 1
                    },
                    new Student
                    {
                        FullName = "Test Student2",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 1
                    },
                    new Student
                    {
                        FullName = "Test Student3",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 1
                    },
                    new Student
                    {
                        FullName = "Test Student4",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 1
                    },
                    new Student
                    {
                        FullName = "Test Student5",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 1
                    },
                    new Student
                    {
                        FullName = "Test Student6",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 2
                    },
                    new Student
                    {
                        FullName = "Test Student7",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 2
                    },
                    new Student
                    {
                        FullName = "Test Student8",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 2
                    },
                    new Student
                    {
                        FullName = "Test Student9",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 2
                    },
                    new Student
                    {
                        FullName = "Test Student10",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 2
                    },
                    new Student
                    {
                        FullName = "Test Student11",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 3
                    },
                    new Student
                    {
                        FullName = "Test Student12",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 3
                    },
                    new Student
                    {
                        FullName = "Test Student13",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 3
                    },
                    new Student
                    {
                        FullName = "Test Student14",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 3
                    },
                    new Student
                    {
                        FullName = "Test Student15",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 3
                    },
                    new Student
                    {
                        FullName = "Test Student16",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 4
                    },
                    new Student
                    {
                        FullName = "Test Student17",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 4
                    },
                    new Student
                    {
                        FullName = "Test Student18",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 4
                    },
                    new Student
                    {
                        FullName = "Test Student19",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 4
                    },
                    new Student
                    {
                        FullName = "Test Student20",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 4
                    },
                    new Student
                    {
                        FullName = "Test Student21",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 5
                    },
                    new Student
                    {
                        FullName = "Test Student22",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 5
                    },
                    new Student
                    {
                        FullName = "Test Student23",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 5
                    },
                    new Student
                    {
                        FullName = "Test Student24",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 6
                    },
                    new Student
                    {
                        FullName = "Test Student25",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 6
                    },
                    new Student
                    {
                        FullName = "Test Student26",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 6
                    },
                    new Student
                    {
                        FullName = "Test Student27",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 7
                    },
                    new Student
                    {
                        FullName = "Test Student28",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 7
                    },
                    new Student
                    {
                        FullName = "Test Student29",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 7
                    },
                    new Student
                    {
                        FullName = "Test Student30",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 7
                    },
                    new Student
                    {
                        FullName = "Test Student31",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 8
                    },
                    new Student
                    {
                        FullName = "Test Student32",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 8
                    },
                    new Student
                    {
                        FullName = "Test Student33",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 8
                    },
                    new Student
                    {
                        FullName = "Test Student34",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 9
                    },
                    new Student
                    {
                        FullName = "Test Student35",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 9
                    },
                    new Student
                    {
                        FullName = "Test Student36",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 9
                    },
                    new Student
                    {
                        FullName = "Test Student37",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 10
                    },
                    new Student
                    {
                        FullName = "Test Student38",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 10
                    },
                    new Student
                    {
                        FullName = "Test Student39",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 10
                    },
                    new Student
                    {
                        FullName = "Test Student40",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 10
                    },
                    new Student
                    {
                        FullName = "Test Student41",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 11
                    },
                    new Student
                    {
                        FullName = "Test Student42",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 11
                    },
                    new Student
                    {
                        FullName = "Test Student43",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 11
                    },
                    new Student
                    {
                        FullName = "Test Student44",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 12
                    },
                    new Student
                    {
                        FullName = "Test Student45",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 12
                    },
                    new Student
                    {
                        FullName = "Test Student46",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 12
                    },
                    new Student
                    {
                        FullName = "Test Student47",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 12
                    },
                    new Student
                    {
                        FullName = "Test Student48",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 12
                    },
                    new Student
                    {
                        FullName = "Test Student49",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 13
                    },
                    new Student
                    {
                        FullName = "Test Student50",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 13
                    },
                    new Student
                    {
                        FullName = "Test Student51",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 13
                    },
                    new Student
                    {
                        FullName = "Test Student52",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 14
                    },
                    new Student
                    {
                        FullName = "Test Student53",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/3001/3001785.png",
                        GroupId = 14
                    },
                    new Student
                    {
                        FullName = "Test Student54",
                        Email = "student@test.com",
                        ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/9159/9159762.png",
                        GroupId = 14
                    }
                    );
                context.SaveChanges();
            }
            // Subjects
            if (!context.Subjects.Any())
            {
                context.Subjects.AddRange(
                    new Subject { Name = "Console Programs", FacultyId = 1 },
                    new Subject { Name = "SQL", FacultyId = 1 },
                    new Subject { Name = "Decomposition", FacultyId = 1 },
                    new Subject { Name = "ASP.NET", FacultyId = 1 },
                    new Subject { Name = "WPF(WinForms)", FacultyId = 1 },
                    new Subject { Name = ".NET 7", FacultyId = 1 },
                    new Subject { Name = "Java Tools", FacultyId = 2 },
                    new Subject { Name = "Clean Code", FacultyId = 2 },
                    new Subject { Name = "Spring Boot Start", FacultyId = 2 },
                    new Subject { Name = "Spring Boot Web API", FacultyId = 2 },
                    new Subject { Name = "Spring Boot REST API", FacultyId = 2 },
                    new Subject { Name = "Intro", FacultyId = 3 },
                    new Subject { Name = "Benchmark", FacultyId = 3 },
                    new Subject { Name = "Tracker", FacultyId = 3 },
                    new Subject { Name = "News Feed", FacultyId = 3 },
                    new Subject { Name = "Forecast", FacultyId = 3 },
                    new Subject { Name = "Telegram", FacultyId = 3 },
                    new Subject { Name = "Clean Code", FacultyId = 4 },
                    new Subject { Name = "Unit Tests", FacultyId = 4 },
                    new Subject { Name = "Collections & CLI", FacultyId = 4 },
                    new Subject { Name = "Report", FacultyId = 4 },
                    new Subject { Name = "Flask", FacultyId = 4 },
                    new Subject { Name = "Django", FacultyId = 4 },
                    new Subject { Name = "CLI", FacultyId = 5 },
                    new Subject { Name = "Tooling", FacultyId = 5 },
                    new Subject { Name = "Chat Bots", FacultyId = 5 },
                    new Subject { Name = "REST", FacultyId = 5 },
                    new Subject { Name = "Cloud", FacultyId = 5 },
                    new Subject { Name = "Selenium Webdrive", FacultyId = 6 },
                    new Subject { Name = "SQL", FacultyId = 6 },
                    new Subject { Name = "Java", FacultyId = 6 },
                    new Subject { Name = "JUnit", FacultyId = 6 },
                    new Subject { Name = "Maven", FacultyId = 6 }
                    );
                context.SaveChanges();
            }
            // LectureRooms
            if (!context.LectureRooms.Any())
            {
                context.LectureRooms.AddRange(
                    new LectureRoom { Name = "1", Capacity = 20, FacultyId = 1},
                    new LectureRoom { Name = "2", Capacity = 10, FacultyId = 1 },
                    new LectureRoom { Name = "3", Capacity = 10, FacultyId = 1 },
                    new LectureRoom { Name = "4", Capacity = 5, FacultyId = 1 },
                    new LectureRoom { Name = "Online", Capacity = 60, FacultyId = 1 },
                    new LectureRoom { Name = "10", Capacity = 20, FacultyId = 2 },
                    new LectureRoom { Name = "11", Capacity = 10, FacultyId = 2 },
                    new LectureRoom { Name = "12", Capacity = 10, FacultyId = 2 },
                    new LectureRoom { Name = "13", Capacity = 5, FacultyId = 2 },
                    new LectureRoom { Name = "Online", Capacity = 100, FacultyId = 2 },
                    new LectureRoom { Name = "Blue", Capacity = 20, FacultyId = 3 },
                    new LectureRoom { Name = "Green", Capacity = 10, FacultyId = 3 },
                    new LectureRoom { Name = "Red", Capacity = 10, FacultyId = 3 },
                    new LectureRoom { Name = "Yellow", Capacity = 5, FacultyId = 3 },
                    new LectureRoom { Name = "Online", Capacity = 50, FacultyId = 3 },
                    new LectureRoom { Name = "A", Capacity = 20, FacultyId = 4 },
                    new LectureRoom { Name = "B", Capacity = 10, FacultyId = 4 },
                    new LectureRoom { Name = "C", Capacity = 10, FacultyId = 4 },
                    new LectureRoom { Name = "D", Capacity = 5, FacultyId = 4 },
                    new LectureRoom { Name = "Online", Capacity = 30, FacultyId = 4 },
                    new LectureRoom { Name = "101", Capacity = 20, FacultyId = 5 },
                    new LectureRoom { Name = "102", Capacity = 10, FacultyId = 5 },
                    new LectureRoom { Name = "103", Capacity = 10, FacultyId = 5 },
                    new LectureRoom { Name = "104", Capacity = 5, FacultyId = 5 },
                    new LectureRoom { Name = "Online", Capacity = 30, FacultyId = 5 },
                    new LectureRoom { Name = "1", Capacity = 20, FacultyId = 6 },
                    new LectureRoom { Name = "2", Capacity = 10, FacultyId = 6 },
                    new LectureRoom { Name = "3", Capacity = 10, FacultyId = 6 },
                    new LectureRoom { Name = "4", Capacity = 5, FacultyId = 6 },
                    new LectureRoom { Name = "Online", Capacity = 40, FacultyId = 6 }
                    );
                context.SaveChanges();
            }
            // Lectures
            if (!context.Lectures.Any())
            {
                context.Lectures.AddRange(
                    new Lecture
                    {
                        LectureDate = DateTime.Now.AddDays(1),
                        StartTime = new TimeSpan(8, 30, 0),
                        EndTime = new TimeSpan(10, 10, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "Console Programs"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 1),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 1)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now.AddDays(1),
                        StartTime = new TimeSpan(8, 30, 0),
                        EndTime = new TimeSpan(10, 10, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "SQL"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 2),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 2)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now.AddDays(1),
                        StartTime = new TimeSpan(10, 25, 0),
                        EndTime = new TimeSpan(12, 5, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "SQL"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 2),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 2)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now.AddDays(1),
                        StartTime = new TimeSpan(10, 25, 0),
                        EndTime = new TimeSpan(12, 5, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "Console Programs"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 1),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 1)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now.AddDays(2),
                        StartTime = new TimeSpan(8, 30, 0),
                        EndTime = new TimeSpan(10, 10, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "Decomposition"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 3),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 3)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now.AddDays(2),
                        StartTime = new TimeSpan(8, 30, 0),
                        EndTime = new TimeSpan(10, 10, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "ASP.NET"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 4),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 4)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now.AddDays(2),
                        StartTime = new TimeSpan(8, 30, 0),
                        EndTime = new TimeSpan(10, 10, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "WPF(WinForms)"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 1),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 5)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now.AddDays(3),
                        StartTime = new TimeSpan(10, 25, 0),
                        EndTime = new TimeSpan(12, 5, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == ".NET 7"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 2),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 5)
                    }
                    );
                context.SaveChanges();
            }
            // Subjects & AcademicEmployees
            if (!context.SubjectsAcademicEmployees.Any())
            {
                context.SubjectsAcademicEmployees.AddRange(
                    new SubjectAcademicEmployee { SubjectId = 1, AcademicEmployeeId = 1 },
                    new SubjectAcademicEmployee { SubjectId = 2, AcademicEmployeeId = 2 },
                    new SubjectAcademicEmployee { SubjectId = 3, AcademicEmployeeId = 3 },
                    new SubjectAcademicEmployee { SubjectId = 4, AcademicEmployeeId = 4 },
                    new SubjectAcademicEmployee { SubjectId = 5, AcademicEmployeeId = 1 },
                    new SubjectAcademicEmployee { SubjectId = 6, AcademicEmployeeId = 2 },
                    new SubjectAcademicEmployee { SubjectId = 1, AcademicEmployeeId = 3 }
                    );
                context.SaveChanges();
            }
            // Subjects & Groups
            if (!context.SubjectsGroups.Any())
            {
                context.SubjectsGroups.AddRange(
                    new SubjectGroup { SubjectId = 1, GroupId = 1 },
                    new SubjectGroup { SubjectId = 1, GroupId = 2 },
                    new SubjectGroup { SubjectId = 1, GroupId = 3 },
                    new SubjectGroup { SubjectId = 1, GroupId = 4 },
                    new SubjectGroup { SubjectId = 2, GroupId = 1 },
                    new SubjectGroup { SubjectId = 2, GroupId = 2 },
                    new SubjectGroup { SubjectId = 2, GroupId = 3 },
                    new SubjectGroup { SubjectId = 2, GroupId = 4 },
                    new SubjectGroup { SubjectId = 3, GroupId = 4 },
                    new SubjectGroup { SubjectId = 3, GroupId = 5 },
                    new SubjectGroup { SubjectId = 4, GroupId = 5 },
                    new SubjectGroup { SubjectId = 5, GroupId = 5 },
                    new SubjectGroup { SubjectId = 6, GroupId = 1 },
                    new SubjectGroup { SubjectId = 6, GroupId = 2 },
                    new SubjectGroup { SubjectId = 6, GroupId = 3 },
                    new SubjectGroup { SubjectId = 6, GroupId = 4 },
                    new SubjectGroup { SubjectId = 6, GroupId = 5 }
                    );
                context.SaveChanges();
            }
            // Lectures & Groups
            if (!context.LecturesGroups.Any())
            {
                context.LecturesGroups.AddRange(                
                    new LectureGroup { LectureId = 1, GroupId = 1 },
                    new LectureGroup { LectureId = 1, GroupId = 2 },
                    new LectureGroup { LectureId = 2, GroupId = 3 },
                    new LectureGroup { LectureId = 2, GroupId = 4 },
                    new LectureGroup { LectureId = 3, GroupId = 1 },
                    new LectureGroup { LectureId = 3, GroupId = 2 },
                    new LectureGroup { LectureId = 4, GroupId = 3 },
                    new LectureGroup { LectureId = 4, GroupId = 4 },
                    new LectureGroup { LectureId = 5, GroupId = 4 },
                    new LectureGroup { LectureId = 5, GroupId = 5 },
                    new LectureGroup { LectureId = 6, GroupId = 5 },
                    new LectureGroup { LectureId = 7, GroupId = 5 },
                    new LectureGroup { LectureId = 8, GroupId = 1 },
                    new LectureGroup { LectureId = 8, GroupId = 2 },
                    new LectureGroup { LectureId = 8, GroupId = 3 },
                    new LectureGroup { LectureId = 8, GroupId = 4 },
                    new LectureGroup { LectureId = 8, GroupId = 5 }
                );
                context.SaveChanges();
            }
        }
    }
}
