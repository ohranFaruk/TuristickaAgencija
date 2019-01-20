using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{
    public class PonudaTurPrikaziVM
    {

        public class row
        {
            public int ponudaId { get; set; }

            public string datumOd { get; set; }

            public string datumDo { get; set; }

            public string slika { get; set; }

            public string naziv { get; set; }



        }

        public List<row> redovi { get; set; }


    }
}
