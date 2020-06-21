using System;
using System.Linq;
using System.Web.Http;
using Licenta2.Dtos;
using Licenta2.Models;

namespace Licenta2.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
          _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            //throw new NotImplementedException();
            //luam clientul din _context 
            //Unable to create a null constant value of type 'System.Collections.Generic.List`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'. Only entity types, enumeration types or primitive types are supported in this context
           
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            var equipments = _context.Equipments.Where(
                m => newRental.EquipmentIds.Contains(m.Id)).ToList();

            foreach (var equipment in equipments)
            {
                if (equipment.NumberAvailable == 0)
                    return BadRequest("Equipments is not available.");

                equipment.NumberAvailable--;

                var rental = new Rental  //obiect creat pt fiecare echipament
                {
                    Customer = customer,
                    Equipment = equipment,
                    DateRented = DateTime.Now
                };

              _context.Rentals.Add(rental);
            }
            _context.SaveChanges();

           return Ok();
        }
    }
}