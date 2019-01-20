using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class VodicJezik
    {
        [Key]
        public int VodicJezikId { get; set; }
        [ForeignKey(nameof(Zaposlenik))]
        public int ZaposlenikId { get; set; }
        public virtual Zaposlenik Zaposlenik{ get; set; }
        [ForeignKey("Jezik")]
        public int JezikId { get; set; }
        public Jezik Jezik { get; set; }
        [Required(ErrorMessage ="Polje \"Stepen\" je obavezno!!!")]
        [StringLength(5,ErrorMessage ="Polje \"Stepen\" može imati najviše 5 znakova!!!")]
        public string Stepen { get; set; }
    }
}
