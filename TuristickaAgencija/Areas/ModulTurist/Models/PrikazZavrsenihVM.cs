using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{
    public class PrikazZavrsenihVM
    {
        
        
        public class row
        {
          

            public string slika { get; set; }

            public string lokacija { get; set; }

            public double ukupnaCijena { get; set; }

            public string datumRezervisanja { get; set; }

            public string datumPolaska { get; set; }


            public string datumPovratka { get; set; }
            public int putovanjeId { get; set; }

            public string vodic { get; set; }

            public int trajanje  { get; set; }

            public int rezervacijaId { get; set; }

           


        }


        public string ime { get; set; }

        public string prezime { get; set; }

        public List<row> redovi { get; set; }


    }
}
