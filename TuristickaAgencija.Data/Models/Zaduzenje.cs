using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TuristickaAgencija.Data.Models
{
   public class Zaduzenje
    {
        [Key]
        public int ZaduzenjeId { get; set; }


        [ForeignKey("Turist")]
        public int ZaposlenikId { get; set; }
        public Zaposlenik Zaposlenik { get; set; }

        [ForeignKey("Putovanje")]
        public int PutovanjeId { get; set; }
        public Putovanje Putovanje { get; set; }

        public bool odgodjeno { get; set; }

        public string opis { get; set; }

        public bool naCekanju { get; set; }


    }
}
