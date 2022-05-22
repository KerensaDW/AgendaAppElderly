using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Controllers
{
    [Authorize(Policy = "Gebruiker")]
    public class SettingsController : Controller
    {
        public IActionResult Settings()
        {
            return View();
        }
    }
}
