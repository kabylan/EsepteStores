using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EsepteStores.Models;
using Microsoft.AspNetCore.Http;
using EsepteStores.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace EsepteStores.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IWebHostEnvironment _appEnvironment;
        EsepteStoresContext context;
        public HomeController(ILogger<HomeController> logger, EsepteStoresContext db, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _logger = logger;
            context = db;
        }


        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("StoreName")))
            {
                return RedirectToAction("Login");
            }

            int? a = HttpContext.Session.GetInt32("StoreId");

            ViewBag.Store = context.Store.Find(a);

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Name, string Password)
        {
            Store store = context.Store.Where(p => p.Name == Name && p.Password == Password).FirstOrDefault();

            if (store != null)
            {
                HttpContext.Session.SetString("StoreName", Name);
                HttpContext.Session.SetInt32("StoreId", store.Id);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string Name, string Password, string PhoneFirst, string PhoneSecond)
        {
            HttpContext.Session.SetString("StoreName", Name);

            Store store = new Store()
            {
                Name = Name,
                Password = Password,
                PhoneFirst = PhoneFirst,
                PhoneSecond = PhoneSecond
            };

            context.Store.Add(store);
            context.SaveChanges();

            HttpContext.Session.SetString("StoreName", Name);
            HttpContext.Session.SetInt32("StoreId", store.Id);


            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/" + "store_" + store.Id);
            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/" + "store_" + store.Id + "/logo");
            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/" + "store_" + store.Id + "/products");


            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
