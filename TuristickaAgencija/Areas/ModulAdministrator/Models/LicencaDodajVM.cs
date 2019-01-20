using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulAdministrator.Models
{
    public class LicencaDodajVM
    {
        public int zaposlenikId { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!!!")]
        public string serijskiBroj { get; set; }
        [Required(ErrorMessage ="Polje je obavezno!!!")]
        public string Naziv { get; set; }
        [DataType(DataType.Date), Required(ErrorMessage = "Polje je obavezno!!!")]
        public DateTime datumStjecanja { get; set; }
    }
}
