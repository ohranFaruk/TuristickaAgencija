﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Areas.ModulTurist.Models
{
    public class LoginVM
    {
        [StringLength(100, ErrorMessage = "Korisničko ime mora sadržavati mininalno 3 karaktera.", MinimumLength = 3)]
        public string username { get; set; }
        [StringLength(100, ErrorMessage = "Password mora sadržavati mininalno 4 karaktera.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
