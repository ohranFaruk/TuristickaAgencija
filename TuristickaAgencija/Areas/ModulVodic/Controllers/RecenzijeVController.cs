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

    public class RecenzijeVController : Controller
    {

        private TuristickaAgencijaDB _db;

        public RecenzijeVController(TuristickaAgencijaDB turistickaAgencijaDB)
        {
            _db = turistickaAgencijaDB;
        }




        public IActionResult Index()
        {


            return View();
        }




        public IActionResult PrikazPutovanjaRec()
        {


            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();


            Zaposlenik zaposlenik = _db.Zaposlenici.Where(x => x.Korisnik.KorisnikId == korisnik.KorisnikId).SingleOrDefault();


            PrikazPutovanjaRecVM prikazPutovanjaRecVM = new PrikazPutovanjaRecVM
            {
                redovi = _db.Zaduzenja.Where(x => x.ZaposlenikId == zaposlenik.ZaposlenikId).Select(x => new PrikazPutovanjaRecVM.row
                {
                    putovanjeId=x.PutovanjeId,
                    nazivPutovanja=x.Putovanje.Grad.Naziv,
                    datumPolaska= x.Putovanje.DatumPolaska.ToString("dd.MM.yyyy"),
                    datumPovratka = x.Putovanje.DatumPovratka.ToString("dd.MM.yyyy"),
                    brojRecenzija=_db.Recenzije.Where(m=>m.Rezervacija.PutovanjeId==x.PutovanjeId).Count()



                }).ToList()
            };


            return View(prikazPutovanjaRecVM);

        }


        public IActionResult PrikazRecenzijaVIndex(int putovanjeId)
        {

            PrikazRecenzijavIndex prikazRecenzijavIndex = new PrikazRecenzijavIndex
            {
                putovanjeId = putovanjeId
            };

            return View(prikazRecenzijavIndex);
        }



        public IActionResult PrikazRecenzijaV(int putovanjeId)
        {

            PrikazRecenzijaVVM prikazRecenzijaVVM = new PrikazRecenzijaVVM
            {
                redovi = _db.Recenzije.Where(x => x.Rezervacija.PutovanjeId == putovanjeId).Select(x => new PrikazRecenzijaVVM.row
                {
                    imeTurista=x.Rezervacija.Turist.Korisnik.Ime,
                    prezimeTurista=x.Rezervacija.Turist.Korisnik.Prezime,
                    datumRecenzije=x.DatumKomentara.ToString("dd.MM.yyyy"),
                    ocjena=x.Ocjena,
                    komentar=x.Komentar,
                    recenzijaId=x.RecenzijaId,
                    putovanjeId=x.Rezervacija.PutovanjeId
                   

                }).ToList()
            };


            return View(prikazRecenzijaVVM);
        }

       
        public IActionResult Komentar(int recenzijaId)
        {
            Recenzija recenzija = _db.Recenzije.Include(x=>x.Rezervacija).Include(x=>x.Rezervacija.PutovanjeSmjestaj.Smjestaj).Include(x=>x.Rezervacija.Turist.Korisnik).Include(x=>x.Rezervacija.Putovanje.Grad).Where(x => x.RecenzijaId == recenzijaId).FirstOrDefault();


            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();


            Zaposlenik zaposlenik = _db.Zaposlenici.Include(x=>x.Korisnik).Where(x => x.Korisnik.KorisnikId == korisnik.KorisnikId).SingleOrDefault();



            KomentarVM komentarVM = new KomentarVM
            {
                datumRecenzije = recenzija.DatumKomentara.ToString("dd.MM.yyyy"),
                smjestaj = recenzija.Rezervacija.PutovanjeSmjestaj.Smjestaj.Naziv,
                ocjena = recenzija.Ocjena,
                turist = recenzija.Rezervacija.Turist.Korisnik.Ime + " " + recenzija.Rezervacija.Turist.Korisnik.Prezime,
                lokacija = recenzija.Rezervacija.Putovanje.Grad.Naziv,
               komentar = recenzija.Komentar,
               vodic=zaposlenik.Korisnik.Ime+" "+zaposlenik.Korisnik.Prezime,
               putovanjeId=recenzija.Rezervacija.PutovanjeId
            };






            return View(komentarVM);

        }





      

    }
}