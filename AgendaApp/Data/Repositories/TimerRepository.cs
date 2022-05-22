using AgendaApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Data.Repositories
{
    public class TimerRepository : ITimerRepository
    {
        private readonly AgendaAppContext _context;
        private readonly DbSet<Timer> _timers;

        public TimerRepository(AgendaAppContext context)
        {
            this._context = context;
            this._timers = _context.Timers;
        }
        public Timer GetBy(string naam, string gebruiker)
        {
            return _timers.SingleOrDefault(t => t.GebruikerNaam == gebruiker && t.Naam == naam);
        }
        public void Add(Timer timer)
        {
            _timers.Add(timer);
        }
        public void EndTimer(string naam, string gebruiker)
        {
            var timer = GetBy(naam, gebruiker);
            if (timer != null)
            {
                timer.EndTimer();
            }
            
        }

        public void Replace(Timer timer)
        {
            Timer t2 = _timers.SingleOrDefault(t => t.Naam == timer.Naam && t.GebruikerNaam == timer.GebruikerNaam);
            t2.Start = timer.Start;
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
