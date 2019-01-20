using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{
    public class PutovanjeTurDetalji
    {

        public int putovanjeId { get; set; }

        public int gradId { get; set; }

        public string  opis { get; set; }
        public double cijena { get; set; }

        public double cijenaBezSmjestaja { get; set; }

        public string lokacija { get; set; }

        public string datumPolaska { get; set; }

        public string datumPovratka { get; set; }

        public class row
        {
            public string slika { get; set; }

            public int slikaId { get; set; }
            public string nazivHotela { get; set; }
        }


        public class hotel
        {
            public int smjestajId { get; set; }

            public string nazivHotela { get; set; }

            public double cijenaNoc { get; set; }

        }


       
        public List<row> redovi { get; set; }

        public List<hotel>   hoteli { get; set; }
    }
}
