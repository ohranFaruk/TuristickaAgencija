using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulVodic.Models
{
    public class ZaduzenjePrikazVM
    {
        public class row
        {
            public int zaduzenjeId { get; set; }

            public string nazivPutovanja { get; set; }


            public int trenutnoTurista { get; set; }

            public string datumPolaska { get; set; }

            public string datumPovratka { get; set; }

            public int putovanjeId { get; set; }

            public string opis { get; set; }

            public bool naCekanju { get; set; }




        }

        public List<row> redovi { get; set; }

    }
}


