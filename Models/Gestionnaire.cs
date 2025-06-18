using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Models;

[Table("gestionnaires")]
[Index(nameof(Email), IsUnique = true)]
public partial class Gestionnaire
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [Column("secteur")]
    [StringLength(100)]
    public string Secteur { get; set; } = null!;

    [Column("email")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Column("mot_de_passe")]
    [StringLength(255)]
    public string MotDePasse { get; set; } = null!;
}
