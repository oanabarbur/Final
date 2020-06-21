using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Net;
using System.Net.Http;
using System.Web.Http;
using Licenta2.Dtos;
using Licenta2.Models;
using AutoMapper;
using System.Data.Entity;


namespace Licenta2.Controllers.API
{
    public class CustomersController : ApiController
    {
        //ia datele din baza de date
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET  /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        //GET  /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST  /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto) //obicetul customer va fi automat initiat de catre api
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges(); //id-ul va fi corelat cu cel din dbe....acest customer object are un id generat din baza de date

            customerDto.Id = customer.Id; //adaugam id-ul in dti si il returnam la client


            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);  //returnam un URI pt resurse nule ale clientului
            
        }

        //PUT   /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //e posibil clientul sa trimita un id invalid si trebe sa il verificam daca exista si afisam eroarea
            if (customerInDb == null)
                return NotFound();

            //facem update
            Mapper.Map(customerDto, customerInDb);//mapam an customer dto to a customer
            //customerInDb.Name = customerDto.Name;
            //customerInDb.BirthDate = customerDto.BirthDate;
            //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

            _context.SaveChanges(); //salvam schimbarile

            return Ok();

        }

        //DELETE  /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            //verificam daca exista customer in baza de date
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb); //acest ob va fi marcat ca remove in memory
            _context.SaveChanges();

            return Ok();
        }

    }
}
