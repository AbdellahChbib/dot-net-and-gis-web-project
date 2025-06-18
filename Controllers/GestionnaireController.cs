using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GisServerProject.Data;
using GisServerProject.Models;

namespace GisServerProject.Controllers
{
    public class GestionnaireController : Controller
    {
        private readonly AppDbContext _context;

        public GestionnaireController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Gestionnaire
        public IActionResult Index()
        {
            // Redirige vers la liste des vols
            return RedirectToAction(nameof(Vols));
        }

        // GET: Gestionnaire/Vols
        public async Task<IActionResult> Vols()
        {
            var vols = await _context.Vols
                .Include(v => v.Reservations)
                    .ThenInclude(r => r.Client)
                .ToListAsync();
            return View(vols);
        }

        // GET: Gestionnaire/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var vol = await _context.Vols
                .Include(v => v.Reservations)
                    .ThenInclude(r => r.Client)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vol == null) return NotFound();

            return View(vol);
        }

        // GET: Gestionnaire/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gestionnaire/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomVol,NbPlacesMax,Destination,Depart,Prix,Geom")] Vol vol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Vols));
            }
            return View(vol);
        }

        // GET: Gestionnaire/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var vol = await _context.Vols.FindAsync(id);
            if (vol == null) return NotFound();

            return View(vol);
        }

        // POST: Gestionnaire/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomVol,NbPlacesMax,Destination,Depart,Prix,Geom")] Vol vol)
        {
            if (id != vol.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolExists(vol.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Vols));
            }
            return View(vol);
        }

        // GET: Gestionnaire/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var vol = await _context.Vols
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vol == null) return NotFound();

            return View(vol);
        }

        // POST: Gestionnaire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vol = await _context.Vols.FindAsync(id);
            if (vol != null)
            {
                _context.Vols.Remove(vol);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Vols));
        }

        // POST: Gestionnaire/Confirmer/5
        [HttpPost]
        public async Task<IActionResult> Confirmer(int id)
        {
            var res = await _context.Reservations.FindAsync(id);
            if (res == null) return NotFound();

            res.Statut = "confirmée";
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = res.VolId });
        }

        // POST: Gestionnaire/Annuler/5
        [HttpPost]
        public async Task<IActionResult> Annuler(int id)
        {
            var res = await _context.Reservations.FindAsync(id);
            if (res == null) return NotFound();

            res.Statut = "annulée";
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = res.VolId });
        }

        private bool VolExists(int id)
        {
            return _context.Vols.Any(e => e.Id == id);
        }
    }
}
