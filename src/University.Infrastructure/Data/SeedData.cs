using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
                    new Faculty { Name = "C#/.NET", Logo = "logo-1.png" },
                    new Faculty { Name = "Java Spring", Logo = "logo-2.png" },
                    new Faculty { Name = "Android", Logo = "logo-3.png" },
                    new Faculty { Name = "Python", Logo = "logo-4.png" },
                    new Faculty { Name = "Node.js", Logo = "logo-5.png" },
                    new Faculty { Name = "QA Automation", Logo = "logo-6.png" }
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
                        FirstName = "Dmytro",
                        LastName = "Lezhen",
                        Email = "test@test.com",
                        PhoneNumber ="111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        AcademicPosition = AcademicPosition.Professor,
                        FacultyId = 1
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Mykola",
                        LastName = "Kuzub",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/09/kuzub_nikolaj-removebg-preview.png",
                        AcademicPosition = AcademicPosition.Professor,
                        FacultyId = 1
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Andriy",
                        LastName = "Vynnychuk",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/09/andriyvynnychuk.png",
                        AcademicPosition = AcademicPosition.AssociateProfessor,
                        FacultyId = 1
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Ievgenii",
                        LastName = "Karpenko",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/inzhener-programist-removebg-preview.png",
                        AcademicPosition = AcademicPosition.SeniorResearcher,
                        FacultyId = 1
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Anton",
                        LastName = "Gusev",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/anton-gusev-java-1.png",
                        AcademicPosition = AcademicPosition.Professor,
                        FacultyId = 2
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Viktor",
                        LastName = "Gogilchyn",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/gogilchinjava.png",
                        AcademicPosition = AcademicPosition.AssociateProfessor,
                        FacultyId = 2
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Tetiana",
                        LastName = "Iefimenko",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/09/efimenko_tatyana-removebg-preview.png",
                        AcademicPosition = AcademicPosition.SeniorResearcher,
                        FacultyId = 2
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Boryslav",
                        LastName = "Kolomiiets",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/kolomyecz.png",
                        AcademicPosition = AcademicPosition.AssistantProfessor,
                        FacultyId = 2
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Dan",
                        LastName = "Gladshtein",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/dan-gladshtejn-android.png",
                        AcademicPosition = AcademicPosition.Professor,
                        FacultyId = 3
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Yaroslav",
                        LastName = "Rudnyk",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/yaroslav_rudnyk-removebg-preview.png",
                        AcademicPosition = AcademicPosition.AssociateProfessor,
                        FacultyId = 3
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Natalia",
                        LastName = "Kulbaka",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/natalya_kulbaka-removebg-preview-4.png",
                        AcademicPosition = AcademicPosition.AssistantProfessor,
                        FacultyId = 3
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Serhii",
                        LastName = "Tytarenko",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/sergej-titarenko-pythom-2.png",
                        AcademicPosition = AcademicPosition.Professor,
                        FacultyId = 4
                    }, new AcademicEmployee
                    {
                        FirstName = "Vadym",
                        LastName = "Serdiuk",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/serdyuk.png",
                        AcademicPosition = AcademicPosition.AssociateProfessor,
                        FacultyId = 4
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Eugene",
                        LastName = "Chernyshov",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/evgenij-chernyshov-1.png",
                        AcademicPosition = AcademicPosition.SeniorResearcher,
                        FacultyId = 4
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Nikita",
                        LastName = "Galkin",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/2.png",
                        AcademicPosition = AcademicPosition.Professor,
                        FacultyId = 5
                    }, new AcademicEmployee
                    {
                        FirstName = "Pavel",
                        LastName = "Koryagin",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/koryagyn_pavel-removebg-preview.png",
                        AcademicPosition = AcademicPosition.AssociateProfessor,
                        FacultyId = 5
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Aleksei",
                        LastName = "Chestnykh",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/oleksij_chestnyh-removebg-preview.png",
                        AcademicPosition = AcademicPosition.SeniorResearcher,
                        FacultyId = 5
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Ilyana",
                        LastName = "Gurova",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/ilyana-gurova-qa-1-1.png",
                        AcademicPosition = AcademicPosition.Professor,
                        FacultyId = 6
                    }, new AcademicEmployee
                    {
                        FirstName = "Ivan",
                        LastName = "Matiash",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/matyash_ivan_olegovych-removebg-preview.png",
                        AcademicPosition = AcademicPosition.AssociateProfessor,
                        FacultyId = 6
                    },
                    new AcademicEmployee
                    {
                        FirstName = "Oleg",
                        LastName = "Dereka",
                        Email = "test@test.com",
                        PhoneNumber = "111-11-11",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/10/dereka_oleg_mykolajovych-removebg-preview.png",
                        AcademicPosition = AcademicPosition.SeniorResearcher,
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
                        FirstName = "test",
                        LastName = "Student1",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 1
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student2",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 1
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student3",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 1
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student4",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 1
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student5",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 1
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student6",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 2
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student7",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 2
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student8",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 2
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student9",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 2
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student10",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 2
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student11",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 3
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student12",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 3
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student13",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 3
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student14",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 3
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student15",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 3
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student16",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 4
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student17",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 4
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student18",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 4
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student19",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 4
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student20",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 4
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student21",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 5
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student22",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 5
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student23",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 5
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student24",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 6
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student25",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 6
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student26",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 6
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student27",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 7
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student28",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 7
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student29",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 7
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student30",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 7
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student31",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 8
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student32",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 8
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student33",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 8
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student34",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 9
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student35",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 9
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student36",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 9
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student37",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 10
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student38",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 10
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student39",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 10
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student40",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 10
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student41",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 11
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student42",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 11
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student43",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 11
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student44",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 12
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student45",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 12
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student46",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 12
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student47",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 12
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student48",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 12
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student49",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 13
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student50",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 13
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student51",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 13
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student52",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 14
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student53",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
                        GroupId = 14
                    },
                    new Student
                    {
                        FirstName = "test",
                        LastName = "Student54",
                        Email = "student@test.com",
                        PhoneNumber = "222-22-22",
                        ProfilePictureURL = "https://cdn-ckkcn.nitrocdn.com/zFBNvlNnhjnAtIruckhWUtdrrYcfdzYJ/assets/static/optimized/rev-d24a128/wp-content/uploads/2022/08/logo-subscribe.svg",
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
                        LectureDate = DateTime.Now,
                        StartTime = new TimeSpan(8, 30, 0),
                        EndTime = new TimeSpan(10, 10, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "Console Programs"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 1),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 1)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now,
                        StartTime = new TimeSpan(8, 30, 0),
                        EndTime = new TimeSpan(10, 10, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "SQL"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 2),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 2)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now,
                        StartTime = new TimeSpan(10, 25, 0),
                        EndTime = new TimeSpan(12, 5, 0),
                        FacultyId = 1,
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "SQL"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 2),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 2)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now,
                        StartTime = new TimeSpan(10, 25, 0),
                        EndTime = new TimeSpan(12, 5, 0),
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
                        Subject = context.Subjects.FirstOrDefault(n => n.Name == "Decomposition"),
                        Teacher = context.AcademicEmployees.FirstOrDefault(n => n.Id == 3),
                        LectureRoom = context.LectureRooms.FirstOrDefault(n => n.Id == 3)
                    },
                    new Lecture
                    {
                        LectureDate = DateTime.Now.AddDays(1),
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
                        LectureDate = DateTime.Now.AddDays(2),
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
