using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EsepteStores.Data;
using EsepteStores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EsepteStores.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EsepteStoresContext _context;
        IWebHostEnvironment _appEnvironment;

        public ProductsController(EsepteStoresContext context, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            int? a = HttpContext.Session.GetInt32("StoreId");

            return View(new Product() { StoreId = a.Value });
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> Files, [Bind("Id,StoreId,Name,Price,Deliver,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();

                foreach (var file in Files)
                {
                    string path = "images/" + "store_" + product.StoreId + "/products/" + file.FileName;


                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/" + path, FileMode.Create))
                    {


                        await file.CopyToAsync(fileStream);
                    }

                    _context.ProductImages.Add(new ProductImage() { Name = file.FileName, Path = path, ProductId = product.Id });
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<IFormFile> Files, int id, [Bind("Id,StoreId,Name,Price,Deliver,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // old delete
                    _context.ProductImages.RemoveRange(_context.ProductImages.Where(p => p.ProductId == product.Id).ToList());
                    await _context.SaveChangesAsync();

                    // new add
                    foreach (var file in Files)
                    {
                        string path = "images/" + "store_" + product.StoreId + "/products/" + file.FileName;


                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/" + path, FileMode.Create))
                        {


                            await file.CopyToAsync(fileStream);
                        }

                        _context.ProductImages.Add(new ProductImage() { Name = file.FileName, Path = path, ProductId = product.Id });
                    }
                    await _context.SaveChangesAsync();

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
