using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{
    public class PrijavaVM
    {
        [Required(ErrorMessage = "Polje \"Ime\" je obavezno!!!"), DataType(DataType.Text)]


        public string Ime { get; set; }
        [Required(ErrorMessage = "Polje \"Prezime\" je obavezno!!!"), DataType(DataType.Text)]


        public string Prezime { get; set; }
    }
}
