using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class PutovanjeIndexVM
    {
        public List<SelectListItem> kontinenti;
        public int? kontinentId { get; set; }
        public List<SelectListItem> drzave;
        public int? drzavaId { get; set; }

        public List<SelectListItem> gradovi;
        public int? gradId { get; set; }

    }
}
