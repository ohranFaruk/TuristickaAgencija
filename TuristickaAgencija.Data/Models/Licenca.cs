using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class Licenca
    {
        [Key]
        public int LicencaId { get; set; }
        [Required(ErrorMessage ="Polje \"Naziv\" je obavezno!!!")]
        [StringLength(50, ErrorMessage ="Polje \"Naziv\" može imati najviše 50 znakova!!!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage ="Polje \"Serijski broj\" je obavezno!!!")]
        [StringLength(16,ErrorMessage ="Polje \"Serijski broj\" mora imati tačno 16 znamenki!!!")]
        public string SerijskiBrojLicence { get; set; }
        [Required(ErrorMessage ="Polje \"Datum stjecanja\" je obavezno!!!")]
        public DateTime DatumStjecanja { get; set; }
        [ForeignKey(nameof(Zaposlenik))]
        public int VodicId { get; set; }
        public virtual Zaposlenik Vodic { get; set; }
    }
}
