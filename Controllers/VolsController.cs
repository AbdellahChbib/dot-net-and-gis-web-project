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

[HttpGet]
public async Task<IActionResult> FilteredGeoJson(string? depart, string? destination, int? nbPlacesMin, decimal? prixMax)
{
    var query = _context.Vols.AsQueryable();

    if (!string.IsNullOrEmpty(depart))
        query = query.Where(v => v.Depart != null && v.Depart.ToLower().Contains(depart.ToLower()));

    if (!string.IsNullOrEmpty(destination))
        query = query.Where(v => v.Destination != null && v.Destination.ToLower().Contains(destination.ToLower()));

    if (nbPlacesMin.HasValue)
        query = query.Where(v => v.NbPlacesMax >= nbPlacesMin.Value);

    if (prixMax.HasValue)
        query = query.Where(v => v.Prix <= prixMax.Value);

    var vols = await query.ToListAsync();

    var features = vols.Where(v => v.Geom != null).Select(v => new
    {
        type = "Feature",
        geometry = new
        {
            type = "LineString",
            coordinates = v.Geom.Coordinates.Select(c => new[] { c.X, c.Y })
        },
        properties = new
        {
            nom = v.NomVol,
            depart = v.Depart,
            destination = v.Destination,
            prix = v.Prix,
            nbPlaces = v.NbPlacesMax
        }
    }).ToList();

    return Json(new { type = "FeatureCollection", features });
}



        // Tu pourras ajouter Create, Edit, Delete, Details ici plus tard
    }
}
