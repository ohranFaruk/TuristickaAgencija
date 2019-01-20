using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulAdministrator.Controllers
{
    public class AdminHomeController : Controller
    {
       // [Autorizacija(admin: true, zaposlenik: false,turist:false)]
        public IActionResult Index()
        {
            return View();
        }
    }
}