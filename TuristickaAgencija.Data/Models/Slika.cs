using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TuristickaAgencija.Data.Models
{
    public class Slika
    {
        [Key]
        public int SlikaId { get; set; }
        [ForeignKey(nameof(Putovanje))]
        public int? PutovanjeId { get; set; }
        public virtual Putovanje Putovanje{ get; set; }
        public int? SmjestajId { get; set; }
        public virtual Smjestaj Smjestaj { get; set; }
        public byte[] Image { get; set; }
        public string imgType { get; set; }

        public int? PonudaId { get; set; }
        public virtual Ponuda Ponuda { get; set; }

    }
}
