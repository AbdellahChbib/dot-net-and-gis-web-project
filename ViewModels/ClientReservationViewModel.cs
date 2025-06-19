using System.ComponentModel.DataAnnotations;
using GisServerProject.Models;

namespace GisServerProject.ViewModels
{
    public class ClientReservationViewModel
    {
        public Vol Vol { get; set; } = null!;

        public int PlacesDisponibles { get; set; }

        [Required(ErrorMessage = "Veuillez spécifier le nombre de places à réserver.")]
        [Range(1, int.MaxValue, ErrorMessage = "Le nombre de places doit être au moins 1.")]
        public int NombrePlacesReservees { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}
