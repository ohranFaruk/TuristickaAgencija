using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulVodic.Models
{
    public class PrikazPutovanjaRecVM
    {
        public class row
        {
         
            public int putovanjeId { get; set; }

            public string nazivPutovanja { get; set; }

            public string datumPolaska { get; set; }

            public string datumPovratka { get; set; }

            public int? brojRecenzija { get; set; }
        }

        public List<row> redovi { get; set; }


    }


}
