using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Models;

public partial class reservation
{
    [Key]
    public int id { get; set; }

    public int client_id { get; set; }

    public int vol_id { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime date_reservation { get; set; }

    [StringLength(20)]
    public string statut { get; set; } = null!;

    [ForeignKey("client_id")]
    [InverseProperty("reservations")]
    public virtual client client { get; set; } = null!;

    [ForeignKey("vol_id")]
    [InverseProperty("reservations")]
    public virtual vol vol { get; set; } = null!;
}
