using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Areas.ModulAdministrator.Helper;
using TuristickaAgencija.Areas.ModulZaposlenik.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.ViewModels;
using Nexmo.Api;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Controllers
{
   // [Autorizacija(admin:true,zaposlenik:true,turist:false)]
    public class PutovanjeController : Controller
    {
        private TuristickaAgencijaDB _db;
        private DropDown _dropdown;
        public PutovanjeController(TuristickaAgencijaDB db)
        {
            _db = db;
            _dropdown = new DropDown(db);
        }

        public IActionResult Index()
        {
            PutovanjeIndexVM model = new PutovanjeIndexVM
            {
                gradovi=_dropdown.Gradovi(),
                drzave=_dropdown.Drzave(),
                kontinenti=_dropdown.Kontinenti()
            };
            return View(model);
        }

        public IActionResult PrikaziPutovanja(PutovanjeIndexVM vm, string aktivna="da")
        {
            if (vm.drzavaId == 0)
                vm.drzavaId = null;
            if (vm.kontinentId == 0)
                vm.kontinentId = null;
            if (vm.gradId == 0)
                vm.gradId = null;
            PutovanjePrikaziVM model = new PutovanjePrikaziVM
            {
                divs = _db.Putovanja.Include(x=>x.Grad)
                                    .Include(x=>x.Grad.Regija)
                                    .Include(x => x.Grad.Regija.Drzava)
                                    .Include(x => x.Grad.Regija.Drzava.Kontinent)
                                    .Where(x=>x.isAktivno==(aktivna=="da") && (vm.gradId==null || x.GradId==vm.gradId) && (vm.drzavaId==null||x.Grad.Regija.Drzava.DrzavaId==vm.drzavaId) && (vm.kontinentId==null||x.Grad.Regija.Drzava.Kontinent.KontinentId==vm.kontinentId))
                .Select(x => new PutovanjePrikaziVM.Div
                {
                    putovanjeId = x.PutovanjeId,
                    trajanje=(x.DatumPovratka.Subtract(x.DatumPolaska).Days).ToString(),
                    cijena=(x.Cijena-(x.Cijena*(x.Popust??0)/100)).ToString("F2")+" KM"

                }).ToList()

            };
            foreach (var x in model.divs)
            {
                x.lokacija = getLokacija(x.putovanjeId);
                x.slika = getCoverSlikaSrc(x.putovanjeId);
            }
            return PartialView(model);
        }


        [HttpGet]
        public IActionResult Dodaj()
        {
            PutovanjeDodajVM model = new PutovanjeDodajVM
            {
                datumPovratka = DateTime.Now.Date,
                datumPolaska = DateTime.Now.Date,
                prevozi = _dropdown.PrevoznaSredstva(),
                ponude = _dropdown.Ponude(),
                gradovi = _dropdown.Gradovi()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DodajAsync(PutovanjeDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.gradovi = _dropdown.Gradovi(true, vm.gradId);
                vm.prevozi = _dropdown.PrevoznaSredstva(true, vm.prevozId);
                vm.ponude = _dropdown.Ponude(true,vm.ponudaId);
                return View("Dodaj",vm);
            }
            if (vm.ponudaId == 0)
                vm.ponudaId = null;
            Putovanje p = new Putovanje
            {
                Cijena = vm.cijena,
                GradId = vm.gradId,
                PrevoznoSredstvoId = vm.prevozId,
                Popust = vm.popust,
                DatumPolaska = vm.datumPolaska,
                DatumPovratka = vm.datumPovratka,
                isAktivno = true,
                Opis=vm.Opis,
                PonudaId=vm.ponudaId,
                DatumIzmjene=DateTime.Now
            };

            _db.Putovanja.Add(p);
            _db.SaveChanges();

            var smjestaji = _db.Smjestaji.Where(x => x.GradId == p.GradId).ToList();
            foreach (var x in smjestaji)
            {
                _db.PutvanjaSmjestaji.Add(new PutovanjeSmjestaj { PutovanjeId = p.PutovanjeId, SmjestajId = x.SmjestajId });
            }

            foreach (var x in vm.slike)
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (x.Length > 0)
                    {
                        await x.CopyToAsync(memoryStream);

                        byte[] slika = memoryStream.ToArray();

                        _db.Slike.Add(new Slika { Image=slika,PutovanjeId=p.PutovanjeId,imgType=x.ContentType});
                    }
                }
            }

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Uredi(int putovanjeId)
        {
            Putovanje p = _db.Putovanja.Include(x => x.Grad).Where(x => x.PutovanjeId == putovanjeId).FirstOrDefault();

            PutovanjeUrediVM model = new PutovanjeUrediVM {
                  prevozi=_dropdown.PrevoznaSredstva(true,p.PrevoznoSredstvoId),
                  ponude=_dropdown.Ponude(true,p.PonudaId),
                  prevozId=p.PrevoznoSredstvoId,
                  ponudaId=p.PonudaId,
                  cijena=p.Cijena,
                  popust=p.Popust,
                  Opis=p.Opis,
                  datumPolaska=p.DatumPolaska,
                  datumPovratka=p.DatumPovratka,
                  grad=p.Grad.Naziv,
                  putovanjeId=p.PutovanjeId
            };


            return View(model);
        }
        [HttpPost]
        public IActionResult Uredi(PutovanjeUrediVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ponude = _dropdown.Ponude(true, vm.ponudaId);
                vm.prevozi = _dropdown.PrevoznaSredstva(true, vm.prevozId);
                vm.grad = _db.Putovanja.Include(x => x.Grad)
                                       .Where(x=>x.PutovanjeId==vm.putovanjeId)
                                       .FirstOrDefault().Grad.Naziv;
                return View(vm);
            }

            Putovanje p = _db.Putovanja.Find(vm.putovanjeId);

            bool noviCiklus = (p.DatumPovratka < DateTime.Now.Date);

            if (noviCiklus)
                p.DatumKreiranja = DateTime.Now.Date;


            if (!noviCiklus)
            {
                var turistiUgrupi = _db.PutovanjaGrupe.Include(x=>x.Rezervacija).Where(x => x.DatumPutovanja == p.DatumPolaska && x.Rezervacija.PutovanjeId==p.PutovanjeId).ToList();
                foreach (var x in turistiUgrupi)
                {
                    x.DatumPutovanja = vm.datumPolaska;
                    _db.PutovanjaGrupe.Update(x);
                }

            }

            if (vm.ponudaId == 0)
                vm.ponudaId = null;
            p.Opis = vm.Opis;
            p.PonudaId = vm.ponudaId;
            p.PrevoznoSredstvoId = vm.prevozId;
            p.Popust = vm.popust;
            p.Cijena = vm.cijena;
            p.DatumPolaska = vm.datumPolaska;
            p.DatumPovratka = vm.datumPovratka;
            p.DatumIzmjene = DateTime.Now;

            _db.Putovanja.Update(p);
            _db.SaveChanges();
            return RedirectToAction("Pregled", new { putovanjeId = p.PutovanjeId });
        }

        public IActionResult PrikaziRezervacije(int putovanjeId)
        {
            PutovanjeRezervacijeVM model = new PutovanjeRezervacijeVM
            {
                rows = _db.Rezervacije.Include(x => x.PutovanjeSmjestaj)
                                    .Include(x => x.PutovanjeSmjestaj.Smjestaj)
                                    .Include(x => x.Turist)
                                    .Include(x => x.Putovanje)
                                    .Include(x => x.Turist.Korisnik)
                                    .Include(x => x.Stanje)
                                    .OrderBy(x => x.DatumRezervacije)
                                    .Where(x => x.PutovanjeId == putovanjeId && x.DatumRezervacije >= x.Putovanje.DatumKreiranja && x.Stanje.StanjeRezervacije == "Plaćena")
                                    .Select(x => new PutovanjeRezervacijeVM.Row {
                                        imePrezime = x.Turist.Korisnik.Ime + " " + x.Turist.Korisnik.Prezime,
                                        spol = x.Turist.Korisnik.Spol,
                                        smjestaj = x.PutovanjeSmjestajId == null ? "Lično" : x.PutovanjeSmjestaj.Smjestaj.Naziv,
                                        datum = x.DatumRezervacije.ToString("dd.MM.yyyy"),
                                        ukupanIznos = x.UkupanIznos.ToString(),
                                        rezervacijaId = x.RezervacijaId
                                    }).ToList(),
                putovanjeId = putovanjeId,
                datumPutovanja = _db.Putovanja.Find(putovanjeId).DatumPolaska,
            };
            //Radi kako treba :D 
            return View(model);
        }


        public IActionResult PregledGrupa(int putovanjeId)
        {
            var rezervacije = _db.PutovanjaGrupe.Include(x => x.Zaposlenik)
                                                .Include(x=>x.Zaposlenik.Korisnik)
                                                .Include(x => x.Rezervacija)
                                                .Include(x => x.Rezervacija.Turist)
                                                .Include(x => x.Rezervacija.Turist.Korisnik)
                                                .Include(x => x.Grupa)
                                                .Where(x=>x.Rezervacija.PutovanjeId==putovanjeId  && x.Rezervacija.Putovanje.DatumPolaska==x.DatumPutovanja)
                                                .ToList();
                                               

            PutovanjeGrupeVM model = new PutovanjeGrupeVM
            {
                rowGrupa1 = rezervacije.Where(x=>x.Grupa.Naziv=="G1").Select(x=>new PutovanjeGrupeVM.Row {
                    imePrezime=x.Rezervacija.Turist.Korisnik.Ime+" "+x.Rezervacija.Turist.Korisnik.Prezime,
                    spol=x.Rezervacija.Turist.Korisnik.Spol,
                    starost=((DateTime.Now.Year)-(x.Rezervacija.Turist.Korisnik.DatumRodjenja.Year)).ToString(),
                    vodic=x.ZaposlenikId==null?"N/A":x.Zaposlenik.Korisnik.Ime+" "+x.Zaposlenik.Korisnik.Prezime
                }).ToList(),
                rowGrupa2 = rezervacije.Where(x => x.Grupa.Naziv == "G2").Select(x => new PutovanjeGrupeVM.Row
                {
                    imePrezime = x.Rezervacija.Turist.Korisnik.Ime + " " + x.Rezervacija.Turist.Korisnik.Prezime,
                    spol = x.Rezervacija.Turist.Korisnik.Spol,
                    starost = ((DateTime.Now.Year) - (x.Rezervacija.Turist.Korisnik.DatumRodjenja.Year)).ToString(),
                    vodic = x.ZaposlenikId == null ? "N/A" : x.Zaposlenik.Korisnik.Ime + " " + x.Zaposlenik.Korisnik.Prezime

                }).ToList(),
                rowGrupa3 = rezervacije.Where(x => x.Grupa.Naziv == "G3").Select(x => new PutovanjeGrupeVM.Row
                {
                    imePrezime = x.Rezervacija.Turist.Korisnik.Ime + " " + x.Rezervacija.Turist.Korisnik.Prezime,
                    spol = x.Rezervacija.Turist.Korisnik.Spol,
                    starost = ((DateTime.Now.Year) - (x.Rezervacija.Turist.Korisnik.DatumRodjenja.Year)).ToString(),
                    vodic = x.ZaposlenikId == null ? "N/A" : x.Zaposlenik.Korisnik.Ime + " " + x.Zaposlenik.Korisnik.Prezime
                }).ToList(),
                putovanjeId=putovanjeId
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult DodjeliVodica(int putovanjeId, string nazivGrupe)
        {
            PutovanjeDodjeliVodicaVM model = new PutovanjeDodjeliVodicaVM
            {
                putovanjeId = putovanjeId,
                vodici=_db.Zaposlenici.Include(x=>x.Korisnik)
                                      .Include(x=>x.StepenVodica)
                                      .Where(x=>x.IsVodic==true)
                                      .Select(x=>new SelectListItem {
                                          Value=x.ZaposlenikId.ToString(),
                                          Text =x.Korisnik.Ime+" "+x.Korisnik.Prezime+" | "+x.StepenVodica.Stepen
                                      }).ToList(),
                grupa=nazivGrupe
            };
            model.grupaId = _db.Grupe.Where(x => x.Naziv == nazivGrupe).FirstOrDefault().GrupaId;
            model.vodici.Insert(0, new SelectListItem { Value = "0", Text = ">>Odaberi vodiča<<" });
            return View(model);
        }
        [HttpPost]
        public IActionResult DodjeliVodica(PutovanjeDodjeliVodicaVM vm)
        {

            if (!ModelState.IsValid)
                return View(vm.grupa = _db.Grupe.Where(x => x.GrupaId == vm.grupaId).FirstOrDefault().Naziv);

            Putovanje p = _db.Putovanja.Find(vm.putovanjeId);
            var turistiUGrupi = _db.PutovanjaGrupe.Where(x => x.DatumPutovanja == p.DatumPolaska && x.GrupaId==vm.grupaId).ToList();

            foreach (var x in turistiUGrupi)
            {
                x.ZaposlenikId = vm.vodicId;
                _db.PutovanjaGrupe.Update(x);
            }
            _db.SaveChanges();


            //implementirat SMS slanje poruka svim turistima o informacijama za putovanje 

            //var turistiIds = _db.PutovanjaGrupe.Include(x => x.Rezervacija).Select(x => x.Rezervacija.TuristId).ToList();

            //var kontaktTelefoni = new List<string>();

            //var sviKontakti = _db.KontaktPodaci.ToList();

            //foreach (var x in turistiIds)
            //{
            //    foreach (var z in sviKontakti)
            //    {
            //        if (x == z.KorisnikId)
            //            kontaktTelefoni.Add(z.Telefon);
            //    }
            //}
            //var nazivGrupe = _db.Grupe.Where(x => x.GrupaId == vm.grupaId).FirstOrDefault().Naziv;
            //Korisnik vodic = _db.Korisnici.Find(vm.vodicId);

            //SMS.Send(new SMS.SMSRequest
            //{
            //    from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
            //    to="063297255",
            //    text="Dodjeljeni ste u grupu: "+nazivGrupe+".\nVaš vodič je: "+vodic.Ime+" "+vodic.Prezime+"\nVaš WTTA tim."
            //});
            //SMS.Send(new SMS.SMSRequest
            //{
            //    from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
            //    to = "0603075038",
            //    text = "Dodjeljeni ste u grupu: " + nazivGrupe + ".\nVaš vodič je: " + vodic.Ime + " " + vodic.Prezime + "\nVaš WTTA tim.\nPS. eto me da igramo lige!!!"
            //});
            //foreach (var x in kontaktTelefoni)
            //{
            //    SMS.Send(new SMS.SMSRequest
            //    {
            //        from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
            //        to = x,
            //        text = "Dodjeljeni ste u grupu: " + nazivGrupe + ".\nVaš vodič je: " + vodic.Ime + " " + vodic.Prezime + "\nVaš WTTA tim."
            //    });
            //}

            return RedirectToAction("PregledGrupa", new { putovanjeId = vm.putovanjeId });
        }



        public IActionResult KreirajGrupe(int putovanjeId)
        {
            var rezervacije = _db.Rezervacije.Include(x => x.Turist)
                                             .Include(x => x.Turist.Korisnik)
                                             .Include(x=>x.Putovanje)
                                             .Where(x => x.PutovanjeId == putovanjeId && x.DatumRezervacije >= x.Putovanje.DatumKreiranja && x.Stanje.StanjeRezervacije == "Plaćena")
                                             .ToList();

            int brojac = rezervacije.Count();
            int idGrupa1 = _db.Grupe.Where(x => x.Naziv == "G1").FirstOrDefault().GrupaId;
            int idGrupa2 = _db.Grupe.Where(x => x.Naziv == "G2").FirstOrDefault().GrupaId;
            int idGrupa3 = _db.Grupe.Where(x => x.Naziv == "G3").FirstOrDefault().GrupaId;

            if (brojac > 30)
            {
                brojac -= 1;

                while (brojac >= 0)
                {
                    PutovanjaGrupe pg1 = new PutovanjaGrupe { DatumPutovanja = rezervacije[brojac].Putovanje.DatumPolaska, GrupaId = idGrupa1, RezervacijaId = rezervacije[brojac].RezervacijaId };
                    _db.PutovanjaGrupe.Add(pg1);
                    brojac--;
                    if (brojac < 0)
                        break;
                    PutovanjaGrupe pg2 = new PutovanjaGrupe { DatumPutovanja = rezervacije[brojac].Putovanje.DatumPolaska, GrupaId = idGrupa2, RezervacijaId = rezervacije[brojac].RezervacijaId };
                    _db.PutovanjaGrupe.Add(pg2);
                    brojac--;
                }
            }
            else if (brojac > 60)
            {
                brojac -= 1;

                while (brojac >= 0)
                {
                    PutovanjaGrupe pg1 = new PutovanjaGrupe { DatumPutovanja = rezervacije[brojac].Putovanje.DatumPolaska, GrupaId = idGrupa1, RezervacijaId = rezervacije[brojac].RezervacijaId };
                    _db.PutovanjaGrupe.Add(pg1);
                    brojac--;
                    if (brojac < 0)
                        break;
                    PutovanjaGrupe pg2 = new PutovanjaGrupe { DatumPutovanja = rezervacije[brojac].Putovanje.DatumPolaska, GrupaId = idGrupa2, RezervacijaId = rezervacije[brojac].RezervacijaId };
                    _db.PutovanjaGrupe.Add(pg2);
                    brojac--;
                    if (brojac < 0)
                        break;
                    PutovanjaGrupe pg3 = new PutovanjaGrupe { DatumPutovanja = rezervacije[brojac].Putovanje.DatumPolaska, GrupaId = idGrupa3, RezervacijaId = rezervacije[brojac].RezervacijaId };
                    _db.PutovanjaGrupe.Add(pg3);
                    brojac--;
                }
            }
            else {

                foreach (var x in rezervacije)
                {
                    PutovanjaGrupe pg1 = new PutovanjaGrupe { DatumPutovanja = x.Putovanje.DatumPolaska, GrupaId = idGrupa1, RezervacijaId = x.RezervacijaId };
                    _db.PutovanjaGrupe.Add(pg1);
                }
            }

            _db.SaveChanges();
            return RedirectToAction("PregledGrupa", "Putovanje", new { putovanjeId = putovanjeId });
        }


        public IActionResult Pregled(int putovanjeId)
        {
            Putovanje p = _db.Putovanja.Include(x => x.Ponuda)
                                       .Include(x => x.PrevnoznoSredstvo)
                                       .Where(x => x.PutovanjeId == putovanjeId).FirstOrDefault();
            PutovanjePregledVM model = new PutovanjePregledVM
            {
                putovanjeId=p.PutovanjeId,
                trajanje=(p.DatumPovratka.Subtract(p.DatumPolaska).Days).ToString(),
                cijenaSPopustom=(p.Cijena-(p.Cijena*(p.Popust??0)/100)).ToString()+" KM",
                datumIzmjene=p.DatumIzmjene.ToString(),
                redovnaCijena=p.Cijena.ToString()+" KM",
                popust=(p.Popust??0).ToString()+" %",
                datumPolaska=p.DatumPolaska.ToString("dd.MM.yyyy"),
                datumPovratka=p.DatumPovratka.ToString("dd.MM.yyyy"),
                opis=p.Opis,
                ponuda=p.PonudaId==null?"N/A":p.Ponuda.Naziv,
                prevoz=p.PrevnoznoSredstvo.Naziv,
                isAktivno=p.isAktivno,
                isUProcesuRezervacije=p.DatumPolaska>DateTime.Now,
            };
            model.lokacija = getLokacija(p.PutovanjeId);
            model.cover = getCoverSlikaSrc(p.PutovanjeId);
            model.isKreiraneGrupe = _db.PutovanjaGrupe.Include(x => x.Rezervacija).Where(x => x.Rezervacija.PutovanjeId == p.PutovanjeId && model.isUProcesuRezervacije == true && x.DatumPutovanja == p.DatumPolaska).Count() > 0;
            
            return View(model);
        }

        public IActionResult PromjeniStatus(int putovanjeId)
        {
            Putovanje p = _db.Putovanja.Find(putovanjeId);
            if (p != null)
            {
                p.isAktivno = !p.isAktivno;
                _db.Putovanja.Update(p);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Pregled), new { putovanjeId = p.PutovanjeId });
        }


      


        public string getLokacija(int putovanjeId)
        {
            var p = _db.Putovanja.Include(x => x.Grad)
                                           .Include(x => x.Grad.Regija)
                                           .Include(x => x.Grad.Regija.Drzava)
                                           .Include(x => x.Grad.Regija.Drzava.Kontinent)
                                           .Where(x => x.PutovanjeId == putovanjeId).FirstOrDefault();
            string lokacija = p.Grad.Naziv + ", " + p.Grad.Regija.Drzava.Naziv + ", " + p.Grad.Regija.Drzava.Kontinent.Naziv;

            return lokacija;
        }
        public string getCoverSlikaSrc(int putovanjeId)
        {
            var imagesrc="";

            var s = _db.Slike.Where(x => x.PutovanjeId == putovanjeId).FirstOrDefault();
            if (s != null)
            {
                byte[] slikaByte = s.Image;
                var type = s.imgType;
                var base64 = Convert.ToBase64String(slikaByte);
                imagesrc = string.Format("data:{0};base64,{1}",type, base64);
            }
            else
                imagesrc = "#";
            return imagesrc;
        }
    }
}