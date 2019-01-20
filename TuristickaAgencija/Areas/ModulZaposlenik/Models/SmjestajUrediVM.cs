using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class SmjestajUrediVM
    {
        public string grad;
        public int smjestajId { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string NazivHotela { get; set; }
        public List<SelectListItem> zvjezdice { get; set; }
        [Range(1, 8, ErrorMessage = "Polje je obavezno")]
        public int brZvjezdica { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string opis { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string webStranica { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public double stdCijena { get; set; }
    }
}
