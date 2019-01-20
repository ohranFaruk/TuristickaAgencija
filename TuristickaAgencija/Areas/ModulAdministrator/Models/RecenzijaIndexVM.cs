using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulAdministrator.Models
{
    public class RecenzijaIndexVM
    {
        public class Div {
            public string datumRecenzije;
            public string komentar;
            public string ocjena;
            public string lokacija;
            public string vodic;
            public string turist;
            public int recenzijaId;
            public string smjestaj;
            public int rezervacijaId;
        }

        public List<Div> divs;
    }
}
