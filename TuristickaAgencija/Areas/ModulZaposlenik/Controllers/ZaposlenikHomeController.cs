using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Controllers
{
 //  [Autorizacija(admin:true,zaposlenik:true,turist:false)]
    public class ZaposlenikHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}