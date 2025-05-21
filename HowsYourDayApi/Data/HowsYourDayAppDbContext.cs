using HowsYourDayAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HowsYourDayAPI.Data
{
    public class HowsYourDayAppDbContext: IdentityDbContext<AppUser>
    {
        public HowsYourDayAppDbContext(DbContextOptions<HowsYourDayAppDbContext> options) : base(options)
        {
        }

        public DbSet<Day> Days { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}