using Core.Domain;
using Core.Domain.DomainUtil;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class ModelBuilderExtensions
    {
        // To seed Admin data into DB initially!
        public static void SeedAdmin(this ModelBuilder modelBuilder)
        {
            // Create Admin!
            var admin = new ApplicationUser
            {
                Id = "6cd4f6f3-f566-46c2-9546-b5530964a22f",
                FullName = "Ahmed Admin",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                PhoneNumber = "01029108133",
                Gender = Gender.Female,
                DateOfBirth = new DateOnly(2000, 10, 15),
                AccountType = AccountType.Admin
            };

            // Set Admin Password!
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = ph.HashPassword(admin, "adminPassword1*");

            // Seed Admin!
            modelBuilder.Entity<ApplicationUser>().HasData(admin);
        }

        // To seed Specialization data into DB initially!
        public static void SeedSpecialization(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization
                {
                    Id = (int) SpecializationType.Anesthesiology,
                    NameAr = "تخدير",
                    NameEn = "Anesthesiology"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Cardiology,
                    NameAr = "قلب",
                    NameEn = "Cardiology"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Dermatology,
                    NameAr = "جلدية",
                    NameEn = "Dermatology"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Surgery,
                    NameAr = "جراحة",
                    NameEn = "Surgery"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Neurology,
                    NameAr = "أعصاب",
                    NameEn = "Neurology"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Otolaryngology,
                    NameAr = "أنف وأذن وحنجرة",
                    NameEn = "Otolaryngology"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Pediatrics,
                    NameAr = "أطفال",
                    NameEn = "Pediatrics"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.PhysicalMedicine,
                    NameAr = "علاج طبيعي",
                    NameEn = "PhysicalMedicine"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Psychiatry,
                    NameAr = "طب نفسي",
                    NameEn = "Psychiatry"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Radiology,
                    NameAr = "أشعة",
                    NameEn = "Radiology"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Thoracic,
                    NameAr = "صدر",
                    NameEn = "Thoracic"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Urology,
                    NameAr = "مسالك بولية",
                    NameEn = "Urology"
                },
                new Specialization
                {
                    Id = (int) SpecializationType.Vascular,
                    NameAr = "أوعية دموية",
                    NameEn = "Vascular"
                }
            );
        }

        // To ensure that the NameAr column in the Specialization table can support Arabic strings!
        public static void SupportArabicSpecializationName(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>()
                .Property(s => s.NameAr)
                .UseCollation("Arabic_CI_AI");
        }

    }
}
