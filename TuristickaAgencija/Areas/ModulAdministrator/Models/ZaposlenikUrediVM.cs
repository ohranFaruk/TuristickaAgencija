using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulAdministrator.Models
{
    public class ZaposlenikUrediVM
    {
        public int kontaktId { get; set; }
        public int zaposlenikId { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!!!")]
        [StringLength(30, ErrorMessage = "Polje ne može biti duže od 30 znakova!!!")]
        public string ime { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!!!")]
        [StringLength(30, ErrorMessage = "Polje ne može biti duže od 30 znakova!!!")]
        public string prezime { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!!!")]
        [MinLength(13, ErrorMessage = "Polje mora imati tačno 13 znakova!!!"), MaxLength(13, ErrorMessage = "Polje mora imati tačno 13 znakova!!!")]
        public string jmbg { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!!!"), DataType(DataType.Date)]
        public DateTime datumRodjenja { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!!!")]
        public string adresa { get; set; }
        [StringLength(20, ErrorMessage = "Polje može imati najviše 20 znakova!!!"), Required(ErrorMessage = "Polje je obavezno!!!")]
        public string telefon { get; set; }
        [EmailAddress(ErrorMessage = "Polje nije u ispravnom formatu!!!"), Required(ErrorMessage = "Polje je obavezno!!!")]
        [StringLength(80, ErrorMessage = "Polje može imati najviše 80 znakova")]
        public string email { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!!!"), DataType(DataType.Date)]
        public DateTime datumZaposljavanja { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!!!")]
        public int mjeseciIskustva { get; set; }
        public bool isVodic { get; set; }
        public List<SelectListItem> gradovi;
        [Range(1, int.MaxValue, ErrorMessage = "Polje je obavezno!!!")]
        public int gradId { get; set; }
        public List<SelectListItem> spolovi;
        [Required(ErrorMessage = "Polje je obavezno!!!")]
        public string spol { get; set; }
        
    }
}
