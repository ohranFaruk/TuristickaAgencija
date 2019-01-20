using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulAdministrator.Models
{
    public class JezikDodajVM
    {
        public int zaposlenikId { get; set; }
        public List<SelectListItem> jezici;
        [Range(1, int.MaxValue, ErrorMessage = "Morate izabrati jezik!!!        ")]
        public int jezikId { get; set; }
        public List<SelectListItem> stepeniJezika;
        [Required(ErrorMessage = "        Morate izabrati stepen jezika!!!")]
        public string stepenJezika { get; set; }
    }
}
