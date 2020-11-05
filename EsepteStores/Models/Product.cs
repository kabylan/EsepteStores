using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EsepteStores.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Цена")]
        public string Price { get; set; }
        [Display(Name = "Доставка")]
        public string Deliver { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    
        public List<ProductImage> ProductImages { get; set; }
    }

    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
