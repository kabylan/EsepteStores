using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsepteStores.Models
{
    public class Store
    {
        public int Id { get; set; }
        [Display(Name = "Название организации")]
        public string Name { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Контактный номер 1")]
        public string PhoneFirst { get; set; }
        [Display(Name = "Контактный номер 2")]
        public string PhoneSecond { get; set; }

        public string LogoFilePath { get; set; }
        public string LogoFileName { get; set; }
    }

}
