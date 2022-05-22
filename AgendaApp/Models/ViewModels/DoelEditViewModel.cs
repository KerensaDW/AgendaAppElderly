using AgendaApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models.ViewModels
{
    public class DoelEditViewModel
    {
        public decimal Id { get; }
        [Required(ErrorMessage = "Gelieve een naam te voorzien.")]
        public string Naam { get; set; }

        public string Kleur { get; set; }

        [Required(ErrorMessage = "Gelieve minstens 1 dag aan te duiden.")]
        [Display(Name = "Selecteer data:")]
        public List<string> Dates { get; set; }

        public DoelEditViewModel()
        {

        }

        public DoelEditViewModel(Doel doel)
        {
            Id = doel.Id;
            Naam = doel.Naam;
            Kleur = doel.Kleur;
            Dates = doel.Dates.Split(",").ToList();
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Dates.Count == 0)
                yield return new ValidationResult("Duid minstens één datum aan.");
        }
    }
}
