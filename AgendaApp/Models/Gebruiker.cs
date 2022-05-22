using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AgendaApp.Models
{
    public partial class Gebruiker : IdentityUser
    {
        public Gebruiker()
        {
            
            TwoFactorEnabled = false;
            PhoneNumberConfirmed = true;
            EmailConfirmed = true;
            LockoutEnabled = true;
            //LockoutEnd = DateTime.Now.AddDays(1);
            AccessFailedCount = 0;
            //Contracts.Add(new Contract()); //tijdelijk voor testen
        }

        //public string Gebruikersnaam { get; set; }
        public int? Gebruikernr { get; set; }
        public string Status { get; set; }
        //public string Wachtwoord { get; set; }
        
        
    }
}
