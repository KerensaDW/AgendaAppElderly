using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AgendaApp.Models
{
    public partial class AgendaGebruiker : IdentityUser
    {
        public AgendaGebruiker()
        {

        }

        public AgendaGebruiker(string gebruikersnaam, string wachtwoord)
        {
            this.Gebruikersnaam = gebruikersnaam;
            this.Wachtwoord = wachtwoord;
        }

        [Key]
        public string Gebruikersnaam { get; set; }
        public int? Gebruikernr { get; set; }
        public string Status { get; set; }
        public string Wachtwoord { get; set; }
    }
}
