using System.ComponentModel.DataAnnotations;

namespace TP4_Identity_Web.Models
{
    public class Elfe
    {
        public int Id { get; set; }
        public RoyaumeElfe? RoyaumeDorigine { get; set; }
        public int? AgeElfique { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
