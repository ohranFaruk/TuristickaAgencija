using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Data.DAL;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{


   

    public class RezervacijaDodaj
    {
        public int putovanjeId { get; set; }

        public double cijena { get; set; }

        public double cijenaBezSmjestaja { get; set; }

        public int trajanjePutovanja { get; set; }

        public List<SelectListItem> putovanjeSmjestaji { get; set; }
        public int stanjeId { get; set; }

        public int turistId { get; set; }




     
       

        [Required(ErrorMessage = "Polje \"Datum rodjenja\" je obavezno!!!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime datumRodjenjaPutnika { get; set; }

        [Required(ErrorMessage = "Polje \"Email\" je obavezno!!!")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Netačan email")]

        public string email { get; set; }

        [Required(ErrorMessage = "Polje \"Ime putnika\" je obavezno!!!")]

        public string imePutnika { get; set; }

        [Required(ErrorMessage = "Polje \"Kontakt telefon\" je obavezno!!!")]

        public string kontaktTelefon { get; set; }

        [Required(ErrorMessage = "Polje \"Prezime putnika\" je obavezno!!!")]

        public string prezimePutnika { get; set; }


        public string zeljeIprimjedbe { get; set; }
        public double ukupnaCijena { get; set; }
        public DateTime datumRezervacije { get; set; }

       


    }
}
