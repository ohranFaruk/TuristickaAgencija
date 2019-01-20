using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class PutovanjeDodjeliVodicaVM
    {
        public int putovanjeId { get; set; }
        public int grupaId { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Morate prvo odabrati vodiča!!!")]
        public int vodicId { get; set; }
        public List<SelectListItem> vodici;
        public string grupa;

    }
}
