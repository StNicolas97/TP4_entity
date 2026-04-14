using System.ComponentModel.DataAnnotations;

namespace TP4_Identity_Web.Models
{
    public class HobbitComte
    {
        public int Id { get; set; }
        public VillageHobbit? VillageOrigine { get; set; }
        public MetierHobbit? MetierHobbit { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
