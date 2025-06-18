using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Models;

[Index(nameof(Email), IsUnique = true)]
public partial class Gestionnaire
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [StringLength(100)]
    public string Secteur { get; set; } = null!;

    [StringLength(150)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string MotDePasse { get; set; } = null!;
}
