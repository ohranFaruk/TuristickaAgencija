using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class ProfilIndexVM
    {
        public string imePrezime;
        public string datumRodjenja;
        public string spol;
        public string korisnickoIme;
        public string adresa;
        public string telefon;
        public string email;
        public string vrstaZaposlenika;
        public int zaposlenikId;

    }
}
