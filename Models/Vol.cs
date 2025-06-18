using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GisServerProject.Models
{
    public partial class Vol
    {
        [Key]
        public int Id { get; set; }

        public string? NomVol { get; set; }

        public int? NbPlacesMax { get; set; }

        public string? Destination { get; set; }

        public string? Depart { get; set; }

        [Precision(10, 2)]
        public decimal? Prix { get; set; }

        [Column(TypeName = "geometry(LineString,4326)")]
        public LineString? Geom { get; set; }

        [InverseProperty("Vol")]
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
