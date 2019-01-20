using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Areas.ModulZaposlenik.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Controllers
{
   // [Autorizacija(admin:false,zaposlenik:true,turist:false)]
    public class ProfilController : Controller
    {
        private TuristickaAgencijaDB _db;
        public ProfilController(TuristickaAgencijaDB db) {
            _db = db;
        }

        public IActionResult Index(int zaposlenikId)
        {
            Zaposlenik z = _db.Zaposlenici.Include(x=>x.StepenVodica).Where(x=>x.ZaposlenikId==zaposlenikId).FirstOrDefault();
            Korisnik k = _db.Korisnici.Include(x => x.Grad)
                                    .Include(x => x.Grad.Regija)
                                    .Include(x => x.Grad.Regija.Drzava)
                                    .Include(x => x.Grad.Regija.Drzava.Kontinent)
                                    .Where(x => x.KorisnikId == zaposlenikId).FirstOrDefault();
            KontakPodaci kp = _db.KontaktPodaci.Where(x => x.KorisnikId == zaposlenikId).FirstOrDefault();


            ProfilIndexVM model = new ProfilIndexVM
            {
                imePrezime=k.Ime+" "+k.Prezime,
                adresa=k.Adresa+", "+k.Grad.Naziv+", "+k.Grad.Regija.Naziv+", "+k.Grad.Regija.Drzava.Naziv,
                datumRodjenja=k.DatumRodjenja.ToString("dd.MM.yyyy"),
                email=kp.Email,
                korisnickoIme=k.KorisnickoIme,
                spol=k.Spol,
                telefon=kp.Telefon,
                vrstaZaposlenika=z.IsVodic==true?"Vodič":"Osoblje",
                zaposlenikId=z.ZaposlenikId
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult Uredi(int zaposlenikId)
        {
            Korisnik k = _db.Korisnici.Where(x => x.KorisnikId == zaposlenikId).FirstOrDefault();
            KontakPodaci kp = _db.KontaktPodaci.Where(x => x.KorisnikId == zaposlenikId).FirstOrDefault();
            if (k == null)
             return RedirectToAction("LoginPage", "Login");

            return View(new ProfilUrediVM {zaposlenikId=zaposlenikId });
        }

        

        [HttpPost]
        public IActionResult Uredi(ProfilUrediVM vm)
        {
            Korisnik k = _db.Korisnici.Where(x => x.KorisnikId == vm.zaposlenikId).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var hashStaraLozinka = Autentifikacija.getHash(vm.staraLozinka);
           
            if ((hashStaraLozinka != k.Lozinka)||(vm.novaLozinka != vm.potrvdaLozinke))
            {
                TempData["IzmjenaProfila_Greska"] = "Trenutna lozinka nije ispravna, ili se nove lozinke ne podudaraju!!!";
                return View(vm);
            }

            var hashNovaLozinka = Autentifikacija.getHash(vm.novaLozinka);
            k.Lozinka = hashNovaLozinka;
            _db.Korisnici.Update(k);

            TempData["IzmjenaProfila_Uspjeh"] = "Uspješno ste spasili izmjene!!!";

            _db.SaveChanges();
            return RedirectToAction("Index", new { zaposlenikId = vm.zaposlenikId });
        }
        [HttpGet]
        public IActionResult PrviLoginIzmjena(int zaposlenikId)
        {
            return View(new PrviLoginIzmjenaVM {zaposlenikId=zaposlenikId});
        }

        [HttpPost]
        public IActionResult PrviLoginIzmjena(PrviLoginIzmjenaVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Korisnik k = _db.Korisnici.Where(x => x.KorisnikId == vm.zaposlenikId).FirstOrDefault();
            if ((vm.novaLozinka != vm.potrvdaLozinke))
            {
                TempData["IzmjenaProfila_Greska"] = "Lozinke se ne podudaraju!!!";
                return View(vm);
            }

            var hashNovaLozinka = Autentifikacija.getHash(vm.novaLozinka);
            k.Lozinka = hashNovaLozinka;
            k.isPromjenoLozinku = true;
            _db.Korisnici.Update(k);

            _db.SaveChanges();
            Zaposlenik z = _db.Zaposlenici.Find(vm.zaposlenikId);
            if(!z.IsVodic)
                return RedirectToAction("Index","ZaposlenikHome");
            else
                return RedirectToAction("Index","VodicHome");
        }

    }
}