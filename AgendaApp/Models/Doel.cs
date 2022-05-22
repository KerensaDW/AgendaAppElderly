using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class Doel
    {
        public Doel()
        {

        }
        public Doel(Gebruiker gebruiker)
        {
            GebruikerNaam = gebruiker.UserName;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; }
        public string Naam { get; set; }
        public string Dates { get; set; }
        public string Kleur { get; set; }
        public string GebruikerNaam { get; set; }

        public void EditDoel(List<string> dates, string kleur, string naam)
        {
            Dates = String.Join(",", dates);
            Kleur = kleur;
            Naam = naam;
        }
    }
}
