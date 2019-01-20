using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class PonudaIndexVM
    {
        public class Row
        {
            public string naziv;
            public string pocetak;
            public string zavrsetak;
            public string zadnjaIzmjena;
            public string brPutovanja;
            public int ponudaId;
            public bool isAktivna;
        }
        public List<Row> rows;
    }
}
