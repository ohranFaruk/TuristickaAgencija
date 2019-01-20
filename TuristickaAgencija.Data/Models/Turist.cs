using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TuristickaAgencija.Data.Models
{
   public class Turist
    {
        [Key]
        [ForeignKey(nameof(Korisnik))]
        [Required]
        public int TuristId { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        [Required(ErrorMessage = "Polje \"Index\" je obavezno!!!")]
        [StringLength(20, ErrorMessage = "Polje \" Index\" može imati najviše 20 znakova!!!")]
        public string Index { get; set; }
        [Required(ErrorMessage ="Polje \"Stepen turista\" je obavezno!!!")]
        [ForeignKey("StepenTurista")]
        public int StepenTuristaId { get; set; }
        public virtual StepenTurista StepenTurista { get; set; }

    }
}
