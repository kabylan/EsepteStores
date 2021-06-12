using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsepteStores.Data;
using EsepteStores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsepteStores.Controllers
{
    public class iController : Controller
    {
        private readonly EsepteStoresContext _context;

        public iController(EsepteStoresContext context)
        {
            _context = context;
        }

        [Route("/Products/{storeId}")]
        public IActionResult Products(int? storeId)
        {
            if (storeId == null)
            {
                return NotFound();
            }

            //int? storeId = _context.Store.Where(p => p.Name == name).FirstOrDefault().Id;
            
            
            List<Product> products = _context.Product
                .Where(p =>p.StoreId == storeId)
                .Include(p => p.Store)
                .Include(p => p.ProductImages)
                .ToList();

            
            products.Reverse();

            
            ViewBag.Products = products;



            ViewBag.Store = _context.Store.Find(storeId);


            
            return View();
        }

        [Route("/Order/{storeId}/{productId}")]
        public IActionResult Order(int? storeId, int? productId)
        {

            ViewBag.Store = _context.Store.Find(storeId);
            ViewBag.Product = _context.Product.Find(productId);

            return View();
        }
        
        [HttpPost]
        [Route("/Order")]
        public IActionResult Order(int? productId, int? storeId, [Bind("Id,CustomerName,CustomerPhone,CustomerAddress,Created,IsDelivered")] Order order)
        {
            order.Created = DateTime.Now;
            order.IsDelivered = false;
            order.StoreId = storeId.Value;


            _context.Add(order);

            _context.SaveChanges();

            _context.Add(new OrderPoruduct() { OrderId = order.Id, ProductId = productId.Value });


            _context.SaveChanges();

            ViewBag.Store = _context.Store.Find(order.StoreId);

            return RedirectToAction("Products", new { storeId = _context.Product.Find(productId).StoreId });
        }

        [Route("/Details/{productId}")]
        public IActionResult Details(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }
            Product product = _context.Product
                .Where(p => p.Id == productId)
                .FirstOrDefault();

            product.Store = _context.Store.Find(product.StoreId);


            product.ProductImages = _context.ProductImages.Where(p => p.ProductId == productId).ToList();


            ViewBag.Store = _context.Store.Find(product.StoreId);


            return View(product);
        }




    }
}
