using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsepteStores.Models
{
    public class Card
    {
        public int Id { get; set; }
        
        public int StoreId { get; set; }
        public Store Store { get; set; }
        [Display(Name = "Вид услуги")]
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }

        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Паспортные данные")]
        public string Passport { get; set; }
        
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        
        [Display(Name = "Оплата")]
        public bool IsPayed { get; set; }

        [Display(Name = "Дата регистрации")]
        [DataType(DataType.Date)]
        public DateTime? RegisterDate { get; set; }

        [Display(Name = "Комментарии")]
        public string Comment { get; set; }

        
    }

}
