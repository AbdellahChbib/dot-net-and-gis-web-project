using Microsoft.AspNetCore.Identity;

namespace GisServerProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Ajoute ici des propriétés personnalisées du client si besoin, par ex:
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public int? Age { get; set; }
        public string? CIN { get; set; }
    }
}
