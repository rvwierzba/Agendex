 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agendex.Data;
using Agendex.Models;

namespace Agendex.Controllers
{
    public class LocationController : Controller
    {
       private readonly IDAL _dal;

        public LocationController(IDAL idal)
        {
            _dal = idal;
        }

        // GET: Location
        public IActionResult Index()
        {
              return _dal.GetLocations() != null ? 
                          View(_dal.GetLocations()) :
                          Problem("Entity set 'ApplicationDbContext.Locations'  is null.");
        }

        // GET: Location/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null || _dal.GetLocations() == null)
            {
                return NotFound();
            }

            var location = _dal.GetLocation((int)id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location location)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    _dal.CreateLocation(location);
                    TempData["Alert"] = $"Local: {location.Name} cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex) 
                {
                    ViewData["Alert"] = $"Erro ao cadastrar Local: {ex.Message}";
                    return View(location);
                }

              
            }
            return View(location);
        }

       
    }
}
