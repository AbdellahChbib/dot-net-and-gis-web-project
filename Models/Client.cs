using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Models;

[Table("clients")]
[Index(nameof(CIN), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public partial class Client
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [Column("prenom")]
    [StringLength(100)]
    public string Prenom { get; set; } = null!;

    [Column("cin")]
    [StringLength(20)]
    public string CIN { get; set; } = null!;

    [Column("age")]
    public int? Age { get; set; }

    [Column("email")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Column("mot_de_passe")]
    [StringLength(255)]
    public string MotDePasse { get; set; } = null!;

    [InverseProperty(nameof(Reservation.Client))]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
