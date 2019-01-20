using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;
using TuristickaAgencija.ViewModels;

namespace TuristickaAgencija.Controllers
{
    public class LoginController : Controller
    {
        private TuristickaAgencijaDB _db;


        public LoginController(TuristickaAgencijaDB db) {

            _db = db;
        }
        public IActionResult LoginPage(string url)
        {

            LoginPageVM model = new LoginPageVM();
            model.url=url;
            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();
       
            if (korisnik!=null)
            {
                return Redirect(url);

            }
            return View(model);

        }
        [HttpPost]

        #region prijava
        public IActionResult Prijava(LoginPageVM vm) {

            if (!ModelState.IsValid)
                return View("LoginPage", vm);

            var hashLozinka = Autentifikacija.getHash(vm.lozinka);
          
          
            Korisnik korisnik = _db.Korisnici.Where(x => x.KorisnickoIme == vm.korisnickoIme&& x.Lozinka==hashLozinka).FirstOrDefault();
            if (korisnik != null)
            {
               

                korisnik.DatumZadnjePrijave = DateTime.Now;
                _db.Korisnici.Update(korisnik);
                _db.SaveChanges();



                HttpContext.SetLogiraniKorisnik(korisnik, vm.zapamtiLozinku);
                //if (!korisnik.isPromjenoLozinku)
                //{
                //    //za faruka komentar: prilikom dodavanja tj registracije novog turista odmah mu postavi "isPromjenoLozinku=true;"
                //    return RedirectToAction("PrviLoginIzmjena", new {controller = "Profil", area = "ModulZaposlenik",zaposlenikId=korisnik.KorisnikId });
                //}

              


                Zaposlenik zaposlenik = _db.Zaposlenici.Where(x => x.Korisnik.KorisnikId == korisnik.KorisnikId).SingleOrDefault();
                if (zaposlenik != null)
                    return RedirectToAction("Index", new { controller="VodicHome",area= "ModulVodic" });
                else 
                if(zaposlenik != null && !zaposlenik.IsVodic)
                    return RedirectToAction("Index", new {controller="ZaposlenikHome", area="ModulZaposlenik" });

                Turist turist = _db.Turisti.Find(korisnik.KorisnikId);
                if (turist != null)
                    return Redirect(vm.url);

                
            }

     


            //if (korisnik.isAdmin)
            //    return RedirectToAction("Index", new {controller="AdminHome", area="ModulAdministrator" });

            TempData["pogresanLogin"] = "Korisničko ime ili lozinka nisu ispravni";
            return View("LoginPage", vm);
        }

       
        #endregion

        public IActionResult Provjera(LoginPageVM vm)
        {

       


           return Redirect(vm.url);

        }



        public IActionResult Odjava(string url)
        {
            HttpContext.Session.Clear();

            return Redirect(url);
        }
    }
}