using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{
    public class RezervacijaPrikaz
    {

        public int korisnikId { get; set; }


        public class row
        {

            public int rezervacijaId { get; set; }

            public string destinacija { get; set; }

            public string turist { get; set; }


            public string datumRezervacije { get; set; }

            public double UkupanIznos { get; set; }

            public string stanje { get; set; }

            public string slika { get; set; }
            public int putovanjeId { get; set; }


        }

        public List<row> redovi { get; set; }

    }
}
