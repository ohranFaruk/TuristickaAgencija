using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Areas.ModulVodic.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace TuristickaAgencija.Areas.ModulVodic.Controllers
{







    public class ZaduzenjeController : Controller
    {

        private TuristickaAgencijaDB _db;

        public ZaduzenjeController(TuristickaAgencijaDB turistickaAgencijaDB)
        {
            _db = turistickaAgencijaDB;
        }


        //public IActionResult Index()
        //{

        //    Korisnik k = HttpContext.GetLogiraniKorisnik();
        //    if (k==null)
        //    {
        //        Redirect("/login/loginpage");
        //    }



        //    return View();
        //}

        public IActionResult PrikazZaduzenja()
        {


            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();
            if (korisnik==null)
            {
                Redirect("/login/loginpage");

            }


            Zaposlenik zaposlenik = _db.Zaposlenici.Where(x => x.Korisnik.KorisnikId == korisnik.KorisnikId).SingleOrDefault();

            ZaduzenjePrikazVM prikavVM = new ZaduzenjePrikazVM
            {
                redovi = _db.Zaduzenja.Where(x => x.ZaposlenikId == zaposlenik.ZaposlenikId).Select(x => new ZaduzenjePrikazVM.row
                {
                    zaduzenjeId = x.ZaduzenjeId,
                    nazivPutovanja = x.Putovanje.Grad.Naziv,
                    trenutnoTurista = _db.Rezervacije.Where(m => m.PutovanjeId == x.PutovanjeId).Count(),
                    datumPolaska = x.Putovanje.DatumPolaska.ToString("dd.MM.yyyy"),
                    datumPovratka = x.Putovanje.DatumPovratka.ToString("dd.MM.yyyy"),
                    putovanjeId = x.PutovanjeId,
                    opis = x.opis,
                    naCekanju=x.naCekanju



                }).ToList()


            };


            return View(prikavVM);



             }
        }
}