using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulVodic.Models
{
    public class PrikazRecenzijaVVM
    {
        public class row
        {
            public int recenzijaId { get; set; }

            public int turistId { get; set; }

            public string imeTurista { get; set; }

            public string prezimeTurista { get; set; }

            public string datumRecenzije { get; set; }

            public int? ocjena { get; set; }

            public string komentar { get; set; }

            public int putovanjeId { get; set; }

        }

        public List<row> redovi { get; set; }

    }
}
