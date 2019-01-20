using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModels
{
    public class LoginPageVM
    {
        [Required(ErrorMessage = "Polje \"Korisničko ime\" je obavezno!!!")]
        public string korisnickoIme { get; set; }
        [Required(ErrorMessage ="Polje \"Lozinka\" je obavezno!!!"),DataType(DataType.Password)]
        public string lozinka { get; set; }
        public bool zapamtiLozinku { get; set; }

        public string url { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }




    }
}
