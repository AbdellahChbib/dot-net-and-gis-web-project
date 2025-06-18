using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Models;

[Table("reservations")]
public partial class Reservation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("vol_id")]
    public int VolId { get; set; }

    [Column("date_reservation", TypeName = "timestamp without time zone")]
    public DateTime DateReservation { get; set; }

    [Column("statut")]
    [StringLength(20)]
    public string Statut { get; set; } = null!;

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(Client.Reservations))]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey(nameof(VolId))]
    [InverseProperty(nameof(Vol.Reservations))]
    public virtual Vol Vol { get; set; } = null!;
}
