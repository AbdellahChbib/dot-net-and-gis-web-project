using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace GisServerProject.Models;

public partial class vol
{
    [Key]
    public int id { get; set; }

    public string? nom_vol { get; set; }

    public int? nb_places_max { get; set; }

    public string? destination { get; set; }

    public string? depart { get; set; }

    [Precision(10, 2)]
    public decimal? prix { get; set; }

    [Column(TypeName = "geometry(LineString,4326)")]
    public LineString? geom { get; set; }

    [InverseProperty("vol")]
    public virtual ICollection<reservation> reservations { get; set; } = new List<reservation>();
}
