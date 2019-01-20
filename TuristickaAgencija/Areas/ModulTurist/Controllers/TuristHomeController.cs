using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Areas.ModulTurist.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulTurist.Controllers
{
    public class TuristHomeController : Controller
    {

        private TuristickaAgencijaDB _db;
        private readonly IEmailSender _emailSender;

        public TuristHomeController(TuristickaAgencijaDB turistickaAgencijaDB,IEmailSender emailSender)
        {
            _db = turistickaAgencijaDB;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
            //test 
        }


        public IActionResult Registracija()
        {

            RegistracijaVM registracijaVM = new RegistracijaVM();

            registracijaVM.gradovi = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            registracijaVM.gradovi.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "null", Text = "Odaberite grad" });
            registracijaVM.gradovi.AddRange(_db.Gradovi.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem {Value=x.GradId.ToString(),Text=x.Naziv }).ToList());


            registracijaVM.spol = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            registracijaVM.spol.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "null", Text = "Odaberite spol" });
            registracijaVM.spol.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "M", Text = "Musko" });
            registracijaVM.spol.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Ž", Text = "Zensko" });



            return View(registracijaVM);
            //test 
        }


        public async Task<IActionResult> SaveAsync(RegistracijaVM registracijaVM)
        {

            Korisnik k = new Korisnik
            {
                Ime = registracijaVM.ime,
                Prezime = registracijaVM.prezime,
                Adresa = registracijaVM.Adresa,
                DatumRodjenja = registracijaVM.DatumRodjenja,
                DatumKreiranja = DateTime.Now,
               
                GradId = registracijaVM.gradId,
                JMBG = registracijaVM.JMBG,
                KorisnickoIme = GenerisiKorisnickoIme(registracijaVM.ime, registracijaVM.prezime),
                isAktivan = true,
                Spol = registracijaVM.spol.ToString()=="M"?"M":"Z",
                isPromjenoLozinku = true,
                Lozinka=registracijaVM.lozinka
               
            };
            _db.Korisnici.Add(k);
            ///////////////////////////////

            Turist turist = new Turist
            {
                TuristId = k.KorisnikId,
                Index = "TR" + k.KorisnikId.ToString(),
                StepenTuristaId=1


            };
            _db.Turisti.Add(turist);
            ///////////////////////////


            KontakPodaci kp = new KontakPodaci
            {
                Email = registracijaVM.email,
                Telefon = registracijaVM.Telefon,
                KorisnikId = k.KorisnikId,
            };
            _db.KontaktPodaci.Add(kp);



            //   hashiranje lozinke
            var clearLozinka = registracijaVM.lozinka;
            var hashedLozinka = getHash(clearLozinka);
            k.Lozinka = hashedLozinka;




            //slanje mejla 
            await _emailSender.SendEmailAsync(kp.Email, "Konfirmacija korisnickog racuna ", $"Poštovani " + k.Ime + " " + k.Prezime + ",\n\nDrago nam je što ste postali dio naše zajednice.\n" +
                "Da biste se prijavili na sistem koristite sljedeće podatke:\n\n\t-korisničko ime:  " + k.KorisnickoIme + "\n\t-lozinku:  " + clearLozinka+"\n\nVaš WTTA tim !");




            _db.SaveChanges();

            return RedirectToAction("index");
        }


      public IActionResult Prijava()
        {
            PrijavaVM prijavaVM = new PrijavaVM();

            return View(prijavaVM);



           
        }


        public  IActionResult provjera(PrijavaVM prijavaVM)
        {
            if (_db.Rezervacije.Where(x=>x.imePutnika==prijavaVM.Ime&&prijavaVM.Prezime==x.prezimePutnika).Count()==0)
            {
                TempData["nema putovanja"] = "Niste putovali sa nama";

                return View("Prijava");

            }

            return Redirect("/modulturist/putovanjetur/PrikazZavrsenihPutovanja?ime="+prijavaVM.Ime+"&prezime="+prijavaVM.Prezime);
        }







        //funkcije za pomoc
        public string GenerisiKorisnickoIme(string ime, string prezime)
        {
            string x = (ime + "." + prezime).ToLower().Replace("ć", "c")
                                                      .Replace("č", "c")
                                                      .Replace("š", "s")
                                                      .Replace("ž", "z")
                                                      .Replace("đ", "dj");

            return x;
        }

        public string CreatePassword(int length = 10)
        {
            const string valid = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ1234567890_?#*";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static string getHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }




    }
}