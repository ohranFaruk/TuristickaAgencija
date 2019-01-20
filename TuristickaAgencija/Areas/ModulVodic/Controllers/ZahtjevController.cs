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


    public class ZahtjevController : Controller
    {

        private TuristickaAgencijaDB _db;

        public ZahtjevController(TuristickaAgencijaDB turistickaAgencijaDB)
        {
            _db = turistickaAgencijaDB;
        }






        public IActionResult Index(int zaduzenjeId)
        {

            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();



            Zaposlenik zaposlenik = _db.Zaposlenici.Where(x => x.Korisnik.KorisnikId == korisnik.KorisnikId).SingleOrDefault();

            ZahtjevDodaj zahtjevDodaj = new ZahtjevDodaj { zaduzenjeId=zaduzenjeId};


            return View(zahtjevDodaj);
        }

        public IActionResult Snimi(ZahtjevDodaj zahtjevDodaj)
        {


            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();


            Zaposlenik zaposlenik = _db.Zaposlenici.Where(x => x.Korisnik.KorisnikId == korisnik.KorisnikId).SingleOrDefault();

            Zaduzenje zaduzenje = _db.Zaduzenja.Include(x=>x.Putovanje.Grad).Where(x => x.ZaduzenjeId == zahtjevDodaj.zaduzenjeId).SingleOrDefault();


            Zahtjev zahtjev = new Zahtjev
            {
                datumKreiranja = DateTime.Now,
                potvrdjen = false,
                razlog = zahtjevDodaj.razlog,
                ZaposlenikId = zaposlenik.ZaposlenikId,
                lokacija = zaduzenje.Putovanje.Grad.Naziv,
                datumPotvrde=DateTime.MinValue


            };

            _db.Zahtjev.Add(zahtjev);

            zaduzenje.naCekanju = true;
            _db.SaveChanges();
     
            

            return Redirect("/ModulVodic/VodicHome");


        }

       
        public IActionResult Prikaz()
        {
            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();


            Zaposlenik zaposlenik = _db.Zaposlenici.Where(x => x.Korisnik.KorisnikId == korisnik.KorisnikId).SingleOrDefault();



            ZahtjevPrikaz zahtjevPrikaz = new ZahtjevPrikaz
            {
                redovi = _db.Zahtjev.Where(x => x.ZaposlenikId == zaposlenik.ZaposlenikId).Select(x => new ZahtjevPrikaz.row
                {
                    zahtjevId = x.ZahtjevId,
                    datumKreiranja = x.datumKreiranja.ToString("dd.MM.yyyy"),
                    razlog = x.razlog,
                    potvrdjen = x.potvrdjen,
                    putovanje = x.lokacija,
                    datumPotvrde=x.datumPotvrde.ToString("dd.MM.yyyy")
                    





                }).ToList()
            };



            return View(zahtjevPrikaz);


        }




    }
}