using System;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Data.Models
{
    public class Ponuda
    {
        [Key]
        public int PonudaId { get; set; }
        [Required(ErrorMessage = "Polje \"Naziv\" je obavezno!!!")]
        [StringLength(100, ErrorMessage = "Polje \"Naziv\" ne može biti duži od 100 znakova!!!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Polje \"Datum početka\" je obavezno!!!")]
        public DateTime DatumPocetka { get; set; }
        [Required(ErrorMessage = "Polje \"Datum završetka\" je obavezno!!!")]
        public DateTime DatumZavrsetka { get; set; }
        public DateTime DatumIzmjene { get; set; }
        public bool isAktivna { get; set; }

    }
}