using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // To seed the Admin data!
            modelBuilder.SeedAdmin();

            // To support Arabic strings!
            modelBuilder.SupportArabicSpecializationName();

            // To seed the Specialization data!
            modelBuilder.SeedSpecialization();

            // To prevent the Booking removing by the effect of Time removing!
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Time)
                .WithOne(t => t.Booking)
                .HasForeignKey<Booking>(b => b.TimeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
