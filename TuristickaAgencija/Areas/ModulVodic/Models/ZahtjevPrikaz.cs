using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulVodic.Models
{
    public class ZahtjevPrikaz
    {

        
        public class row
        {

            public int zahtjevId { get; set; }


            public string datumKreiranja { get; set; }


            public string razlog { get; set; }


            public bool potvrdjen { get; set; }

            public string putovanje { get; set; }

            public string datumPotvrde { get; set; }




        }


        public List<row> redovi { get; set; }
    }
}
