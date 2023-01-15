using LeGuideDesPlantesApp.Data;
using LeGuideDesPlantesApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Web.Common;

namespace LeGuideDesPlantesApp.Controllers
{
    public class ArbresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArbresController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Arbres
        public async Task<IActionResult> Index(int? page, string SearchString, string currentFilter, string sortOrder)
        {
            

            ViewBag.CurrentSort = sortOrder;

            //   ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Croiss" : "";

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            IQueryable<Arbres> arbres = from a in _context.Arbres
                                        select a;

            arbres = arbres.OrderBy(a => a.Nom);

            if (!string.IsNullOrEmpty(SearchString))
            {
                arbres = arbres.Where(a => a.Nom.Contains(SearchString));

            }
             
            int pageNumber = page ?? 1;
            int pageSize = 3;
            
            return View(arbres.ToPagedList(pageNumber, pageSize));
        }

       



        // GET: Arbres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Arbres == null)
            {
                return NotFound();
            }

            Arbres? arbres = _context.Arbres
                .FirstOrDefault(m => m.Id == id);
            return arbres == null ? NotFound() : View(arbres);
        }

        // GET: Arbres/Create
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: Arbres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Photos,Habitat,Danger,BienFait,Culture,Taille,Rusticite,Maladies,PeriodeDeFleuraison,Arosage,Voisinage,PrefenrenceTerrain,Entretiens")] Arbres arbres, IFormFile Photos)
        {

            if (Photos.Length > 0)
            {
                if (ModelState.IsValid)
                {
                    string rootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(Photos.FileName) + "_" +
                               Guid.NewGuid() +
                               Path.GetExtension(Photos.FileName);
                    string path = Path.Combine(rootPath + "/assets/PhotosArbres/", fileName);
                    // copier physiquement le fichier dans les dossier Images
                    // pour cela on utilise le fileStream

                    FileStream fileStream = new(path, FileMode.Create);
                    // copi async

                    await Photos.CopyToAsync(fileStream);

                    fileStream.Close();
                    arbres.Photos = fileName;

                    _ = _context.Add(arbres);

                    _ = await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(arbres);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // GET: Arbres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Arbres == null)
            {
                return NotFound();
            }

            Arbres? arbres = await _context.Arbres.FindAsync(id);
            return arbres == null ? NotFound() : View(arbres);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: Arbres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Photos,Habitat,Danger,BienFait,Culture,Taille,Rusticite,Maladies,PeriodeDeFleuraison,Arosage,Voisinage,PrefenrenceTerrain,Entretiens")] Arbres arbres, IFormFile Photos)
        {
            if (id != arbres.Id)
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
                        string path = Path.Combine(rootPath + "/assets/PhotosArbres/", fileName);
                        // copier physiquement le fichier dans les dossier Images
                        // pour cela on utilise le fileStream

                        FileStream fileStream = new(path, FileMode.Create);
                        // copi async

                        await Photos.CopyToAsync(fileStream);

                        fileStream.Close();
                        arbres.Photos = fileName;

                        _ = _context.Update(arbres);
                        _ = await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ArbresExists(arbres.Id))
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

            return View(arbres);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // GET: Arbres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Arbres == null)
            {
                return NotFound();
            }

            Arbres? arbres = await _context.Arbres
                .FirstOrDefaultAsync(m => m.Id == id);
            return arbres == null ? NotFound() : View(arbres);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: Arbres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Arbres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Arbres'  is null.");
            }
            Arbres? arbres = await _context.Arbres.FindAsync(id);
            if (arbres != null)
            {
                _ = _context.Arbres.Remove(arbres);
            }

            _ = await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArbresExists(int id)
        {
            return _context.Arbres.Any(e => e.Id == id);
        }
    }
}
