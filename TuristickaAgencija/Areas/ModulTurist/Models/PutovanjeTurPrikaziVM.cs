using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{
    public class PutovanjeTurPrikaziVM
    {
        public int ponudaId { get; set; }

        public class row
        {

            public int putovanjeId { get; set; }

            public int? ponudaId { get; set; }

            public string datumPolaska { get; set; }


            public string datumPovratka { get; set; }


            public string opis { get; set; }


            public double cijena { get; set; }
            public double cijenaBezSmjestaja { get; set; }

            public string slika { get; set; }

            public int gradId { get; set; }

            public string prevoznoSredstvo { get; set; }

            public string nazivGrada { get; set; }

            public string drzava { get; set; }

            public int trajanje { get; set; }
            public double? prosjecnaOcjena { get; set; }


        }

        public List<row> redovi { get; set; }
    }
}
