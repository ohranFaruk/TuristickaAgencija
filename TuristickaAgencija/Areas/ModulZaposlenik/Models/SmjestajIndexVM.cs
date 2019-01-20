using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class SmjestajIndexVM
    {
        public class Div { 
            public int smjestajId;
            public string nazivHotela;
            public string brojZvjezdica;
            public string lokacija;

        }
        public List<Div> divs;
    }
}
