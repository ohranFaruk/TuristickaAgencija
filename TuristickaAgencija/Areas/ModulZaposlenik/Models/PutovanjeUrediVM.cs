using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class PutovanjeUrediVM
    {
        public int putovanjeId { get; set; }
        public string grad;
        public List<SelectListItem> prevozi;
        [Range(1, int.MaxValue, ErrorMessage = "Polje je obavezno!!!")]
        public int prevozId { get; set; }

        public List<SelectListItem> ponude;
        public int? ponudaId { get; set; }

        [DataType(DataType.Date), Required(ErrorMessage = "Polje je obavezno!!!")]
        public DateTime datumPolaska { get; set; }

        [DataType(DataType.Date), Required(ErrorMessage = "Polje je obavezno!!!")]
        public DateTime datumPovratka { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!!!")]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!!!"), DataType(DataType.Currency, ErrorMessage = "Polje mora bit u numerickom obliku!!!")]
        public double cijena { get; set; }
        public int? popust { get; set; }
    }
}
