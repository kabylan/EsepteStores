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
    public class OrderPoruductsController : Controller
    {
        private readonly EsepteStoresContext _context;

        public OrderPoruductsController(EsepteStoresContext context)
        {
            _context = context;
        }

        // GET: OrderPoruducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderPoruduct.ToListAsync());
        }

        // GET: OrderPoruducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPoruduct = await _context.OrderPoruduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderPoruduct == null)
            {
                return NotFound();
            }

            return View(orderPoruduct);
        }

        // GET: OrderPoruducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderPoruducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,ProductId")] OrderPoruduct orderPoruduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderPoruduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderPoruduct);
        }

        // GET: OrderPoruducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPoruduct = await _context.OrderPoruduct.FindAsync(id);
            if (orderPoruduct == null)
            {
                return NotFound();
            }
            return View(orderPoruduct);
        }

        // POST: OrderPoruducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ProductId")] OrderPoruduct orderPoruduct)
        {
            if (id != orderPoruduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderPoruduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderPoruductExists(orderPoruduct.Id))
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
            return View(orderPoruduct);
        }

        // GET: OrderPoruducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPoruduct = await _context.OrderPoruduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderPoruduct == null)
            {
                return NotFound();
            }

            return View(orderPoruduct);
        }

        // POST: OrderPoruducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderPoruduct = await _context.OrderPoruduct.FindAsync(id);
            _context.OrderPoruduct.Remove(orderPoruduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderPoruductExists(int id)
        {
            return _context.OrderPoruduct.Any(e => e.Id == id);
        }
    }
}
