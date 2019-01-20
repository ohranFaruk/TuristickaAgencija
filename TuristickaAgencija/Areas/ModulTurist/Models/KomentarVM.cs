using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{
    public class KomentarVM
    {
        public int putovanjeId { get; set; }
        public class row
        {
            public string imeTuriste { get; set; }


            public string Tekst { get; set; }

            public int? Ocjena { get; set; }
            public string DatumKomentara { get; set; }

        }

        public List<row> redovi { get; set; }

    }
}
