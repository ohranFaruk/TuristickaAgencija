using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TuristickaAgencija.Data.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string Naziv { get; set; }

    }
}
