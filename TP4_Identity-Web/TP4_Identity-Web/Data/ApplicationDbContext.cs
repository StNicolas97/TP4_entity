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
    }
}
