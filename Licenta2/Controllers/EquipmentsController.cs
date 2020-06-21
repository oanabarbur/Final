using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Licenta2.Models;
using Licenta2.ViewModels;

namespace Licenta2.Controllers
{
    public class EquipmentsController : Controller
    {
        private ApplicationDbContext _context;
        public EquipmentsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManageEquipments))
                return View("List");
                return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageEquipments)]
        public ViewResult New()
        {
            var category = _context.Category.ToList();

            var viewModel = new EquipmentFormViewModel
            {
                Category = category
            };
            return View("EquipmentForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var equipment = _context.Equipments.SingleOrDefault(c => c.Id == id);

            if (equipment == null)
                return HttpNotFound();

            var viewModel = new EquipmentFormViewModel(equipment) // face legatura cu equipmentformviewmodel - public equipmentformviewmodel; ca sa fie codu mai clar
            {
                Category = _context.Category.ToList()
            };
            return View("EquipmentForm", viewModel);
        }
        public ActionResult Details(int id)
        {
            var equipment = _context.Equipments.Include(m => m.Category).SingleOrDefault(m => m.Id == id);
            if (equipment == null)
                return HttpNotFound();
            return View(equipment);
        }
        // GET: Equipments
        public ActionResult Random()
        {
            var equipment = new Equipment() { Name = "schiuri" };
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomEquipmentViewModel
            {
                Equipment = equipment,
                Customers = customers
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save (Equipment equipment)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new EquipmentFormViewModel(equipment)
                {
                    Category = _context.Category.ToList()
                };
                return View("EquipmentForm", viewModel);
            }
            if(equipment.Id == 0)
            {
                equipment.DateAdded = DateTime.Now;
                _context.Equipments.Add(equipment);
            }
            else
            {
                var equipmentInDb = _context.Equipments.Single(m => m.Id == equipment.Id);

                equipmentInDb.Name = equipment.Name;
                equipmentInDb.CategoryId = equipment.CategoryId;
                equipmentInDb.NumberInStock = equipment.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Equipments");
        }
 
    }
}