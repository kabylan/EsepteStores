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
    public class ServiceTypesController : Controller
    {
        private readonly EsepteStoresContext _context;

        public ServiceTypesController(EsepteStoresContext context)
        {
            _context = context;
        }

        // GET: ServiceTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.ServiceType.ToListAsync());
        }

        // GET: ServiceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServiceType == null)
            {
                return NotFound();
            }

            var serviceType = await _context.ServiceType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }

        // GET: ServiceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }

        // GET: ServiceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServiceType == null)
            {
                return NotFound();
            }

            var serviceType = await _context.ServiceType.FindAsync(id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }

        // POST: ServiceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] ServiceType serviceType)
        {
            if (id != serviceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceTypeExists(serviceType.Id))
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
            return View(serviceType);
        }

        // GET: ServiceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServiceType == null)
            {
                return NotFound();
            }

            var serviceType = await _context.ServiceType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }

        // POST: ServiceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServiceType == null)
            {
                return Problem("Entity set 'EsepteStoresContext.ServiceType'  is null.");
            }
            var serviceType = await _context.ServiceType.FindAsync(id);
            if (serviceType != null)
            {
                _context.ServiceType.Remove(serviceType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceTypeExists(int id)
        {
          return _context.ServiceType.Any(e => e.Id == id);
        }
    }
}
