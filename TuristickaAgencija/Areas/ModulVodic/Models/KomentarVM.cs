using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulVodic.Models
{
    public class KomentarVM
    {


        public string turist { get; set; }


        public string datumRecenzije { get; set; }


        public int? ocjena { get; set; }


        public string lokacija { get; set; }

        public string smjestaj { get; set; }

        public string vodic { get; set; }

        public string komentar { get; set; }
        public int putovanjeId { get; set; }
    }
}
