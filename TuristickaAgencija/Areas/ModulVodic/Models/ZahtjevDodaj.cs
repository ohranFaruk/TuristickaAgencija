using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulVodic.Models
{
    public class ZahtjevDodaj
    {

        public int zaduzenjeId { get; set; }


        public string razlog { get; set; }

        public int putovanjeId { get; set; }
    }
}
