using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlobalGames.Dados;
using GlobalGames.Dados.Entidades;

namespace GlobalGames.Controllers
{
    public class InscricoesController : Controller
    {
        private readonly DataContext _context;

        public InscricoesController(DataContext context)
        {
            _context = context;
        }

        // GET: Inscricoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inscricoes.ToListAsync());
        }

        // GET: Inscricoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscricoes = await _context.Inscricoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inscricoes == null)
            {
                return NotFound();
            }

            return View(inscricoes);
        }

        // GET: Inscricoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inscricoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Apelido,Morada,CC,Localidade,DataNascimento")] Inscricoes inscricoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscricoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(inscricoes);
        }

        // GET: Inscricoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscricoes = await _context.Inscricoes.FindAsync(id);
            if (inscricoes == null)
            {
                return NotFound();
            }
            return View(inscricoes);
        }

        // POST: Inscricoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Apelido,Morada,CC,Localidade,DataNascimento")] Inscricoes inscricoes)
        {
            if (id != inscricoes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscricoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscricoesExists(inscricoes.Id))
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
            return View(inscricoes);
        }

        // GET: Inscricoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscricoes = await _context.Inscricoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inscricoes == null)
            {
                return NotFound();
            }

            return View(inscricoes);
        }

        // POST: Inscricoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscricoes = await _context.Inscricoes.FindAsync(id);
            _context.Inscricoes.Remove(inscricoes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscricoesExists(int id)
        {
            return _context.Inscricoes.Any(e => e.Id == id);
        }
    }
}
