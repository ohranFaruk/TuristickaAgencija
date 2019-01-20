using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Data.Models;

namespace TuristickaAgencija.Areas.ModulAdministrator.Models
{
    public class ZaposlenikPregledVM
    {

        public string imePrezime;
        public string datumRodjenja;
        public string jmbg;
        public string spol;
        public string korisnickoIme;
        public string datumKreiranja;
        public string datumZadnjePrijave;
        public string adresa;
        public string telefon;
        public string email;
        [DataType(DataType.EmailAddress)]
        public string datumZaposljavanja;
        public string iskustvo;
        public string vrstaZaposlenika;
        public string stepenVodica;
        public string prosjcnaOcjena;
        public int zaposlenikId;
        public bool isAktivan;
        public string starost;

    }
}
