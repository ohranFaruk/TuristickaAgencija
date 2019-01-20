using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class Jezik
    {
        [Key]
        public int JezikId { get; set; }
        [Required(ErrorMessage = "Polje \"Jezik\" je obavezno!!!")]
        [StringLength(30, ErrorMessage = "Polje \"Jezik\" ne može biti duže od 30 znakova!!!")]
        public string NazivJezika { get; set; }
    }
}
