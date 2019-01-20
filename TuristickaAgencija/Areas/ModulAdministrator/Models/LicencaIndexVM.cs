using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulAdministrator.Models
{
    public class LicencaIndexVM
    {
        public class Row {

            public int licencaId;
            public string naziv;
            public string serijskiBroj;
            public string datumStjecanja;

        }
        public int zaposlenikId;
        public List<Row> rows;
    }
}
