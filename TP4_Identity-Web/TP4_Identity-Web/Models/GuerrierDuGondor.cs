using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace TP4_Identity_Web.Models
{
    public class GuerrierDuGondor
    {
        public int Id { get; set; }
        public RangGondor? Rang { get; set; }
        public DivisionGondor? Division { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
