using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TP4_Identity_Seeder;
using TP4_Identity_Web.Models;

Console.WriteLine("Debut du seed!");

using var context = DbContextFactory.CreateDbContext();

// --- Vidange des utilisateurs seed existants ---
Console.WriteLine("Nettoyage des donnees existantes...");

var seedEmails = new[] { "aragorn@gondor.gov", "haldir@lorien.gov", "sam@comte.gov", "boromir@gondor.gov" };
var existingUsers = await context.Users.Where(u => seedEmails.Contains(u.Email!)).ToListAsync();
var existingIds = existingUsers.Select(u => u.Id).ToList();

context.UserRoles.RemoveRange(context.UserRoles.Where(r => existingIds.Contains(r.UserId)));
context.UserClaims.RemoveRange(context.UserClaims.Where(c => existingIds.Contains(c.UserId)));
context.LesGuerriers.RemoveRange(context.LesGuerriers.Where(g => existingIds.Contains(g.UserId)));
context.LesElfes.RemoveRange(context.LesElfes.Where(e => existingIds.Contains(e.UserId)));
context.LesHobbits.RemoveRange(context.LesHobbits.Where(h => existingIds.Contains(h.UserId)));
context.Users.RemoveRange(existingUsers);
await context.SaveChangesAsync();

// --- Creation des roles ---
Console.WriteLine("Creation des roles...");

foreach (var roleName in new[] { Roles.Roi, Roles.Capitaine, Roles.Habitant })
{
    if (!await context.Roles.AnyAsync(r => r.Name == roleName))
    {
        context.Roles.Add(new IdentityRole
        {
            Id               = Guid.NewGuid().ToString(),
            Name             = roleName,
            NormalizedName   = roleName.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        });
    }
}
await context.SaveChangesAsync();

var roleRoi       = await context.Roles.FirstAsync(r => r.Name == Roles.Roi);
var roleCapitaine = await context.Roles.FirstAsync(r => r.Name == Roles.Capitaine);
var roleHabitant  = await context.Roles.FirstAsync(r => r.Name == Roles.Habitant);

var hasher = new PasswordHasher<ApplicationUser>();

// --- Aragorn (Roi / Guerrier du Gondor - Intendant) ---
Console.WriteLine("Creation d'Aragorn...");

var aragorn = new ApplicationUser
{
    Id                 = Guid.NewGuid().ToString(),
    UserName           = "aragorn@gondor.gov",
    NormalizedUserName = "ARAGORN@GONDOR.GOV",
    Email              = "aragorn@gondor.gov",
    NormalizedEmail    = "ARAGORN@GONDOR.GOV",
    EmailConfirmed     = true,
    Nom                = "Elessar",
    Surnom             = "Aragorn",
    SecurityStamp      = Guid.NewGuid().ToString(),
    ConcurrencyStamp   = Guid.NewGuid().ToString()
};
aragorn.PasswordHash = hasher.HashPassword(aragorn, "Gondor@1234!");
context.Users.Add(aragorn);
context.UserClaims.Add(new IdentityUserClaim<string> { UserId = aragorn.Id, ClaimType = "Peuple", ClaimValue = "Gondor" });
context.LesGuerriers.Add(new GuerrierDuGondor
{
    UserId   = aragorn.Id,
    Rang     = RangGondor.Intendant,
    Division = DivisionGondor.MinasTirith
});
context.UserRoles.Add(new IdentityUserRole<string> { UserId = aragorn.Id, RoleId = roleRoi.Id });

// --- Boromir (Capitaine / Guerrier du Gondor - Capitaine) ---
Console.WriteLine("Creation de Boromir...");

