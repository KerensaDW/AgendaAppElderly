using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public interface IDoelRepository
    {
        Doel GetBy(decimal doelId);
        Doel GetBy(string naam);
        IEnumerable<Doel> GetAllByGebruiker(string email);
        void Add(Doel doel);
        void Delete(Doel doel);
        void SaveChanges();
    }
}
