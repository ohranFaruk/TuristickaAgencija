using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Areas.ModulTurist.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulTurist.Controllers
{
   
    public class KomentariController : Controller
    {

        private TuristickaAgencijaDB _db;


        public KomentariController(TuristickaAgencijaDB turistickaAgencija)
        {
            _db = turistickaAgencija;
        }



        public IActionResult Index(int putovanjeId)
        {
            KomentarVM komentarVM;

            Putovanje putovanje = _db.Putovanja.Where(x => x.PutovanjeId == putovanjeId).SingleOrDefault();

            if (_db.Recenzije.Where(x=>x.Rezervacija.PutovanjeId==putovanjeId)==null)
            {
                 komentarVM = null;
                return Unauthorized();
            }
            else
            {
                 komentarVM = new KomentarVM
                {
                    putovanjeId = putovanjeId,
                    redovi = _db.Recenzije.Where(x => x.Rezervacija.PutovanjeId == putovanjeId).Select(x => new KomentarVM.row
                    {
                        Tekst = x.Komentar,
                        Ocjena = x.Ocjena,
                        DatumKomentara = x.DatumKomentara.ToString("dd.MM.yyyy"),
                        imeTuriste = x.Rezervacija.Turist.Korisnik.Ime + " " + x.Rezervacija.Turist.Korisnik.Prezime
                    }).ToList()
                };

            }







            return View(komentarVM);
        }
    }
}