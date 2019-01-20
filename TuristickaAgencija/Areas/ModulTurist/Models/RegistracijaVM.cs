using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Data.Models;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{
    public class RegistracijaVM
    {

        [Required(ErrorMessage = "Polje \"Ime\" je obavezno!!!")]
        public string ime { get; set; }
        [Required(ErrorMessage = "Polje \"Prezime\" je obavezno!!!")]
        public string prezime { get; set; }


        [Required(ErrorMessage = "Polje \"KorisnickoIme\" je obavezno!!!")]
        public string KorisnickoIme { get; set; }


        [Required(ErrorMessage = "Polje \"Loznika\" je obavezno!!!"),DataType(DataType.Password)]
        public string lozinka { get; set; }



        [Required(ErrorMessage = "Polje \"ponovljenaLoznika\" je obavezno!!!"), DataType(DataType.Password)]
        [Compare(nameof(lozinka), ErrorMessage = "Lozinke se ne podudaraju")]
        public string ponovljenaLoznika { get; set; }

        [Required(ErrorMessage = "Polje \"Email\" je obavezno!!!")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage ="Netačan email")]
        public string email { get; set; }


        [Required(ErrorMessage = "Polje \"JMBG\" je obavezno!!!")]
        [StringLength(13, ErrorMessage = "Polje \"JMBG\" mora imati tačno 13 znakova!!!")]
        public string JMBG { get; set; }

        [StringLength(30, ErrorMessage = "Polje \"Telefon\" može imati najviše 30 znakova!!!")]
        [Required(ErrorMessage = "Polje \"Telefon\" je obavezno!!!")]
        public string Telefon { get; set; }


        [Required(ErrorMessage = "Polje \"Datum rodjenja\" je obavezno!!!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }


        [Required(ErrorMessage = "Polje \"Grad\" je obavezno!!!")]

        public int gradId { get; set; }

        public List<SelectListItem> gradovi { get; set; }



        [Required(ErrorMessage = "Polje \"Adresa\" je obavezno!!!")]
        [StringLength(70, ErrorMessage = "Adresa ne može biti duža od 70 znakova!!!")]
        public string Adresa { get; set; }


        [Required(ErrorMessage = "Polje \"Spol\" je obavezno!!!")]

        public List<SelectListItem> spol { get; set; }


        public bool isPromjenoLozinku { get; set; }
        public bool isAktivan { get; set; }
        public bool isAdmin { get; set; }



    }
}
