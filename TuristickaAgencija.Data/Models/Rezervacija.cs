using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuristickaAgencija.Data.Models
{
    public class Rezervacija
    {
        [Key]
        public int RezervacijaId { get; set; }
        public DateTime DatumRezervacije { get; set; }
        [Required(ErrorMessage = "Polje \"UkupanIznos\" je obavezno!!!")]
        public double UkupanIznos { get; set; }
        [ForeignKey("Putovanje")]
        public int PutovanjeId { get; set; }
        public virtual Putovanje Putovanje { get; set; }
        [ForeignKey("PutovanjeSmjestaj")]
        public int? PutovanjeSmjestajId { get; set; }
        public virtual PutovanjeSmjestaj PutovanjeSmjestaj { get; set; }
        [ForeignKey("Turist")]
        public int TuristId { get; set; }
        public Turist Turist { get; set; }
        [ForeignKey("Stanje")]
        public int StanjeId { get; set; }
        public virtual Stanje Stanje { get; set; }

        [Required(ErrorMessage = "Polje \"Ime putnika\" je obavezno!!!")]
        public string imePutnika { get; set; }


        [Required(ErrorMessage = "Polje \"Prezime putnika\" je obavezno!!!")]
        public string prezimePutnika { get; set; }

        [Required(ErrorMessage = "Polje \"Datum rodjenja putnika\" je obavezno!!!")]

        public DateTime datumRodjenjaPutnika { get; set; }

        public string zeljeIprimjedbe { get; set; }

        [Required(ErrorMessage = "Polje \"Kontakt telefon\" je obavezno!!!")]

        public string kontaktTelefon { get; set; }


        [Required(ErrorMessage = "Polje \"Email \" je obavezno!!!")]

        public string email { get; set; }

    }
}