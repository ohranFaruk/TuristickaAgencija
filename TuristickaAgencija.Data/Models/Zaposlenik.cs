using System;
using System.Collections.Generic;
using System.Text;
using  System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuristickaAgencija.Data.Models
{
  public  class Zaposlenik
    {
        [Key]
        [ForeignKey(nameof(Korisnik))]
        [Required]
        public int ZaposlenikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        [Required(ErrorMessage ="Polje \"Datum zapošljavanja\" je obavezno!!!"),DataType(DataType.Date)]
        public DateTime DatumZaposljavanja { get; set; }
        [Required(ErrorMessage = "Polje \"Mjeseci iskustva\" je obavezno!!!"), DataType(DataType.Date)]
        public int MjeseciIskustva { get; set; }
        [ForeignKey("StepenVodica")]
        public int? StepenVodicaId { get; set; }
        public virtual StepenVodica StepenVodica { get; set; }
        public bool IsVodic { get; set; }

    }
}
