using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace TuristickaAgencija.Data.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikId { get; set; }
        [Required(ErrorMessage = "Polje \"Ime\" je obavezno!!!")]
        [StringLength(30, ErrorMessage = "Polje \"Ime\" ne može biti duže od 30 znakova!!!")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Polje \"Prezime\" je obavezno!!!")]
        [StringLength(30, ErrorMessage = "Polje \"Prezime\" ne može biti duže od 30 znakova!!!")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Polje \"JMBG\" je obavezno!!!")]
        [StringLength(13, ErrorMessage = "Polje \"JMBG\" mora imati tačno 13 znakova!!!")]
        public string JMBG { get; set; }
        [Required(ErrorMessage = "Polje \"Datum rođenja\" je obavezno!!!"),DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }
        [Required(ErrorMessage = "Polje \"Spol\" je obavezno!!!")]
        [StringLength(1)]
        public string Spol { get; set; }
        public DateTime DatumKreiranja { get; set; }
        [Required(ErrorMessage = "Polje \"Korisničko ime\" je obavezno!!!")]
        [StringLength(70)]
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        [Required(ErrorMessage = "Polje \"Grad\" je obavezno!!!")]
        [ForeignKey("Grad")]
        public int GradId { get; set; }
        public virtual Grad Grad { get; set; }
        [Required(ErrorMessage = "Polje \"Adresa\" je obavezno!!!")]
        [StringLength(70, ErrorMessage = "Adresa ne može biti duža od 70 znakova!!!")]
        public string Adresa { get; set; }
        public DateTime DatumZadnjePrijave { get; set; }

        public bool isPromjenoLozinku { get; set; }
        public bool isAktivan { get; set; }
        public bool isAdmin { get; set; }

    }
}
