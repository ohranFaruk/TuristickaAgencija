using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class PutovanjePregledVM
    {
        public bool isKreiraneGrupe;
        public bool isUProcesuRezervacije;
        public int putovanjeId;
        public string lokacija;
        public string datumPolaska;
        public string datumPovratka;
        public string cijenaSPopustom;
        public string trajanje;
        public string ponuda;
        public string prevoz;
        public string popust;
        public string cover;
        public string redovnaCijena;
        public string opis;
        public string datumIzmjene;
        public bool isAktivno;
    }
}
