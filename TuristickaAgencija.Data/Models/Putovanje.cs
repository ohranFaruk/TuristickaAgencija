using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class Putovanje
    {

        [Key]
        public int PutovanjeId { get; set; }
        [ForeignKey("Grad")]
        [Required(ErrorMessage ="Polje \"Grad\" je obavezno!!!")]
        public int GradId { get; set; }
        public virtual Grad Grad { get; set; }
        [Required(ErrorMessage = "Polje \"DatumPolaska\" je obavezno!!!")]
        public DateTime DatumPolaska { get; set; }
        [Required(ErrorMessage = "Polje \"DatumPovratka\" je obavezno!!!")]
        public DateTime DatumPovratka { get; set; }
        [Required(ErrorMessage = "Polje \"Opis\" je obavezno!!!")]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Polje \"Cijena\" je obavezno!!!")]
        public double Cijena { get; set; }
        public int? Popust { get; set; }
        [ForeignKey("PrevoznoSredstvo")]
        public int PrevoznoSredstvoId  { get; set; }
        public virtual PrevoznoSredstvo PrevnoznoSredstvo { get; set; }
        [ForeignKey("Ponuda")]
        public int? PonudaId { get; set; }
        public virtual Ponuda Ponuda { get; set; }
        public DateTime DatumIzmjene { get; set; }
        public DateTime DatumKreiranja { get; set; }

        public bool isAktivno { get; set; }


    }
}
