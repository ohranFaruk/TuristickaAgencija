using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuristickaAgencija.Data.Models
{
    public class KreditnaKartica
    {
        [Key]
        public int KarticaId { get; set; }
        [Required(ErrorMessage = "Polje \"Tip kartice\" je obavezno!!!")]
        [StringLength(30, ErrorMessage = "Polje \"Tip kartice\" ne može biti duži od 30 znakova!!!")]
        public string Tip { get; set; }
        [Required(ErrorMessage = "Polje \"Broj kartice\" je obavezno!!!")]
        [StringLength(16, ErrorMessage = "Polje \"Broj kartice\" mora imati tačno 16 znakova!!!")]
        public string BrojKartice { get; set; }
        [Required(ErrorMessage = "Polje \"Mjesec isteka\" je obavezno!!!")]
        [Range(1, 12, ErrorMessage = "Polje \"Mjesec isteka\" može imati vrijednost između 1 i 12!!!")]
        public int  MjesecIsteka { get; set; }
        [Required(ErrorMessage = "Polje \"Godina isteka\" je obavezno!!!")]
        public int GodinaIsteka { get; set; }
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }

}