using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class Grad
    {
        [Key]
        public int GradId { get; set; }
        [Required(ErrorMessage = "Polje \"Naziv\" je obavezno!!!")]
        [StringLength(50, ErrorMessage = "Polje \"Naziv\" ne može biti duži od 50 znakova!!!")]
        public string Naziv { get; set; }
        [ForeignKey("Regija")]
        [Required(ErrorMessage = "Polje \"Regija\" je obavezno!!!")]
        public int RegijaId { get; set; }
        public virtual Regija Regija { get; set; }
    }
}
