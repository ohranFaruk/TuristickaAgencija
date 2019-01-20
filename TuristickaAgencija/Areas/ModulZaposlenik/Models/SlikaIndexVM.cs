using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class SlikaIndexVM
    {
        public class Row {
            public string imgsrc;
            public int slikaId;
        }
        public int? slikaPutovanjeId;
        public int? slikaSmjestajId;
        public List<Row> rows;
        public List<IFormFile> slike { get; set; }
        public int putovanjeId { get; set; }
        public int smjestajId { get; set; }

    }
}
