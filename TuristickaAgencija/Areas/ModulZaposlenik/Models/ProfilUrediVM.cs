using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Models
{
    public class ProfilUrediVM
    {
        public int zaposlenikId { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage ="Polje je obavezno")]
        public string staraLozinka { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Polje je obavezno")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.[_@*<>/+-]?).{8,24}$", ErrorMessage = "Lozinka mora sadržavati: velika slova, mala slova, brojeve")]
        public string novaLozinka { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Polje je obavezno")]
        public string potrvdaLozinke { get; set; }

    }
}
