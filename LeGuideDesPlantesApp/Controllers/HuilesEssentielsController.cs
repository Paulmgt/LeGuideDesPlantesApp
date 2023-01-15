using LeGuideDesPlantesApp.Data;
using LeGuideDesPlantesApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LeGuideDesPlantesApp.Controllers
{
    public class HuilesEssentielsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HuilesEssentielsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: HuilesEssentiels
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

            IQueryable<HuilesEssentiel> huiles = from h in _context.HuilesEssentiel
                         .Include(h => h.PlantesAromatiques)
                         .Include(h => h.PlantesSauvages)
                                                 select h;

            huiles = huiles.OrderBy(h => h.Nom);

            if (!string.IsNullOrEmpty(SearchString))
            {
                huiles = huiles.Where(h => h.Nom.Contains(SearchString));

            }


            int pageNumber = page ?? 1;
            int pageSize = 3;
            return View(huiles.ToPagedList(pageNumber, pageSize));

        }

        // GET: HuilesEssentiels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HuilesEssentiel == null)
            {
                return NotFound();
            }

            HuilesEssentiel? huilesEssentiel = await _context.HuilesEssentiel
                .Include(h => h.PlantesAromatiques)
                .Include(h => h.PlantesSauvages)
                .FirstOrDefaultAsync(m => m.Id == id);
            return huilesEssentiel == null ? NotFound() : View(huilesEssentiel);
        }
        [Authorize(Policy = "RequireAdminRole")]
        // GET: HuilesEssentiels/Create
        public IActionResult Create()
        {
            ViewData["PlantesAromatiquesId"] = new SelectList(_context.PlantesAromatiques, "Id", "Nom");
            ViewData["PlantesSauvagesId"] = new SelectList(_context.PlantesSauvages, "Id", "Nom");
            return View();
        }
        [Authorize(Policy = "RequireAdminRole")]
        // POST: HuilesEssentiels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,NomLatin,Photos,Famille,MethodeExtraction,ComposantPrincipal,Conservation,ProprietePrincipal,ActionSurLeCorps,Circulation,Digestion,MusclesEtArticulations,PeauEtCheveux,ProblemesFeminins,GrossesEtEnfants,SensualitePourCouples,Precaution,PlantesSauvagesId,PlantesAromatiquesId")] HuilesEssentiel huilesEssentiel, IFormFile Photos)
        {
            if (Photos.Length > 0)
            {
                if (ModelState.IsValid)
                {

                    string rootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(Photos.FileName) + "_" +
                               Guid.NewGuid() +
                               Path.GetExtension(Photos.FileName);
                    string path = Path.Combine(rootPath + "/assets/PhotosHuiles/", fileName);
                    // copier physiquement le fichier dans les dossier Images
                    // pour cela on utilise le fileStream

                    FileStream fileStream = new(path, FileMode.Create);
                    // copi async

                    await Photos.CopyToAsync(fileStream);

                    fileStream.Close();
                    huilesEssentiel.Photos = fileName;

                    _ = _context.Add(huilesEssentiel);

                    _ = await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["PlantesAromatiquesId"] = new SelectList(_context.PlantesAromatiques, "Id", "Nom", huilesEssentiel.PlantesAromatiquesId);
            ViewData["PlantesSauvagesId"] = new SelectList(_context.PlantesSauvages, "Id", "Nom", huilesEssentiel.PlantesSauvagesId);
            return View(huilesEssentiel);
        }
        [Authorize(Policy = "RequireAdminRole")]
        // GET: HuilesEssentiels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HuilesEssentiel == null)
            {
                return NotFound();
            }

            HuilesEssentiel? huilesEssentiel = await _context.HuilesEssentiel.FindAsync(id);
            if (huilesEssentiel == null)
            {
                return NotFound();
            }
            ViewData["PlantesAromatiquesId"] = new SelectList(_context.PlantesAromatiques, "Id", "Nom", huilesEssentiel.PlantesAromatiquesId);
            ViewData["PlantesSauvagesId"] = new SelectList(_context.PlantesSauvages, "Id", "Nom", huilesEssentiel.PlantesSauvagesId);
            return View(huilesEssentiel);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: HuilesEssentiels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,NomLatin,Photos,Famille,MethodeExtraction,ComposantPrincipal,Conservation,ProprietePrincipal,ActionSurLeCorps,Circulation,Digestion,MusclesEtArticulations,PeauEtCheveux,ProblemesFeminins,GrossesEtEnfants,SensualitePourCouples,Precaution,PlantesSauvagesId,PlantesAromatiquesId")] HuilesEssentiel huilesEssentiel, IFormFile Photos)
        {
            if (id != huilesEssentiel.Id)
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
                        string path = Path.Combine(rootPath + "/assets/PhotosHuiles/", fileName);
                        // copier physiquement le fichier dans les dossier Images
                        // pour cela on utilise le fileStream

                        FileStream fileStream = new(path, FileMode.Create);
                        // copi async

                        await Photos.CopyToAsync(fileStream);

                        fileStream.Close();
                        huilesEssentiel.Photos = fileName;

                        _ = _context.Update(huilesEssentiel);
                        _ = await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!HuilesEssentielExists(huilesEssentiel.Id))
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

            ViewData["PlantesAromatiquesId"] = new SelectList(_context.PlantesAromatiques, "Id", "Nom", huilesEssentiel.PlantesAromatiquesId);
            ViewData["PlantesSauvagesId"] = new SelectList(_context.PlantesSauvages, "Id", "Nom", huilesEssentiel.PlantesSauvagesId);
            return View(huilesEssentiel);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // GET: HuilesEssentiels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HuilesEssentiel == null)
            {
                return NotFound();
            }

            HuilesEssentiel? huilesEssentiel = await _context.HuilesEssentiel
                .Include(h => h.PlantesAromatiques)
                .Include(h => h.PlantesSauvages)
                .FirstOrDefaultAsync(m => m.Id == id);
            return huilesEssentiel == null ? NotFound() : View(huilesEssentiel);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: HuilesEssentiels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HuilesEssentiel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HuilesEssentiel'  is null.");
            }
            HuilesEssentiel? huilesEssentiel = await _context.HuilesEssentiel.FindAsync(id);
            if (huilesEssentiel != null)
            {
                _ = _context.HuilesEssentiel.Remove(huilesEssentiel);
            }

            _ = await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HuilesEssentielExists(int id)
        {
            return _context.HuilesEssentiel.Any(e => e.Id == id);
        }
    }
}
