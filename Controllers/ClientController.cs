using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GisServerProject.Data;
using GisServerProject.Models;
using GisServerProject.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace GisServerProject.Controllers
{
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Client/Vols
        public async Task<IActionResult> Vols()
        {
            var vols = await _context.Vols.ToListAsync();
            return View(vols);
        }

        // GET: Client/Reserver/{id}
        public async Task<IActionResult> Reserver(int? id)
        {
            if (id == null) return NotFound();

            var vol = await _context.Vols.FindAsync(id);
            if (vol == null) return NotFound();

            int placesDejaReservees = _context.Reservations.Count(r => r.VolId == vol.Id && r.Statut == "confirmée");
            int placesDispo = (vol.NbPlacesMax ?? 0) - placesDejaReservees;

            var model = new ClientReservationViewModel
            {
                Vol = vol,
                PlacesDisponibles = placesDispo,
                ClientId = 1 // TODO : Récupérer depuis User.Identity (avec Identity)
            };

            return View(model);
        }

        // POST: Client/Reserver/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserver(int id, ClientReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var vol = await _context.Vols.FindAsync(id);
                if (vol == null) return NotFound();

                int placesDispo = (vol.NbPlacesMax ?? 0) -
                    _context.Reservations.Count(r => r.VolId == id && r.Statut == "confirmée");

                model.Vol = vol;
                model.PlacesDisponibles = placesDispo;

                return View(model);
            }

            var volDb = await _context.Vols.FindAsync(id);
            if (volDb == null) return NotFound();

            int placesDispoFinal = (volDb.NbPlacesMax ?? 0) -
                _context.Reservations.Count(r => r.VolId == id && r.Statut == "confirmée");

            if (model.NombrePlacesReservees > placesDispoFinal)
            {
                ModelState.AddModelError("", "Pas assez de places disponibles.");
                model.Vol = volDb;
                model.PlacesDisponibles = placesDispoFinal;
                return View(model);
            }

            for (int i = 0; i < model.NombrePlacesReservees; i++)
            {
                _context.Reservations.Add(new Reservation
                {
                    ClientId = model.ClientId,
                    VolId = id,
                    DateReservation = System.DateTime.Now,
                    Statut = "en attente"
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("MesReservations", new { clientId = model.ClientId });
        }

        // GET: Client/MesReservations?clientId=1
        public async Task<IActionResult> MesReservations(int clientId)
        {
            var reservations = await _context.Reservations
                .Where(r => r.ClientId == clientId)
                .Include(r => r.Vol)
                .OrderByDescending(r => r.DateReservation)
                .ToListAsync();

            return View(reservations);
        }

        // POST: Client/Annuler/{id}
        [HttpPost]
        public async Task<IActionResult> Annuler(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            // TODO : Ajouter une vérification si c’est bien le client connecté
            if (reservation.Statut != "annulée")
            {
                reservation.Statut = "annulée";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MesReservations", new { clientId = reservation.ClientId });
        }
    }
}
