using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta2.Dtos
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<int> EquipmentIds { get; set; }
    }
}