using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class Timer
    {
        public Timer()
        {

        }
        public Timer(Gebruiker gebruiker, string naam)
        {
            Naam = naam;
            GebruikerNaam = gebruiker.UserName;
            Start = DateTime.Now;
        }
        public void EndTimer()
        {
            End = DateTime.Now;
            Time = (End - Start).ToString();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; }
        public string Naam { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Time { get; set; }
        public string GebruikerNaam { get; set; }

    }
}
