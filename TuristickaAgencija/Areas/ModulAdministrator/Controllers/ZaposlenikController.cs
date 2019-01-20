using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Areas.ModulAdministrator.Helper;
using TuristickaAgencija.Areas.ModulAdministrator.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace TuristickaAgencija.Areas.ModulAdministrator.Controllers
{
    // [Autorizacija(admin: true, zaposlenik: false, turist: false)]
    public class ZaposlenikController : Controller
    {
        private TuristickaAgencijaDB _db;
        private DropDown _dropDown;
        private readonly IEmailSender _emailSender;

        public ZaposlenikController(TuristickaAgencijaDB db, IEmailSender emailSender)
        {
            _db = db;
            _dropDown = new DropDown(db);
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult PrikaziZaposlenike(string prezime = null, string ime= null, string jmbg=null, string vodici = null)
        {

            ZaposlenikIndexVM model = new ZaposlenikIndexVM
            {
                rows = _db.Zaposlenici.Include(x => x.Korisnik)
                                .OrderByDescending(x => x.ZaposlenikId)
                                .Where(x => x.Korisnik.isAktivan == true && (vodici == null || x.IsVodic == (vodici == "da"))
                                                                         && (prezime == null || x.Korisnik.Prezime.StartsWith(prezime))
                                                                         && (ime==null || x.Korisnik.Ime.StartsWith(ime))
                                                                         && (jmbg==null||x.Korisnik.JMBG.Equals(jmbg))                                                                         )
                                .Select(x => new ZaposlenikIndexVM.Row
                                {
                                    zaposlenikId = x.ZaposlenikId,
                                    datumZaposljavanja = x.DatumZaposljavanja.ToString("dd.MM.yyyy"),
                                    imePrezime = x.Korisnik.Ime + " " + x.Korisnik.Prezime,
                                    isVodic = x.IsVodic,
                                    jmbg=x.Korisnik.JMBG

                                }).ToList()

            };

            return PartialView(model);

        }
        public IActionResult PrikaziNeaktivne()
        {
            ZaposlenikIndexVM model = new ZaposlenikIndexVM
            {
                rows = _db.Zaposlenici.Include(x => x.Korisnik)
                               .OrderByDescending(x => x.ZaposlenikId)
                               .Where(x => x.Korisnik.isAktivan == false)
                               .Select(x => new ZaposlenikIndexVM.Row
                               {
                                   zaposlenikId = x.ZaposlenikId,
                                   datumZaposljavanja = x.DatumZaposljavanja.ToString("dd.MM.yyyy"),
                                   imePrezime = x.Korisnik.Ime + " " + x.Korisnik.Prezime,
                                   isVodic = x.IsVodic,
                                   jmbg = x.Korisnik.JMBG

                               }).ToList()

            };

            return PartialView("PrikaziZaposlenike",model);

        }

        public IActionResult Dodaj()
        {
            return View(GetDefaultZaposlenikDodajVM(new ZaposlenikDodajVM()));
        }

        public async Task<IActionResult> SaveAsync(ZaposlenikDodajVM vm)
        {


            if (!ModelState.IsValid)
                return View("Dodaj", GetDefaultZaposlenikDodajVM(vm));
            Korisnik k = new Korisnik {
                Ime = vm.ime,
                Prezime = vm.prezime,
                DatumRodjenja = vm.datumRodjenja,
                DatumKreiranja = DateTime.Now,
                Adresa = vm.adresa,
                GradId = vm.gradId,
                JMBG = vm.jmbg,
                KorisnickoIme = GenerisiKorisnickoIme(vm.ime,vm.prezime),
                isAktivan = true,
                Spol = vm.spol,
                isPromjenoLozinku = true,
                Lozinka=vm.lozinka
            };

            _db.Korisnici.Add(k);
            //--------------------------------
            Zaposlenik z = new Zaposlenik {
               ZaposlenikId = k.KorisnikId,
               IsVodic = vm.isVodic,
               MjeseciIskustva = vm.mjeseciIskustva,
               StepenVodicaId = _db.StepeniVodica.Where(x => x.Stepen == "Pionir").FirstOrDefault().StepenVodicaId,
               DatumZaposljavanja = vm.datumZaposljavanja,
            };

            _db.Zaposlenici.Add(z);
            //---------------------------------
            KontakPodaci kp = new KontakPodaci {
               Email = vm.email,
               Telefon = vm.telefon,
               KorisnikId = k.KorisnikId,
            };
            
            _db.KontaktPodaci.Add(kp);

            //---------------

            for (int i = 0; i < vm.odabraniJezici.Length; i++)
            {
                if(vm.odabraniJezici[i]>0)
                    _db.VodiciJezici.Add(new VodicJezik { JezikId = vm.odabraniJezici[i], ZaposlenikId = z.ZaposlenikId, Stepen = vm.odabraniStepeni[i] });
            }
            //var clearLozinka = CreatePassword();

            //var hashedLozinka = Autentifikacija.getHash(clearLozinka);
            //k.Lozinka = hashedLozinka;


            //   hashiranje lozinke
            var clearLozinka = vm.lozinka;
            var hashedLozinka = getHash(clearLozinka);
            k.Lozinka = hashedLozinka;

            _db.SaveChanges();


            await _emailSender.SendEmailAsync(kp.Email, "Wellcome to WTTA", $"Poštovani " + k.Ime + " " + k.Prezime + ",\n\ndrago nam je što Ste postali dio naše zajednice.\n" +
                "Da biste se prijavili na sistem koristite sljedeće podatke:\n\t-korisničko ime:  " + k.KorisnickoIme + "\n\t-lozinku:  " + clearLozinka + "\n\nPrilikom prve prijave na sistem zahtjevat će Vam se promjena lozinke." +
                "\n\nVaš WTTA tim!!!");




        






            return RedirectToAction(nameof(Index));
        }

        public IActionResult Pregled(int zaposlenikId)
        {
            Zaposlenik z = _db.Zaposlenici
                .Include(x => x.Korisnik)
                .Include(x => x.StepenVodica)
                .Include(x => x.Korisnik.Grad)
                .Include(x => x.Korisnik.Grad.Regija)
                .Include(x => x.Korisnik.Grad.Regija.Drzava)
                .Where(x=>x.ZaposlenikId==zaposlenikId)
                .FirstOrDefault();
            KontakPodaci k = _db.KontaktPodaci.Where(x => x.KorisnikId == z.ZaposlenikId).FirstOrDefault();
            ZaposlenikPregledVM model = new ZaposlenikPregledVM
            {
                imePrezime=z.Korisnik.Ime+" "+z.Korisnik.Prezime,
                spol=z.Korisnik.Spol,
                zaposlenikId=z.ZaposlenikId,
                stepenVodica=z.StepenVodica.Stepen,
                datumZadnjePrijave=z.Korisnik.DatumZadnjePrijave.ToString("dd.MM.yyyy HH:mm:ss"),
                adresa= z.Korisnik.Adresa+", "+z.Korisnik.Grad.Naziv+", "+ z.Korisnik.Grad.Regija.Drzava.Naziv,
                datumKreiranja =z.Korisnik.DatumKreiranja.ToString("dd.MM.yyyy HH:mm:ss"),
                datumZaposljavanja=z.DatumZaposljavanja.ToString("dd.MM.yyyy"),
                datumRodjenja=z.Korisnik.DatumRodjenja.ToString("dd.MM.yyyy"),
                email=k.Email,
                telefon=k.Telefon,
                iskustvo=z.MjeseciIskustva.ToString(),
                jmbg=z.Korisnik.JMBG,
                korisnickoIme=GenerisiKorisnickoIme(z.Korisnik.Ime,z.Korisnik.Prezime),
                vrstaZaposlenika=z.IsVodic?"Vodič":"Osoblje",
                prosjcnaOcjena=IzracunajProsjekVodica(z.ZaposlenikId).ToString(),
                isAktivan=z.Korisnik.isAktivan,
                starost=(DateTime.Now.Year-z.Korisnik.DatumRodjenja.Year).ToString()
            };

            return View(model);
        }

        public IActionResult PromjeniStatus(int zaposlenikId)
        {
            Korisnik k = _db.Korisnici.Find(zaposlenikId);
            if (k != null)
            {
                k.isAktivan = !k.isAktivan;
                _db.Korisnici.Update(k);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Pregled), new { zaposlenikId = zaposlenikId });
        }

        [HttpGet]
        public IActionResult Uredi(int zaposlenikId)
        {
            Zaposlenik x = _db.Zaposlenici.Include(z => z.Korisnik)
                                          .Include(z => z.Korisnik.Grad)
                                          .Where(z => z.ZaposlenikId == zaposlenikId).FirstOrDefault();
            KontakPodaci k = _db.KontaktPodaci.Where(f => f.KorisnikId == x.ZaposlenikId).FirstOrDefault();
            ZaposlenikUrediVM model = new ZaposlenikUrediVM
            {
                ime=x.Korisnik.Ime,
                prezime=x.Korisnik.Prezime,
                jmbg=x.Korisnik.JMBG,
                datumRodjenja=x.Korisnik.DatumRodjenja,
                datumZaposljavanja=x.DatumZaposljavanja,
                adresa=x.Korisnik.Adresa,
                kontaktId=k.KontaktId,
                email=k.Email,
                telefon=k.Telefon,
                isVodic=x.IsVodic,
                mjeseciIskustva=x.MjeseciIskustva,
                zaposlenikId=x.ZaposlenikId,
                gradId=x.Korisnik.GradId,
                gradovi = _dropDown.Gradovi(true, x.Korisnik.GradId),
                spolovi = _dropDown.Spolovi(x.Korisnik.Spol),
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Uredi(ZaposlenikUrediVM vm)
        {
            if (!ModelState.IsValid)
                return View(GetModelStateNotValidZaposlenikUrediVM(vm));
            Zaposlenik z = _db.Zaposlenici.Find(vm.zaposlenikId);
            z.DatumZaposljavanja = vm.datumZaposljavanja;
            z.IsVodic = vm.isVodic;
            z.MjeseciIskustva = vm.mjeseciIskustva;
            
            _db.Zaposlenici.Update(z);

            Korisnik k = _db.Korisnici.Find(vm.zaposlenikId);
            k.Ime = vm.ime;
            k.Prezime = vm.prezime;
            k.Adresa = vm.adresa;
            k.Spol = vm.spol;
            k.GradId = vm.gradId;
            k.DatumRodjenja = vm.datumRodjenja;
            k.JMBG = vm.jmbg;
            k.KorisnickoIme = GenerisiKorisnickoIme(vm.ime, vm.prezime);

            _db.Korisnici.Update(k);

            KontakPodaci kp = _db.KontaktPodaci.Find(vm.kontaktId);

            kp.Email = vm.email;
            kp.Telefon = vm.telefon;

            _db.KontaktPodaci.Update(kp);

            _db.SaveChanges();

            return RedirectToAction(nameof(Pregled),new {zaposlenikId=z.ZaposlenikId });

        }

        public async Task<IActionResult> ResetujSifruAsync(int zaposlenikId)
        {
            Korisnik k = _db.Korisnici.Find(zaposlenikId);
            if (k == null)
                TempData["poruka"] = "Resetovanje Lozinke nije uspjelo";
            else {
                string novaLozinka= CreatePassword();
                k.Lozinka = Autentifikacija.getHash(novaLozinka);
                _db.Korisnici.Update(k);
                _db.SaveChanges();
                TempData["poruka"] ="Uspješno ste resetovali lozinku zaposleniku: "+ k.Ime + " " + k.Prezime;
                string email = _db.KontaktPodaci.Where(x => x.KorisnikId == zaposlenikId).FirstOrDefault().Email;
                await _emailSender.SendEmailAsync(email, "Promjena Lozinke", "Poštovani " + k.Ime + " " + k.Prezime + ",\n\nVaša nova lozinka je: " + novaLozinka+"\n\nVaš WTTA tim!!!");
            }

            return PartialView();
        }

        // dolje ispod neke funkcije koje mi dodju kao pomoc

        public string CreatePassword(int length=10)
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
        private ZaposlenikDodajVM GetDefaultZaposlenikDodajVM(ZaposlenikDodajVM vm)
        {
            vm.datumRodjenja = DateTime.Now;
            vm.datumZaposljavanja = DateTime.Now;
            vm.spolovi = vm.spolovi ?? _dropDown.Spolovi();
            vm.gradovi = vm.gradovi ?? _dropDown.Gradovi();
            vm.jezici = vm.jezici ?? _dropDown.Jezici();
            vm.stepeniJezika = vm.stepeniJezika ?? _dropDown.StepeniJezika();
            return vm;
        }
        private ZaposlenikUrediVM GetModelStateNotValidZaposlenikUrediVM(ZaposlenikUrediVM vm)
        {
            vm.datumRodjenja = DateTime.Now;
            vm.datumZaposljavanja = DateTime.Now;
            vm.spolovi = vm.spolovi ?? _dropDown.Spolovi(vm.spol);
            vm.gradovi = vm.gradovi ?? _dropDown.Gradovi(true,vm.gradId);
            return vm;
        }
        public string GenerisiKorisnickoIme(string ime, string prezime)
        {
            string x = (ime + "." + prezime).ToLower().Replace("ć", "c")
                                                      .Replace("č", "c")
                                                      .Replace("š", "s")
                                                      .Replace("ž", "z")
                                                      .Replace("đ", "dj");

            return x;
        }

        public double IzracunajProsjekVodica(int zaposlenikid)
        {
            var grupePutovanja = _db.PutovanjaGrupe.Where(x => x.ZaposlenikId == zaposlenikid).ToList();
            var rezervacije = _db.Rezervacije.ToList();
            var potrebneRezervacije = new List<Rezervacija>();
            foreach (var x in grupePutovanja)
            {
                for (int i = 0; i < rezervacije.Count; i++)
                {
                    if (x.RezervacijaId == rezervacije[i].RezervacijaId)
                        potrebneRezervacije.Add(rezervacije[i]);

                }
            }

            var sveRecenzije = _db.Recenzije.ToList();
            var potrebneRecnezije = new List<Recenzija>();

            foreach (var x in potrebneRezervacije)
            {
                for (int i = 0; i < sveRecenzije.Count; i++)
                {
                    if (x.RezervacijaId == sveRecenzije[i].RezervacijaId)
                        potrebneRecnezije.Add(sveRecenzije[i]);
                }
            }

            double prosjek = potrebneRecnezije.Average(x => x.Ocjena)??0;


            return prosjek;

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