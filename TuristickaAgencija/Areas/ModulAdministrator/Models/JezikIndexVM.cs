using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulAdministrator.Models
{
    public class JezikIndexVM
    {
        public class Row
        {
            public int vodicJezikId;
            public string nazivJezika;
            public string stepen;
        }
        public List<Row> rows;
        public int zaposlenikId;
    }
}
