using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class PutovanjeSmjestaj
    {
        [Key]
        public int PutovanjeSmjestajId { get; set; }
        [ForeignKey("Putovanje")]
        public int PutovanjeId { get; set; }
        public virtual Putovanje Putovanje { get; set; }
        [ForeignKey("Smjestaj")]
        public int SmjestajId { get; set; }
        public virtual Smjestaj Smjestaj { get; set; }
    }
}
