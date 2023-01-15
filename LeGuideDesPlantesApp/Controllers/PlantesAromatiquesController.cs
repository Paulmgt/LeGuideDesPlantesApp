using LeGuideDesPlantesApp.Data;
using LeGuideDesPlantesApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LeGuideDesPlantesApp.Controllers
{
    public class PlantesAromatiquesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlantesAromatiquesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PlantesAromatiques
        public async Task<IActionResult> Index(int? page, string SearchString, string currentFilter)
        {

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            IQueryable<PlantesAromatiques> herbes = from h in _context.PlantesAromatiques
                                                    select h;

            herbes = herbes.OrderBy(h => h.Nom);

            if (!string.IsNullOrEmpty(SearchString))
            {
                herbes = herbes.Where(h => h.Nom.Contains(SearchString));

            }

            int pageNumber = page ?? 1;
            int pageSize = 3;
            return View(herbes.ToPagedList(pageNumber, pageSize));


        }

        // GET: PlantesAromatiques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlantesAromatiques == null)
            {
                return NotFound();
            }

            PlantesAromatiques? plantesAromatiques = await _context.PlantesAromatiques
                .FirstOrDefaultAsync(m => m.Id == id);
            return plantesAromatiques == null ? NotFound() : View(plantesAromatiques);
        }
        [Authorize(Policy = "RequireAdminRole")]
        // GET: PlantesAromatiques/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: PlantesAromatiques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Photos,Habitat,BienFait,Culture,Taille,Rusticite,Maladies,PeriodeDeFleuraison,Arosage,Voisinage,PrefenrenceTerrain,Entretiens,RecetteSimple")] PlantesAromatiques plantesAromatiques, IFormFile Photos)
        {
            if (Photos.Length > 0)
            {
                if (ModelState.IsValid)
                {
                    string rootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(Photos.FileName) + "_" +
                               Guid.NewGuid() +
                               Path.GetExtension(Photos.FileName);
                    string path = Path.Combine(rootPath + "/assets/PhotosAromatiques/", fileName);
                    // copier physiquement le fichier dans les dossier Images
                    // pour cela on utilise le fileStream

                    FileStream fileStream = new(path, FileMode.Create);
                    // copi async

                    await Photos.CopyToAsync(fileStream);

                    fileStream.Close();
                    plantesAromatiques.Photos = fileName;

                    _ = _context.Add(plantesAromatiques);
                    _ = await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(plantesAromatiques);
        }
        [Authorize(Policy = "RequireAdminRole")]
        // GET: PlantesAromatiques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlantesAromatiques == null)
            {
                return NotFound();
            }

            PlantesAromatiques? plantesAromatiques = await _context.PlantesAromatiques.FindAsync(id);
            return plantesAromatiques == null ? NotFound() : View(plantesAromatiques);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: PlantesAromatiques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Photos,Habitat,BienFait,Culture,Taille,Rusticite,Maladies,PeriodeDeFleuraison,Arosage,Voisinage,PrefenrenceTerrain,Entretiens,RecetteSimple")] PlantesAromatiques plantesAromatiques, IFormFile Photos)
        {
            if (id != plantesAromatiques.Id)
            {
                return NotFound();
            }
            if (Photos.Length > 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        string rootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(Photos.FileName) + "_" +
                                   Guid.NewGuid() +
                                   Path.GetExtension(Photos.FileName);
                        string path = Path.Combine(rootPath + "/assets/PhotosAromatiques/", fileName);
                        // copier physiquement le fichier dans les dossier Images
                        // pour cela on utilise le fileStream

                        FileStream fileStream = new(path, FileMode.Create);
                        // copi async

                        await Photos.CopyToAsync(fileStream);

                        fileStream.Close();
                        plantesAromatiques.Photos = fileName;

                        _ = _context.Update(plantesAromatiques);
                        _ = await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PlantesAromatiquesExists(plantesAromatiques.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(plantesAromatiques);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // GET: PlantesAromatiques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlantesAromatiques == null)
            {
                return NotFound();
            }

            PlantesAromatiques? plantesAromatiques = await _context.PlantesAromatiques
                .FirstOrDefaultAsync(m => m.Id == id);
            return plantesAromatiques == null ? NotFound() : View(plantesAromatiques);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: PlantesAromatiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlantesAromatiques == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PlantesAromatiques'  is null.");
            }
            PlantesAromatiques? plantesAromatiques = await _context.PlantesAromatiques.FindAsync(id);
            if (plantesAromatiques != null)
            {
                _ = _context.PlantesAromatiques.Remove(plantesAromatiques);
            }

            _ = await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantesAromatiquesExists(int id)
        {
            return _context.PlantesAromatiques.Any(e => e.Id == id);
        }
    }
}
