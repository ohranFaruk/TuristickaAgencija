using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class SmjestajPregledVM
    {
        public int smjestajId;
        public string lokacija;
        public string nazivHotela;
        public string brZvjezdica;
        public string cover;
        public string cijena;
        public string opis;
        public bool isAktivan;
    }
}
