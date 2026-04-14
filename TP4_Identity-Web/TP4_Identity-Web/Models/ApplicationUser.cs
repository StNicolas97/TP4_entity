using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TP4_Identity_Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Surnom { get; set; } = string.Empty;

        [Required]
        public string Nom { get; set; } = string.Empty;

        public string? Adresse { get; set; }


    }
}
