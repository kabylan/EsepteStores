using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EsepteStores.Data;
using EsepteStores.Models;

namespace EsepteStores.Controllers
{
    public class Cards1Controller : Controller
    {
        private readonly EsepteStoresContext _context;

        public Cards1Controller(EsepteStoresContext context)
        {
            _context = context;
        }

        // GET: Cards1
        public async Task<IActionResult> Index()
        {
            var esepteStoresContext = _context.Card.Include(c => c.ServiceType).Include(c => c.Store);
            return View(await esepteStoresContext.ToListAsync());
        }

        // GET: Cards1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Card == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .Include(c => c.ServiceType)
                .Include(c => c.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards1/Create
        public IActionResult Create()
        {
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceType, "Id", "Id");
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id");
            return View();
        }

        // POST: Cards1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StoreId,ServiceTypeId,FullName,BirthDate,Passport,Phone,IsPayed,RegisterDate,Comment")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceType, "Id", "Id", card.ServiceTypeId);
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id", card.StoreId);
            return View(card);
        }

        // GET: Cards1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Card == null)
            {
                return NotFound();
            }

            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceType, "Id", "Id", card.ServiceTypeId);
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id", card.StoreId);
            return View(card);
        }

        // POST: Cards1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StoreId,ServiceTypeId,FullName,BirthDate,Passport,Phone,IsPayed,RegisterDate,Comment")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
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
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceType, "Id", "Id", card.ServiceTypeId);
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id", card.StoreId);
            return View(card);
        }

        // GET: Cards1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Card == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .Include(c => c.ServiceType)
                .Include(c => c.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Card == null)
            {
                return Problem("Entity set 'EsepteStoresContext.Card'  is null.");
            }
            var card = await _context.Card.FindAsync(id);
            if (card != null)
            {
                _context.Card.Remove(card);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
          return _context.Card.Any(e => e.Id == id);
        }
    }
}