var boromir = new ApplicationUser
{
    Id                 = Guid.NewGuid().ToString(),
    UserName           = "boromir@gondor.gov",
    NormalizedUserName = "BOROMIR@GONDOR.GOV",
    Email              = "boromir@gondor.gov",
    NormalizedEmail    = "BOROMIR@GONDOR.GOV",
    EmailConfirmed     = true,
    Nom                = "Denethor",
    Surnom             = "Boromir",
    SecurityStamp      = Guid.NewGuid().ToString(),
    ConcurrencyStamp   = Guid.NewGuid().ToString()
};
boromir.PasswordHash = hasher.HashPassword(boromir, "Gondor@1234!");
context.Users.Add(boromir);
context.UserClaims.Add(new IdentityUserClaim<string> { UserId = boromir.Id, ClaimType = "Peuple", ClaimValue = "Gondor" });
context.LesGuerriers.Add(new GuerrierDuGondor
{
    UserId   = boromir.Id,
    Rang     = RangGondor.Capitaine,   // → role Capitaine
    Division = DivisionGondor.Osgiliath
});
context.UserRoles.Add(new IdentityUserRole<string> { UserId = boromir.Id, RoleId = roleCapitaine.Id });

// --- Haldir (Capitaine / Elfe - Lórien) ---
Console.WriteLine("Creation de Haldir...");

var haldir = new ApplicationUser
{
    Id                 = Guid.NewGuid().ToString(),
    UserName           = "haldir@lorien.gov",
    NormalizedUserName = "HALDIR@LORIEN.GOV",
    Email              = "haldir@lorien.gov",
    NormalizedEmail    = "HALDIR@LORIEN.GOV",
    EmailConfirmed     = true,
    Nom                = "Lorien",
    Surnom             = "Haldir",
    SecurityStamp      = Guid.NewGuid().ToString(),
    ConcurrencyStamp   = Guid.NewGuid().ToString()
};
haldir.PasswordHash = hasher.HashPassword(haldir, "Lorien@1234!");
context.Users.Add(haldir);
context.UserClaims.Add(new IdentityUserClaim<string> { UserId = haldir.Id, ClaimType = "Peuple", ClaimValue = "Elfe" });
context.LesElfes.Add(new Elfe
{
    UserId          = haldir.Id,
    RoyaumeDorigine = RoyaumeElfe.Lorien,
    AgeElfique      = 2500
});
context.UserRoles.Add(new IdentityUserRole<string> { UserId = haldir.Id, RoleId = roleCapitaine.Id });

// --- Sam (Habitant / Hobbit - Hobbiton) ---
Console.WriteLine("Creation de Sam...");

var sam = new ApplicationUser
{
    Id                 = Guid.NewGuid().ToString(),
    UserName           = "sam@comte.gov",
    NormalizedUserName = "SAM@COMTE.GOV",
    Email              = "sam@comte.gov",
    NormalizedEmail    = "SAM@COMTE.GOV",
    EmailConfirmed     = true,
    Nom                = "Gamegie",
    Surnom             = "Sam",
    SecurityStamp      = Guid.NewGuid().ToString(),
    ConcurrencyStamp   = Guid.NewGuid().ToString()
};
sam.PasswordHash = hasher.HashPassword(sam, "Comte@1234!");
context.Users.Add(sam);
context.UserClaims.Add(new IdentityUserClaim<string> { UserId = sam.Id, ClaimType = "Peuple", ClaimValue = "Hobbit" });
context.LesHobbits.Add(new HobbitComte
{
    UserId         = sam.Id,
    VillageOrigine = VillageHobbit.Hobbiton,
    MetierHobbit   = MetierHobbit.Jardinier
});
context.UserRoles.Add(new IdentityUserRole<string> { UserId = sam.Id, RoleId = roleHabitant.Id });

await context.SaveChangesAsync();

Console.WriteLine("Seed termine avec succes!");
Console.WriteLine($"  aragorn@gondor.gov  -> Roi       (Aragorn Elessar)  | Mot de passe : Gondor@1234!");
Console.WriteLine($"  boromir@gondor.gov  -> Capitaine (Boromir Denethor) | Mot de passe : Gondor@1234!");
Console.WriteLine($"  haldir@lorien.gov   -> Capitaine (Haldir Lorien)    | Mot de passe : Lorien@1234!");
Console.WriteLine($"  sam@comte.gov       -> Habitant  (Sam Gamegie)      | Mot de passe : Comte@1234!");
