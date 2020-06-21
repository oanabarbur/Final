using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Licenta2.Models;

namespace Licenta2.ViewModels
{
    public class EquipmentFormViewModel
    {
        public IEnumerable<Category> Category { get; set; }
       // public Equipment Equipment { get; set; }
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Category")]
        [Required]
        public byte? CategoryId { get; set; }

        [Display(Name ="Number in stock")]
        //[Range(1,20)]
        [Required]
        public byte? NumberInStock { get; set; }


        public string Nume
          {
            get
              {
                return Id != 0 ? "Edit Equipment" : "New Equipment";
            //    if (Equipment != null && Equipment.Id != 0)
              //      return "Edit Equipment";
              //  return "New Equipment";
               }
           }
        public EquipmentFormViewModel() //va fi utilizat la crearea unui nou echipament
        {
            Id = 0; //sa fim siguri ca in form hidden fields is populate
        }
        
        public EquipmentFormViewModel(Equipment equipment) //
        {
            Id = equipment.Id;
            Name = equipment.Name;
            NumberInStock = equipment.NumberInStock;
            CategoryId = equipment.CategoryId;
        }
    }
}