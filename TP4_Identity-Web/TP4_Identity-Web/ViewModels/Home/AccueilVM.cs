namespace TP4_Identity_Web.ViewModels.Home
{
    public class AccueilVM
    {
        public bool EstConnecte { get; set; }
        public string? Surnom { get; set; }
        public string? Peuple { get; set; }  // "Gondor", "Elfe", "Hobbit" ou null
        public string? Role { get; set; }    // "Roi", "Capitaine", "Habitant" ou null
    }
}
