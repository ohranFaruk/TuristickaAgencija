using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class PutovanjeRezervacijeVM
    {
        public class Row
        {
            public int rezervacijaId;
            public string imePrezime;
            public string datum;
            public string ukupanIznos;
            public string spol;
            public string smjestaj;
        }
        public List<Row> rows;
        public int putovanjeId;
        public DateTime datumPutovanja;
    }
}
