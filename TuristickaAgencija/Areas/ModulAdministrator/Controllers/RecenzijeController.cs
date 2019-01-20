using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Areas.ModulAdministrator.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulAdministrator.Controllers
{
    //  [Autorizacija(admin: true, zaposlenik: false, turist: false)]
    public class RecenzijeController : Controller
    {

        private TuristickaAgencijaDB _db;

        public RecenzijeController(TuristickaAgencijaDB db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            RecenzijaPretragaVM model = new RecenzijaPretragaVM
            {
                putovanja=_db.Putovanja.Include(x=>x.Grad)
                                       .Include(x=>x.Grad.Regija)
                                       .Include(x=>x.Grad.Regija.Drzava)
                                       .Include(x=>x.Grad.Regija.Drzava.Kontinent)
                                       .Select(x=>new SelectListItem {
                                           Value=x.PutovanjeId.ToString(),
                                           Text=x.Grad.Naziv+ " | "+x.Grad.Regija.Drzava.Naziv+" | "+x.Grad.Regija.Drzava.Kontinent.Naziv

                                       }).ToList(),
                vodici=_db.Zaposlenici.Include(x=>x.Korisnik).Where(x=>x.IsVodic==true).Select(x=> new SelectListItem {
                    Value=x.ZaposlenikId.ToString(),
                    Text=x.Korisnik.isAktivan?x.Korisnik.Ime+" "+x.Korisnik.Prezime: x.Korisnik.Ime + " " + x.Korisnik.Prezime+"   X"

                }).ToList()
            };
            model.vodici.Insert(0, new SelectListItem { Value = "null", Text = ">>Odaberi vodiča<<" });
            model.putovanja.Insert(0, new SelectListItem { Value = "null", Text = ">>Odaberi putovanje<<" });


            return View(model);
        }

        public IActionResult PrikaziRecenzije(RecenzijaPretragaVM vm)
        {
            RecenzijaIndexVM model = new RecenzijaIndexVM
            {

                divs = _db.Recenzije.Include(x => x.Rezervacija)
                                  .Include(x => x.Rezervacija.Putovanje)
                                  .Include(x => x.Rezervacija.Putovanje.Grad)
                                  .Include(x => x.Rezervacija.Putovanje.Grad.Regija)
                                  .Include(x => x.Rezervacija.Putovanje.Grad.Regija.Drzava)
                                  .Include(x => x.Rezervacija.Putovanje.Grad.Regija.Drzava.Kontinent)
                                  .Include(x=>x.Rezervacija.PutovanjeSmjestaj)
                                  .Include(x => x.Rezervacija.PutovanjeSmjestaj.Smjestaj)
                                  .Include(x=>x.Rezervacija.Turist)
                                  .Include(x => x.Rezervacija.Turist.Korisnik)
                                  .Where(x => (vm.putovanjeId == null || x.Rezervacija.PutovanjeId == vm.putovanjeId) && (vm.vodicId == null || _db.PutovanjaGrupe.Where(pg => pg.RezervacijaId == x.RezervacijaId).FirstOrDefault().ZaposlenikId == vm.vodicId))
                                  .Select(x => new RecenzijaIndexVM.Div
                                  {
                                      datumRecenzije = x.DatumKomentara.ToString("dd.MM.yyyy"),
                                      komentar = x.Komentar,
                                      lokacija = x.Rezervacija.Putovanje.Grad.Naziv + " | " + x.Rezervacija.Putovanje.Grad.Regija.Drzava.Naziv + " | " + x.Rezervacija.Putovanje.Grad.Regija.Drzava.Kontinent.Naziv,
                                      ocjena = x.Ocjena == null ? "N/A" : x.Ocjena.ToString(),
                                      recenzijaId = x.RecenzijaId,
                                      turist =x.Rezervacija.Turist.Korisnik.Ime + " " + x.Rezervacija.Turist.Korisnik.Prezime,
                                      smjestaj =x.Rezervacija.PutovanjeSmjestaj.Smjestaj.Naziv,
                                      rezervacijaId=x.RezervacijaId
                                  }).ToList()
            };
            foreach (var x in model.divs)
            {
                var pg = _db.PutovanjaGrupe.Include(p => p.Zaposlenik).Include(p => p.Zaposlenik.Korisnik).Where(p => p.RezervacijaId == x.rezervacijaId).FirstOrDefault();
                x.vodic = pg.Zaposlenik.Korisnik.Ime + " " + pg.Zaposlenik.Korisnik.Prezime;
            }


            return PartialView(model);
        }
        public IActionResult IzlistajRecenzije(int zaposlenikId)
        {
            TempData["vodicId"] = zaposlenikId;

            return View();
        }



        public IActionResult Pregled(int recenzijaId)
        {
            Recenzija r = _db.Recenzije.Include(x => x.Rezervacija)
                                     .Include(x => x.Rezervacija.Putovanje)
                                     .Include(x => x.Rezervacija.Putovanje.Grad)
                                     .Include(x => x.Rezervacija.Putovanje.Grad.Regija)
                                     .Include(x => x.Rezervacija.Putovanje.Grad.Regija.Drzava)
                                     .Include(x => x.Rezervacija.Putovanje.Grad.Regija.Drzava.Kontinent)
                                     .Include(x => x.Rezervacija.PutovanjeSmjestaj)
                                     .Include(x => x.Rezervacija.PutovanjeSmjestaj.Smjestaj)
                                     .Include(x => x.Rezervacija.Turist)
                                     .Include(x => x.Rezervacija.Turist.Korisnik).Where(x => x.RecenzijaId == recenzijaId).FirstOrDefault();
            var vodic = _db.PutovanjaGrupe.Include(x => x.Zaposlenik).Include(x => x.Zaposlenik.Korisnik).Where(x => x.RezervacijaId == r.RezervacijaId).FirstOrDefault();
            RecenzijaIndexVM.Div model = new RecenzijaIndexVM.Div
            {
                datumRecenzije=r.DatumKomentara.ToString("dd.MM.yyyy"),
                smjestaj=r.Rezervacija.PutovanjeSmjestaj.Smjestaj.Naziv,
                komentar=r.Komentar,
                lokacija=r.Rezervacija.Putovanje.Grad.Naziv+" | "+r.Rezervacija.Putovanje.Grad.Regija.Drzava.Naziv + " | " + r.Rezervacija.Putovanje.Grad.Regija.Drzava.Kontinent.Naziv,
                ocjena=r.Ocjena.ToString(),
                recenzijaId=r.RecenzijaId,
                rezervacijaId=r.RezervacijaId,
                turist=r.Rezervacija.Turist.Korisnik.Ime+" "+r.Rezervacija.Turist.Korisnik.Prezime,
                vodic=vodic.Zaposlenik.Korisnik.Ime+" "+vodic.Zaposlenik.Korisnik.Prezime
            };



            return PartialView(model);
        }


    }
}