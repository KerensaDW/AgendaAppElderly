using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models.Domain
{
    public class Gebruiker
    {
        #region Properties
        public int GebruikerNr { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
      //  public ICollection<InlogPoging> InlogPogingen { get; set; }
      //  public string status { get; set;
        #endregion
        #region Constructors and methods
        public Gebruiker()
        {
            
        }
        public Gebruiker(string email, string wachtwoord)
        {
            this.Email = email;
            this.Wachtwoord = wachtwoord;
        }
        #endregion

    }
}
