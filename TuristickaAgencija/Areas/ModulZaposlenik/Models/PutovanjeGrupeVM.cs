using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class PutovanjeGrupeVM
    {
        public class Row {
            public string imePrezime;
            public string spol;
            public string starost;
            public string vodic;
        }

        public List<Row> rowGrupa1;
        public List<Row> rowGrupa2;
        public List<Row> rowGrupa3;
        public int putovanjeId;

    }
}
