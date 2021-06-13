using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EsepteStores.Data;
using Microsoft.AspNetCore.Http;
using EsepteStores.Models;

namespace EsepteStores.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EsepteStoresContext _context;

        public OrdersController(EsepteStoresContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            int? storeId = HttpContext.Session.GetInt32("StoreId");

            if (storeId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // заказы магазина
            var orders = await _context.Order.Where(p => p.StoreId == storeId).ToListAsync();
            orders.Reverse();

            // список товар-заказ
            List<OrderPoruduct> orderPoruducts = new List<OrderPoruduct>();
            // добавляем товары связанные с заказом
            foreach (var order in orders)
            {
                var orderProductsByOrderID = _context.OrderPoruduct
                    .Include(op => op.Order)
                    .Include(op => op.Product)
                    .Where(op => op.OrderId == order.Id)
                    .ToList();

                if (orderProductsByOrderID != null)
                {
                    orderPoruducts.AddRange(orderProductsByOrderID);
                }
            }

            ViewBag.OrderPoruducts = orderPoruducts;

            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerName,CustomerPhone,CustomerAddress,Created,IsDelivered")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerName,CustomerPhone,CustomerAddress,Created,IsDelivered")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }


            int? storeId = HttpContext.Session.GetInt32("StoreId");
            order.StoreId = (int)storeId;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
