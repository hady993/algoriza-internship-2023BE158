using Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

            // To prevent the Booking removing by the effect of Time removing!
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Time)
                .WithOne(t => t.Booking)
                .HasForeignKey<Booking>(b => b.TimeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
