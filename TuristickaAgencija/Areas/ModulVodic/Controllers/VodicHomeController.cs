using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Areas.ModulVodic.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulVodic.Controllers
{
    [Autorizacija(admin: false, zaposlenik: true, turist: false)]

    public class VodicHomeController : Controller
    {

        private TuristickaAgencijaDB _db;

        public VodicHomeController(TuristickaAgencijaDB turistickaAgencijaDB)
        {
            _db = turistickaAgencijaDB;
        }

        public IActionResult Index()
        {

            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();


            Zaposlenik zaposlenik = _db.Zaposlenici.Include(x=>x.Korisnik).Where(x => x.Korisnik.KorisnikId == korisnik.KorisnikId).SingleOrDefault();


            VodicHomeIndexVM model = new VodicHomeIndexVM
            {
                zaduzenja = _db.Zaduzenja.Where(x => x.ZaposlenikId == zaposlenik.ZaposlenikId).Count(),
                zahtjevi = _db.Zahtjev.Where(x => x.ZaposlenikId == zaposlenik.ZaposlenikId).Count(),
                ime=zaposlenik.Korisnik.Ime+" "+zaposlenik.Korisnik.Prezime
              
            };


            return View(model);
        }


        public int? racunanjeRecenzija(int putovanjeId, int zaposlenikId)
        {
            int? recenzije = 0;

            foreach (var x in _db.Zahtjev.Where(x => x.ZaposlenikId == zaposlenikId).ToList())
            {
                recenzije += _db.Recenzije.Where(m => m.Rezervacija.PutovanjeId == putovanjeId).Count();

            }





            return recenzije;

        }
    }
}