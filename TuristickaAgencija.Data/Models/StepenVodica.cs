using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Data.Models
{
    public class StepenVodica
    {
        [Key]
        public int StepenVodicaId { get; set; }
        public string Stepen { get; set; }
    }
}
