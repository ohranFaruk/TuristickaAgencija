using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class Kontinent
    {
        [Key]
        public int KontinentId { get; set; }
        [Required(ErrorMessage = "Polje \"Naziv\" je obavezno!!!")]
        [StringLength(20, ErrorMessage = "Polje \"Naziv\" ne može biti duže od 20 znakova!!!")]
        public string Naziv { get; set; }
    }
}
