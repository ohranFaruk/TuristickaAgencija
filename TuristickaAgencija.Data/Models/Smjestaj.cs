using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class Smjestaj
    {
        [Key]
        public int SmjestajId { get; set; }
        [Required(ErrorMessage = "Polje \"Naziv\" je obavezno!!!")]
        [StringLength(50, ErrorMessage = "Polje \"Naziv\" može imati najviše 50 znakova!!!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage ="Polje \"Broj zvjezdica\" je obavezno!!!")]
        [Range(1, 7, ErrorMessage = "Polje \"Broj zvjezdica\" može biti u rasponu od 1 do 7 !!!")]
        public int BrojZvjezdica { get; set; }
        [Required(ErrorMessage = "Polje \"Opis\" je obavezno!!!")]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Polje \"Web stranica\" je obavezno!!!")]
        [StringLength(100, ErrorMessage = "Polje \"Web stranica\" može imati najviše 100 znakova!!!")]
        public string WebStranica { get; set; }
        [ForeignKey("Grad")]
        public int GradId { get; set; }
        public virtual Grad Grad { get; set; }
        [Required(ErrorMessage = "Polje \"Standardna cijena\" je obavezno!!!")]
        public double StandardnaCijena { get; set; }
        public bool isAktivan { get; set; }
        

    }
}
