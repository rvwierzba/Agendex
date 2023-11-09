using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agendex.Data;
using Agendex.Models;
using Agendex.Models.ViewModels;
using Agendex.Controllers.ActionFilters;

namespace Agendex.Controllers
{
    public class EventController : Controller
    {
             private readonly IDAL _dal;

        public EventController(IDAL dal)
        {
          _dal = dal;
        }

        // GET: Event
        public IActionResult Index()
        {
              return _dal.GetEvents() != null ? 
                          View(_dal.GetEvents()) :
                          Problem("Entity set 'ApplicationDbContext.Events'  is null.");
        }

        // GET: Event/Details/{id}
        public IActionResult Details(int? id)
        {
            if (id == null || _dal.GetEvent((int)id) == null)
            {
                return NotFound();
            }

            var @event = _dal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(new EventViewModel(_dal.GetLocations()));
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel eventViewModel, IFormCollection form)
        {
            try
            {
                _dal.CreateEvent(form);
                TempData["Alert"] = "Evento: " + form["Event.Name"] + " criado com sucesso! ";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewData["Alert"] = $"Um erro aconteceu: {ex.Message}";
                return View(eventViewModel);
            }
        }

        // GET: Event/Edit/{id?}
        [UserAccessOnly]
        public IActionResult Edit(int? id)
        {
            if (id == null || _dal.GetEvents() == null)
            {
                return NotFound();
            }

            var @event =  _dal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }
            var eventViewModel = new EventViewModel(@event, _dal.GetLocations());
            return View(eventViewModel);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
                    
            try
            {
                _dal.UpdateEvent(form);
                TempData["Alert"] = $"Evento: {form["Event.Name"]} modificado com sucesso!";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewData["Alert"] = $"Ocorreu um erro: {ex.Message}";
                var eventViewModel = new EventViewModel(_dal.GetEvent(id), _dal.GetLocations());
                return View(eventViewModel);
            }

            return View();
        }

        // GET: Event/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _dal.GetEvents() == null)
            {
                return NotFound();
            }

            var @event = _dal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dal.GetEvents() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }

           _dal.DeleteEvent(id);
           TempData["Alert"] = "Você deletou o Evento com Sucesso!";
           return RedirectToAction(nameof(Index));
                            
      
        }
               
    }
}
