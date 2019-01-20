using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class PutovanjaGrupe
    {
        [Key]
        public int PutovanjeGrupeId { get; set; }
        [ForeignKey("Zaposlenik")]
        public int? ZaposlenikId { get; set; } //dodjeliti vodica tek kad se napuni dovoljno/maximalno raje u grupu tj pred polazak na putovanje
        public virtual Zaposlenik Zaposlenik { get; set; }
        [ForeignKey("Grupa")]
        public int GrupaId { get; set; }
        public virtual Grupa Grupa { get; set; }
        [ForeignKey("Rezervacija")]
        public int RezervacijaId { get; set; }
        public virtual Rezervacija Rezervacija { get; set; }
        public DateTime DatumPutovanja { get; set; }


    }
}
