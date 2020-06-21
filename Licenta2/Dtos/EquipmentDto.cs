using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Licenta2.Models;

namespace Licenta2.Dtos
{
    public class EquipmentDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        // public Category Category { get; set; }

        [Required]
        public byte CategoryId { get; set; }
        public CategoryDto Category {get; set;}

        public DateTime DateAdded { get; set; }

       // [Range(1, 20)]
        public byte NumberInStock { get; set; }

    }
}