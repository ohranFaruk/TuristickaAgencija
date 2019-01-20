using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class PonudaUrediVM
    {
        public int ponudaId { get; set; }
        [Required(ErrorMessage = "Polje je obavezno"), DataType(DataType.Date)]
        public DateTime pocetak { get; set; }
        [Required(ErrorMessage = "Polje je obavezno"), DataType(DataType.Date)]
        public DateTime kraj { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string Naziv { get; set; }
    }
}
