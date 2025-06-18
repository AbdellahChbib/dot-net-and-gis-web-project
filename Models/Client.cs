using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Models;

[Index(nameof(CIN), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public partial class Client
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [StringLength(100)]
    public string Prenom { get; set; } = null!;

    [StringLength(20)]
    public string CIN { get; set; } = null!;

    public int? Age { get; set; }

    [StringLength(150)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string MotDePasse { get; set; } = null!;

    [InverseProperty(nameof(Reservation.Client))]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
