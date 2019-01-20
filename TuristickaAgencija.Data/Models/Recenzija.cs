using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuristickaAgencija.Data.Models
{
    public class Recenzija
    {
        [Key]
        public int RecenzijaId { get; set; }
        [StringLength(500, ErrorMessage = "Polje \"Komentar\" može imati najviše 500 znakova!!!")]
        public string Komentar { get; set; }
        [Range(1, 10, ErrorMessage = "Polje \"Ocjena\" može biti u rasponu od 1 do 5 !!!")]
        public int? Ocjena { get; set; }
        public DateTime DatumKomentara { get; set; }
        [ForeignKey("Rezervacija")]
        public int RezervacijaId { get; set; }
        public virtual Rezervacija Rezervacija { get; set; }
    }

}