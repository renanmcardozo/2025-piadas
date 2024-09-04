using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Piadas.Data;
using Piadas.Models;

namespace Piadas.Controllers
{
    public class PiadaController : Controller
    {
        private readonly PiadasDbContext _context;

        public PiadaController(PiadasDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Busca()
        {
            return View();
        }
        
        public async Task<IActionResult> ShowSearchResults(String TermoDeBusca)
        {
            List<Piada> piadas = await _context.Piada.Where(f => f.Pergunta.Contains(TermoDeBusca)).ToListAsync();
            return View("Index", piadas);
        }
        
        // GET: Piada
        public async Task<IActionResult> Index()
        {
            return View(await _context.Piada.ToListAsync());
        }

        // GET: Piada/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piada = await _context.Piada
                .FirstOrDefaultAsync(m => m.id == id);
            if (piada == null)
            {
                return NotFound();
            }

            return View(piada);
        }

        // GET: Piada/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Piada/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Pergunta,Resposta")] Piada piada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(piada);
        }

        // GET: Piada/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piada = await _context.Piada.FindAsync(id);
            if (piada == null)
            {
                return NotFound();
            }
            return View(piada);
        }

        // POST: Piada/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Pergunta,Resposta")] Piada piada)
        {
            if (id != piada.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PiadaExists(piada.id))
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
            return View(piada);
        }

        // GET: Piada/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piada = await _context.Piada
                .FirstOrDefaultAsync(m => m.id == id);
            if (piada == null)
            {
                return NotFound();
            }

            return View(piada);
        }

        // POST: Piada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var piada = await _context.Piada.FindAsync(id);
            if (piada != null)
            {
                _context.Piada.Remove(piada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PiadaExists(int id)
        {
            return _context.Piada.Any(e => e.id == id);
        }
    }
}
