using Microsoft.EntityFrameworkCore;
using AgendaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly AgendaAppContext _context;
        private readonly DbSet<Gebruiker> _gebruikers;

        public GebruikerRepository(AgendaAppContext context)
        {
            _context = context;
            _gebruikers = _context.Agendagebruikers;
        }

        public Gebruiker GetByUserName(string name)
        {
            return _gebruikers.Where(k => string.Equals(k.UserName, name)).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
