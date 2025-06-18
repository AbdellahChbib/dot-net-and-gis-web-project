using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GisServerProject.Data;
using GisServerProject.Models;
using System.Threading.Tasks;

namespace GisServerProject.Controllers
{
    public class VolsController : Controller
    {
        private readonly AppDbContext _context;

        public VolsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Vols
        public async Task<IActionResult> Index()
        {
            var vols = await _context.Vols.ToListAsync();
            return View(vols);
        }

        [HttpGet]
public async Task<IActionResult> GeoJson()
{
    var vols = await _context.Vols.ToListAsync();

    var features = new List<object>();

    foreach (var vol in vols)
    {
        if (vol.Geom != null)
        {
            features.Add(new
            {
                type = "Feature",
                geometry = new
                {
                    type = "LineString",
                    coordinates = vol.Geom.Coordinates.Select(c => new[] { c.X, c.Y })
                },
                properties = new
                {
                    nom = vol.NomVol,
                    depart = vol.Depart,
                    destination = vol.Destination,
                    prix = vol.Prix
                }
            });
        }
    }

    var geoJson = new
    {
        type = "FeatureCollection",
        features = features
    };

    return Json(geoJson);
}


        // Tu pourras ajouter Create, Edit, Delete, Details ici plus tard
    }
}
