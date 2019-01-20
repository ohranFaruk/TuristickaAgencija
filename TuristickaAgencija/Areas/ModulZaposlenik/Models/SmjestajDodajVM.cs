using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class SmjestajDodajVM
    {
        [Required(ErrorMessage = "Polje je obavezno")]
        public string NazivHotela { get; set; }
        public List<SelectListItem> zvjezdice { get; set; }
        [Range(0, 8, ErrorMessage = "Polje je obavezno")]
        public int brZvjezdica { get; set; }
        public List<SelectListItem> gradovi { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Polje je obavezno")]
        public int gradId { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string opis { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string webStranica { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public double stdCijena { get; set; }
        [Required(ErrorMessage ="Minimalno 1 slika"),DataType(DataType.Upload)]
        public List<IFormFile> slike { get; set; }

    }
}
