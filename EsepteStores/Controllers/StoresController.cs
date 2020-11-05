using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EsepteStores.Data;
using EsepteStores.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EsepteStores.Controllers
{
    public class StoresController : Controller
    {
        private readonly EsepteStoresContext _context;
        IWebHostEnvironment _appEnvironment;

        public StoresController(EsepteStoresContext context, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _context = context;
        }

        // GET: Stores
        public IActionResult Index()
        {
            int? storeId = HttpContext.Session.GetInt32("StoreId");

            if (storeId == null)
            {
                return NotFound();
            }

            return View(_context.Store.Find(storeId));
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Store
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile LogoFile, [Bind("Id,Name,Password,LogoFilePath,LogoFileName,PhoneFirst,PhoneSecond")] Store store)
        {
            if (ModelState.IsValid)
            {

                string path = "images/" + "store_" + store.Id + "/logo/" + LogoFile.FileName;


                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/" + path, FileMode.Create))
                {


                    await LogoFile.CopyToAsync(fileStream);
                }


                store.LogoFilePath = path;


                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Store.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,PhoneFirst,PhoneSecond")] Store store, IFormFile LogoFile)
        {
            if (id != store.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    string path = "images/" + "store_" + store.Id + "/logo/" + LogoFile.FileName;


                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/" + path, FileMode.Create))
                    {
                    
                        
                        await LogoFile.CopyToAsync(fileStream);
                    }


                    store.LogoFilePath = path;


                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.Id))
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
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Store
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Store.FindAsync(id);
            _context.Store.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _context.Store.Any(e => e.Id == id);
        }
    }
}
