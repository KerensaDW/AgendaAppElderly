using Microsoft.AspNetCore.Identity;
using AgendaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Data
{
    public class DataInitializer
    {
        private readonly AgendaAppContext _context;
        private readonly UserManager<Gebruiker> _userManager;
        List<string> names = new List<string>
            {
                "klant1",
                "rene.retore",
                "luc.rumbaut",
                "michel.lejuste",
                "roger.stammeleer",
                "luc.bruneel",
                "etienne.corten",
                "eric.devos",
                "hubert.bulte",
                "margot.debus",
                "rita.hellebaut",
                "treis.bracke",
                "magda.depauw",
                "monik.demol",
                "monique.vdh",
                "marleen.stammeleer",
                "christiane.cobbaut"
            };

        public DataInitializer(AgendaAppContext context, UserManager<Gebruiker> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
            
            foreach (var name in names)
            {
                await _userManager.CreateAsync(new Gebruiker { UserName = name, Email = "email@gmail.com", Gebruikernr = names.IndexOf(name)+1 }, "W@chtwoord1");
            }
            
            _context.SaveChanges();
        }
    }
}
