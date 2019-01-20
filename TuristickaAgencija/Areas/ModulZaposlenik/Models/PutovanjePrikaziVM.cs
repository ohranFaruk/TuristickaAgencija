using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class PutovanjePrikaziVM
    {
        public class Div {
            public int putovanjeId;
            public string lokacija;
            public string trajanje;
            public string cijena;
            public string slika;
        }

        public List<Div> divs;
    }
}
