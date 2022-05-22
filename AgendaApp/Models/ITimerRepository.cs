using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public interface ITimerRepository
    {
        Timer GetBy(string naam, string gebruiker);
        void Add(Timer timer);
        void EndTimer(string naam, string gebruiker);
        void Replace(Timer timer);
        void SaveChanges();
    }
}
