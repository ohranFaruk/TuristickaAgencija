using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TuristickaAgencija.Data.Models
{



   public class Zahtjev
    {

        [Key]
        public int ZahtjevId { get; set; }


        [ForeignKey("Zaposlenik")]
        public int ZaposlenikId { get; set; }

        public Zaposlenik Zaposlenik { get; set; }


        public string razlog { get; set; }

        public DateTime datumKreiranja { get; set; }

        public DateTime datumPotvrde { get; set; }

        public string lokacija { get; set; }

        public bool potvrdjen { get; set; }



    }
}
