﻿using System;
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
                .ToList();
            
            
            ViewBag.Products = products;
            
            
            return View();
        }

        [Route("/Order/{storeId}/{productId}")]
        public IActionResult Order(int? storeId, int? productId)
        {
            return View();
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

            return View(product);
        }




    }
}