using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulAdministrator.Models
{
    public class ZaposlenikIndexVM
    {
        public class Row {

            public int zaposlenikId;
            public string imePrezime;
            public string datumZaposljavanja;
            public bool isVodic;
            public string jmbg;
        }

        public List<Row> rows;
    }
}
