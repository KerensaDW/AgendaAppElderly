using AgendaApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Data.Repositories
{
    public class DoelRepository : IDoelRepository
    {
        private readonly AgendaAppContext _context;
        private readonly DbSet<Doel> _doelen;
        public DoelRepository(AgendaAppContext context)
        {
            _context = context;
            _doelen = _context.Doelen;
        }
        public void Add(Doel doel)
        {
            _doelen.Add(doel);
        }

        public void Delete(Doel doel)
        {
            _doelen.Remove(doel);
        }

        public IEnumerable<Doel> GetAllByGebruiker(string username)
        {
            return _doelen.Where(d => string.Equals(d.GebruikerNaam, username)).OrderBy(d => d.Dates).ToList();
        }

        public Doel GetBy(decimal doelId)
        {
            return _doelen.SingleOrDefault(d => d.Id == doelId);
        }
        public Doel GetBy(string naam)
        {
            return _doelen.SingleOrDefault(d => d.Naam == naam);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
