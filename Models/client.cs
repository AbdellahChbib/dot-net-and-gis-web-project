using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Models;

[Index("cin", Name = "clients_cin_key", IsUnique = true)]
[Index("email", Name = "clients_email_key", IsUnique = true)]
public partial class client
{
    [Key]
    public int id { get; set; }

    [StringLength(100)]
    public string nom { get; set; } = null!;

    [StringLength(100)]
    public string prenom { get; set; } = null!;

    [StringLength(20)]
    public string cin { get; set; } = null!;

    public int? age { get; set; }

    [StringLength(150)]
    public string email { get; set; } = null!;

    [StringLength(255)]
    public string mot_de_passe { get; set; } = null!;

    [InverseProperty("client")]
    public virtual ICollection<reservation> reservations { get; set; } = new List<reservation>();
}
