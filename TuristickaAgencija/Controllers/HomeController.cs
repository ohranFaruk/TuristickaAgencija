using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;

namespace TuristickaAgencija
{
    public class HomeController : Controller
    {
        private TuristickaAgencijaDB _db;

        public HomeController(TuristickaAgencijaDB db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Napuni()
        {
            if (!_db.Drzave.Any())
                DBFirstData.Popuni(_db);
            return RedirectToAction("LoginPage", "Login");
        }
    }
}
