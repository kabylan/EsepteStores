using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EsepteStores.Models;

namespace EsepteStores.Data
{
    public class EsepteStoresContext : DbContext
    {
        public EsepteStoresContext (DbContextOptions<EsepteStoresContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<EsepteStores.Models.Store> Store { get; set; }

        //public DbSet<EsepteStores.Models.Product> Product { get; set; }

        //public DbSet<EsepteStores.Models.OrderPoruduct> OrderPoruduct { get; set; }

        //public DbSet<EsepteStores.Models.Order> Order { get; set; }

        //public DbSet<EsepteStores.Models.ProductImage> ProductImages { get; set; }

        //public DbSet<EsepteStores.Models.OrderPoruduct> OrderPoruduct { get; set; }

        //public DbSet<EsepteStores.Models.Order> Order { get; set; }

        public DbSet<EsepteStores.Models.Card> Card { get; set; }

        //public DbSet<EsepteStores.Models.Product> Product { get; set; }

        //public DbSet<EsepteStores.Models.OrderPoruduct> OrderPoruduct { get; set; }

        //public DbSet<EsepteStores.Models.Order> Order { get; set; }

        //public DbSet<EsepteStores.Models.ProductImage> ProductImages { get; set; }

        //public DbSet<EsepteStores.Models.OrderPoruduct> OrderPoruduct { get; set; }

        //public DbSet<EsepteStores.Models.Order> Order { get; set; }

        public DbSet<EsepteStores.Models.ServiceType> ServiceType { get; set; }


    }
}
