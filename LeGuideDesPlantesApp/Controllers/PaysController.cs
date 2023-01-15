using LeGuideDesPlantesApp.Data;
using LeGuideDesPlantesApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeGuideDesPlantesApp.Controllers
{
    public class PaysController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PaysController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pays
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pays.ToListAsync());
        }

        // GET: Pays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pays == null)
            {
                return NotFound();
            }

            Pays? pays = await _context.Pays
                .FirstOrDefaultAsync(m => m.Id == id);
            return pays == null ? NotFound() : View(pays);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Pays/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        // POST: Pays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomPays,HuilesEssentielId,PlantesAromatiquesId,PlantesSauvagesId,ArbresId")] Pays pays)
        {
            if (ModelState.IsValid)
            {
                _ = _context.Add(pays);
                _ = await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pays);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Pays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pays == null)
            {
                return NotFound();
            }

            Pays? pays = await _context.Pays.FindAsync(id);
            return pays == null ? NotFound() : View(pays);
        }

        [Authorize(Roles = "ADMIN")]
        // POST: Pays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomPays,HuilesEssentielId,PlantesAromatiquesId,PlantesSauvagesId,ArbresId")] Pays pays)
        {
            if (id != pays.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ = _context.Update(pays);
                    _ = await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaysExists(pays.Id))
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
            return View(pays);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Pays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pays == null)
            {
                return NotFound();
            }

            Pays? pays = await _context.Pays
                .FirstOrDefaultAsync(m => m.Id == id);
            return pays == null ? NotFound() : View(pays);
        }

        [Authorize(Policy = "RequireAdminRole")]
        // POST: Pays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pays == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pays'  is null.");
            }
            Pays? pays = await _context.Pays.FindAsync(id);
            if (pays != null)
            {
                _ = _context.Pays.Remove(pays);
            }

            _ = await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaysExists(int id)
        {
            return _context.Pays.Any(e => e.Id == id);
        }
    }
}
