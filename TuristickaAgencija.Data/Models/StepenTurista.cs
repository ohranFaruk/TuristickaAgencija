using System;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Data.Models
{
    public class StepenTurista
    {
        [Key]
        public int StepenTuristaId { get; set; }
        public string Stepen { get; set; }
    }
}