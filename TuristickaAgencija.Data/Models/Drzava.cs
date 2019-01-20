using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class Drzava
    {
        [Key]
        public int DrzavaId { get; set; }
        [Required(ErrorMessage ="Polje \"Naziv\" je obavezno!!!")]
        [StringLength(50,ErrorMessage =" Polje \"Naziv\" ne može biti duži od 50 znakova!!!")]
        public string Naziv { get; set; }
        [ForeignKey("Kontinent")]
        [Required(ErrorMessage ="Polje \"Kontinent\" je obavezno!!!")]
        public int KontinentId { get; set; }
        public virtual Kontinent Kontinent { get; set; }
    }
}
