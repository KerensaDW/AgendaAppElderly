using AgendaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AgendaApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AgendaApp.Controllers
{
    [Authorize(Policy = "Gebruiker")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDoelRepository _doelRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly ITimerRepository _timerRepository;
        private readonly UserManager<Gebruiker> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<Gebruiker> userManager, IGebruikerRepository gebruikerRepository, IDoelRepository doelRepository, ITimerRepository timerRepository)
        {
            _logger = logger;
            _doelRepository = doelRepository;
            _gebruikerRepository = gebruikerRepository;
            _userManager = userManager;
            _timerRepository = timerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Instructions()
        {
            return View();
        }

        public IActionResult EditDoel()
        {
            return View();
        }
        public IActionResult ViewCreateDoel()
        {
            return PartialView("CreateDoel");
        }
        [HttpPost]
        public IActionResult CreateDoel(DoelEditViewModel doelevm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_timerRepository.GetBy("MaakDoel", User.Identity.Name) != null)
                    {
                        _timerRepository.EndTimer("MaakDoel", User.Identity.Name);
                        _timerRepository.SaveChanges();
                    }
                    Gebruiker gebruiker = _gebruikerRepository.GetByUserName(User.Identity.Name);
                    Doel doel = new Doel(gebruiker);
                    MapDoelEditViewModelToDoel(doelevm, doel);
                    _doelRepository.Add(doel);
                    _doelRepository.SaveChanges();
                    TempData["message"] = $"Het doel is aangemaakt.";

                }
                catch
                {
                    TempData["error"] = "Er is een fout opgetreden, het doel is niet aangemaakt.";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ViewEditDoel(int id)
        {
            ViewData["id"] = id;
            Doel doel = _doelRepository.GetBy(id);
            DoelEditViewModel devm = new DoelEditViewModel(doel);
            return PartialView("EditDoel", devm);
        }
        [HttpPost]
        public IActionResult EditDoel(DoelEditViewModel doelevm, int id)
        {

            Doel doel = _doelRepository.GetBy(id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (_timerRepository.GetBy("EditDoel", User.Identity.Name) != null)
                    {
                        _timerRepository.EndTimer("EditDoel", User.Identity.Name);
                        _timerRepository.SaveChanges();
                    }
                    doel.EditDoel(doelevm.Dates, doelevm.Kleur, doelevm.Naam);
                    _doelRepository.SaveChanges();
                    TempData["message"] = $"Het doel is succesvol aangepast.";
                }
                catch
                {
                    TempData["error"] = $"Er is een fout opgetreden, het doel is niet aangepast.";
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(decimal id)
        {
            try
            {
                Doel doel = _doelRepository.GetBy(id);
                if (doel == null)
                {
                    return NotFound();
                }
                if (_timerRepository.GetBy("VerwijderDoel", User.Identity.Name) != null)
                {
                    _timerRepository.EndTimer("VerwijderDoel", User.Identity.Name);
                    _timerRepository.SaveChanges();
                }
                _doelRepository.Delete(doel);
                _doelRepository.SaveChanges();
                TempData["message"] = $"Uw doel is verwijderd.";
            }
            catch
            {
                TempData["error"] = "Er is een fout opgetreden, het doel is niet verwijderd.";
            }
            return RedirectToAction(nameof(Index));
        }


        private void MapDoelEditViewModelToDoel(DoelEditViewModel doelEditViewModel, Doel doel)
        {
            doel.Naam = doelEditViewModel.Naam;
            doel.Kleur = doelEditViewModel.Kleur;
            doel.Dates = String.Join(",",doelEditViewModel.Dates);

        }
        [HttpPost]
        public IActionResult CreateTimer(string naam)
        {
            Gebruiker gebruiker = _gebruikerRepository.GetByUserName(User.Identity.Name);
            Timer timer = new Timer(gebruiker, naam);
            if (_timerRepository.GetBy(naam, User.Identity.Name) != null)
            {
                _timerRepository.Replace(timer);
            } else
            {
                _timerRepository.Add(timer);
            }
            _timerRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EndTimer(string naam, string gebruiker)
        {
            _timerRepository.EndTimer(naam, gebruiker);
            _timerRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
