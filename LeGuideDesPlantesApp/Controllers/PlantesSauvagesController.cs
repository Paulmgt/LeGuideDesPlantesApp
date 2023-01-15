using LeGuideDesPlantesApp.Data;
using LeGuideDesPlantesApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LeGuideDesPlantesApp.Controllers
{
    public class PlantesSauvagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlantesSauvagesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PlantesSauvages
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

            IQueryable<PlantesSauvages> pltSauvages = from h in _context.PlantesSauvages
                                                      select h;

            pltSauvages = pltSauvages.OrderBy(h => h.Nom);

            if (!string.IsNullOrEmpty(SearchString))
            {
                pltSauvages = pltSauvages.Where(h => h.Nom.Contains(SearchString));

            }

            int pageNumber = page ?? 1;
            int pageSize = 3;
            return View(pltSauvages.ToPagedList(pageNumber, pageSize));

        }

        // GET: PlantesSauvages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlantesSauvages == null)
            {
                return NotFound();
            }

            PlantesSauvages? plantesSauvages = await _context.PlantesSauvages
                .FirstOrDefaultAsync(m => m.Id == id);
            return plantesSauvages == null ? NotFound() : View(plantesSauvages);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // GET: PlantesSauvages/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: PlantesSauvages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Photos,Habitat,Danger,BienFait,Culture,Taille,Rusticite,Maladies,PeriodeDeFleuraison,Arosage,Voisinage,PrefenrenceTerrain,Entretiens,RecetteSimple")] PlantesSauvages plantesSauvages, IFormFile Photos)
        {
            if (Photos.Length > 0)
            {
                if (ModelState.IsValid)
                {
                    string rootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(Photos.FileName) + "_" +
                               Guid.NewGuid() +
                               Path.GetExtension(Photos.FileName);
                    string path = Path.Combine(rootPath + "/assets/PhotosPlantesSauvages/", fileName);
                    // copier physiquement le fichier dans les dossier Images
                    // pour cela on utilise le fileStream

                    FileStream fileStream = new(path, FileMode.Create);
                    // copi async

                    await Photos.CopyToAsync(fileStream);

                    fileStream.Close();
                    plantesSauvages.Photos = fileName;

                    _ = _context.Add(plantesSauvages);
                    _ = await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(plantesSauvages);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // GET: PlantesSauvages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlantesSauvages == null)
            {
                return NotFound();
            }

            PlantesSauvages? plantesSauvages = await _context.PlantesSauvages.FindAsync(id);
            return plantesSauvages == null ? NotFound() : View(plantesSauvages);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: PlantesSauvages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Photos,Habitat,Danger,BienFait,Culture,Taille,Rusticite,Maladies,PeriodeDeFleuraison,Arosage,Voisinage,PrefenrenceTerrain,Entretiens,RecetteSimple")] PlantesSauvages plantesSauvages, IFormFile Photos)
        {
            if (id != plantesSauvages.Id)
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
                        string path = Path.Combine(rootPath + "/assets/PhotosPlantesSauvages/", fileName);
                        // copier physiquement le fichier dans les dossier Images
                        // pour cela on utilise le fileStream

                        FileStream fileStream = new(path, FileMode.Create);
                        // copi async

                        await Photos.CopyToAsync(fileStream);

                        fileStream.Close();
                        plantesSauvages.Photos = fileName;

                        _ = _context.Update(plantesSauvages);
                        _ = await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PlantesSauvagesExists(plantesSauvages.Id))
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

            return View(plantesSauvages);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // GET: PlantesSauvages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlantesSauvages == null)
            {
                return NotFound();
            }

            PlantesSauvages? plantesSauvages = await _context.PlantesSauvages
                .FirstOrDefaultAsync(m => m.Id == id);
            return plantesSauvages == null ? NotFound() : View(plantesSauvages);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: PlantesSauvages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlantesSauvages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PlantesSauvages'  is null.");
            }
            PlantesSauvages? plantesSauvages = await _context.PlantesSauvages.FindAsync(id);
            if (plantesSauvages != null)
            {
                _ = _context.PlantesSauvages.Remove(plantesSauvages);
            }

            _ = await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantesSauvagesExists(int id)
        {
            return _context.PlantesSauvages.Any(e => e.Id == id);
        }
    }
}
