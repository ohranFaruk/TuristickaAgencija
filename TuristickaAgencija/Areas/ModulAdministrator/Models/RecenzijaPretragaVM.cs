using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulAdministrator.Models
{
    public class RecenzijaPretragaVM
    {
        public List<SelectListItem> putovanja;
        public int? putovanjeId { get; set; }
        public List<SelectListItem> vodici;
        public int? vodicId { get; set; }
    }
}
