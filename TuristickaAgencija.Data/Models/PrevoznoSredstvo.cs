using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class PrevoznoSredstvo
    {
        [Key]
        public int PrevoznoSredstvoId { get; set; }
        [Required(ErrorMessage ="Polje \"Prevozno sredstvo\" je obavezno!!!")]
        [StringLength(20,ErrorMessage ="Polje \"Prevozno sredstvo\" može imati najviše 20 znakova!!!")]
        public string Naziv { get; set; }

    }
}
