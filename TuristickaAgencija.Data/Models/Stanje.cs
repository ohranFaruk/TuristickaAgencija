using System;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Data.Models
{
    public class Stanje
    {
        [Key]
        public int StanjeId { get; set; }
        public string StanjeRezervacije { get; set; }
    }
}