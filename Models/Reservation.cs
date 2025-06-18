using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace GisServerProject.Models;

public partial class Reservation
{
    [Key]
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int VolId { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime DateReservation { get; set; }

    [StringLength(20)]
    public string Statut { get; set; } = null!;

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(Client.Reservations))]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey(nameof(VolId))]
    [InverseProperty(nameof(Vol.Reservations))]
    public virtual Vol Vol { get; set; } = null!;
}
