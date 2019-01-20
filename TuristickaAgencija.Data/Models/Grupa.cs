using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class Grupa
    {
        [Key]
        public int GrupaId { get; set; }
        [Required(ErrorMessage = "Polje \"Naziv\" je obavezno!!!")]
        [StringLength(30, ErrorMessage = "Polje \"Naziv\" ne može biti duži od 50 znakova!!!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Polje \"Maximalni broj turista\" je obavezno!!!")]
        [Range(0,30,ErrorMessage ="Polje \"Maximalni broj turista\" može biti u opsegu između 1 i 30 ")]
        public int MaxBrojTurista { get; set; }
    }
}
