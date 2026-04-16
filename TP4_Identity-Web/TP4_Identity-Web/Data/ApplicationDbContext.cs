using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TP4_Identity_Web.Models;

namespace TP4_Identity_Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Elfe> LesElfes { get; set; }
        public DbSet<GuerrierDuGondor> LesGuerriers { get; set; }
        public DbSet<HobbitComte> LesHobbits { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var Roles = new Roles();

            // IDs fixes pour la reproductibilité des migrations
            const string roleRoiId = "a1b2c3d4-0001-0000-0000-000000000000";
            const string roleCapitaineId = "a1b2c3d4-0002-0000-0000-000000000000";
            const string roleHabitantId = "a1b2c3d4-0003-0000-0000-000000000000";
            const string aragornId = "b1c2d3e4-0001-0000-0000-000000000000";

            // Seed des rôles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = roleRoiId, Name = Roles.Roi, NormalizedName = "ROI", ConcurrencyStamp = roleRoiId },
                new IdentityRole { Id = roleCapitaineId, Name = Roles.Capitaine, NormalizedName = "CAPITAINE", ConcurrencyStamp = roleCapitaineId },
                new IdentityRole { Id = roleHabitantId, Name = Roles.Habitant, NormalizedName = "HABITANT", ConcurrencyStamp = roleHabitantId }
            );

            // Seed de l'utilisateur Aragorn
            // Hash statique de "Gondor@1234!"
            const string aragornPasswordHash = "AQAAAAIAAYagAAAAEKk0ykdO9xAxtd+E/abSFFxEAHbBfYP9dcwup2JIMxozgtqH08xD5uDKP4WJL44lXA==";
            var aragorn = new ApplicationUser
            {
                Id = aragornId,
                UserName = "aragorn@gondor.gov",
                NormalizedUserName = "ARAGORN@GONDOR.GOV",
                Email = "aragorn@gondor.gov",
                NormalizedEmail = "ARAGORN@GONDOR.GOV",
                EmailConfirmed = true,
                Nom = "Elessar",
                Surnom = "Aragorn",
                SecurityStamp = aragornId,
                ConcurrencyStamp = aragornId,
                PasswordHash = aragornPasswordHash
            };

            builder.Entity<ApplicationUser>().HasData(aragorn);

            // Seed du claim Peuple pour Aragorn
            builder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string> { Id = 1, UserId = aragornId, ClaimType = "Peuple", ClaimValue = "Gondor" }
            );

            // Seed du rôle Roi pour Aragorn
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = aragornId, RoleId = roleRoiId }
            );
        }
    }
}
