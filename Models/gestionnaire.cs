using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Models;

[Index("email", Name = "gestionnaires_email_key", IsUnique = true)]
public partial class gestionnaire
{
    [Key]
    public int id { get; set; }

    [StringLength(100)]
    public string nom { get; set; } = null!;

    [StringLength(100)]
    public string secteur { get; set; } = null!;

    [StringLength(150)]
    public string email { get; set; } = null!;

    [StringLength(255)]
    public string mot_de_passe { get; set; } = null!;
}
