using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Licenta2.Dtos;
using Licenta2.Models;

namespace Licenta2.Controllers.API
{
    public class EquipmentsController : ApiController
    {
        private ApplicationDbContext _context;

        public EquipmentsController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<EquipmentDto> GetEquipments(string query = null)
        {
            var equipmentsQuery = _context.Equipments
                .Include(m => m.Category)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                equipmentsQuery = equipmentsQuery.Where(m => m.Name.Contains(query));

            return equipmentsQuery
                .ToList()
                .Select(Mapper.Map<Equipment, EquipmentDto>);
        }

        public IHttpActionResult GetEquipment(int id)
        {
            var equipment = _context.Equipments.SingleOrDefault(c => c.Id == id);

            if (equipment == null)
                return NotFound();

            return Ok(Mapper.Map<Equipment, EquipmentDto>(equipment));
        }

        [HttpPost]
       // [Authorize(Roles = RoleName.CanManageEquipments)]
        public IHttpActionResult CreateEquipment(EquipmentDto equipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var equipment = Mapper.Map<EquipmentDto, Equipment>(equipmentDto);
            _context.Equipments.Add(equipment);
            _context.SaveChanges();

            equipmentDto.Id = equipment.Id;
            return Created(new Uri(Request.RequestUri + "/" + equipment.Id), equipmentDto);
        }
        [HttpPut]
        public IHttpActionResult UpdateEquipment(int id, EquipmentDto equipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var equipmentInDb = _context.Equipments.SingleOrDefault(c => c.Id == id);

            if (equipmentInDb == null)
                return NotFound();

            Mapper.Map(equipmentDto, equipmentInDb);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteEquipment(int id)
        {
            var equipmentInDb = _context.Equipments.SingleOrDefault(c => c.Id == id);

            if (equipmentInDb == null)
                return NotFound();

            _context.Equipments.Remove(equipmentInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
