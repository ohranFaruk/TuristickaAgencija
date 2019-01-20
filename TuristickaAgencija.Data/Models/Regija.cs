using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class Regija
    {
        [Key]
        public int RegijaId { get; set; }
        [Required(ErrorMessage = "Polje \"Naziv\" je obavezno!!!")]
        [StringLength(50, ErrorMessage = "Polje \"Naziv\" može imati najviše 50 znakova!!!")]
        public string  Naziv { get; set; }
        [Required(ErrorMessage = "Polje \"Drzava\" je obavezno!!!")]
        [ForeignKey("Drzava")]
        public int DrzavaId { get; set; }
        public virtual Drzava Drzava { get; set; }
    }
}
