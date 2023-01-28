using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsepteStores.Models
{
    public class ServiceType
    {
        public int Id { get; set; }


        [Display(Name = "Название услуги")]
        public string Name { get; set; }
        

        [Display(Name = "Цена")]
        public double Price { get; set; }
    }

}
