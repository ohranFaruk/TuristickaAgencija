using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuristickaAgencija.Data.Models
{ 
    public class KontakPodaci
    {
        [Key]
        public int KontaktId { get; set; }
        [StringLength(30,ErrorMessage ="Polje \"Telefon\" može imati najviše 30 znakova!!!")]
        public string Telefon { get; set; }
        [EmailAddress(ErrorMessage ="Polje \"E-mail\" nije u ispravnom formatu!!!")]
        [StringLength(80,ErrorMessage = "Polje \"E-mail\" može imati najviše 80 znakova")]
        public string Email { get; set; }
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }

    }

}