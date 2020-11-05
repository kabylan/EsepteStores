using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EsepteStores.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name="Имя ")]
        public string CustomerName { get; set; }
        [Display(Name="Телефон")]
        public string CustomerPhone { get; set; }
        [Display(Name="Адрес")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Дата заказа")]
        public DateTime Created {get;set;}
        [Display(Name = "Статус доставки")]
        public bool IsDelivered { get; set;}
    }

    public class OrderPoruduct
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }
    }
}
