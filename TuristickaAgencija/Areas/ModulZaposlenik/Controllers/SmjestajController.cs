using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Areas.ModulAdministrator.Helper;
using TuristickaAgencija.Areas.ModulZaposlenik.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Controllers
{
   // [Autorizacija(admin:true,zaposlenik:true,turist:false)]
    public class SmjestajController : Controller
    {
        private TuristickaAgencijaDB _db;
        private DropDown _dropDown;
        public SmjestajController(TuristickaAgencijaDB db)
        {
            _db = db;
            _dropDown = new DropDown(db);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PrikaziSmjestaj(string aktivni = "da")
        {
            SmjestajIndexVM model = new SmjestajIndexVM
            {
                divs = _db.Smjestaji.Where(x=>x.isAktivan == (aktivni=="da"))
                                    .Select(x => new SmjestajIndexVM.Div {
                                        smjestajId=x.SmjestajId,
                                        nazivHotela=x.Naziv
                                    }).ToList(),

            };

            foreach (var x in model.divs)
            {
                x.brojZvjezdica = getBrojZvjezdica(x.smjestajId);
                x.lokacija = getSmjestajLokacija(x.smjestajId);
            }

            return PartialView(model);
        }
        public IActionResult PrikaziSmjestajPutovanje(int? putovanjeId = null, string aktivni = "da")
        {
            SmjestajIndexVM model = new SmjestajIndexVM
            {
                divs = _db.PutvanjaSmjestaji.Include(x => x.Smjestaj)
                                          .Where(x => x.Smjestaj.isAktivan == (aktivni == "da") && (putovanjeId == null || x.PutovanjeId == putovanjeId))
                                          .Select(x => new SmjestajIndexVM.Div
                                          {
                                              smjestajId = x.SmjestajId,
                                              nazivHotela = x.Smjestaj.Naziv
                                          }).ToList(),

            };

            foreach (var x in model.divs)
            {
                x.brojZvjezdica = getBrojZvjezdica(x.smjestajId);
                x.lokacija = getSmjestajLokacija(x.smjestajId);
            }

            return PartialView("PrikaziSmjestaj",model);
        }

        public IActionResult PromjeniStatus(int smjestajId)
        {
            Smjestaj s = _db.Smjestaji.Find(smjestajId);
            if(s!=null)
            { 
                s.isAktivan = !s.isAktivan;
                _db.Smjestaji.Update(s);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Pregled",new {smjestajId=s.SmjestajId });

        }
        [HttpGet]
        public IActionResult Dodaj()
        {
            SmjestajDodajVM model = new SmjestajDodajVM
            {
                zvjezdice=_dropDown.HotelZvjezdice(),
                gradovi=_dropDown.Gradovi()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DodajAsync(SmjestajDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.zvjezdice = _dropDown.HotelZvjezdice(vm.brZvjezdica);
                vm.gradovi = _dropDown.Gradovi(true, vm.gradId);
                return View(vm);
            }
            Smjestaj s = new Smjestaj
            {
                WebStranica = vm.webStranica,
                isAktivan = true,
                BrojZvjezdica = vm.brZvjezdica,
                GradId=vm.gradId,
                Naziv=vm.NazivHotela,
                Opis=vm.opis,
                StandardnaCijena=vm.stdCijena,
            };
            _db.Smjestaji.Add(s);

            var putovanja = _db.Putovanja.Where(x => x.GradId == s.GradId).ToList();
            foreach (var x in putovanja)
            {
                PutovanjeSmjestaj ps = new PutovanjeSmjestaj
                {
                    PutovanjeId=x.PutovanjeId,
                    SmjestajId=s.SmjestajId
                };
                _db.PutvanjaSmjestaji.Add(ps);
            }

            foreach (var x in vm.slike)
            {
                using (var ms = new MemoryStream())
                {
                    if (x.Length > 0)
                    {
                        await x.CopyToAsync(ms);
                        byte[] slika = ms.ToArray();
                        _db.Slike.Add(new Slika { Image = slika, imgType = x.ContentType, SmjestajId = s.SmjestajId });
                    }
                }
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Uredi(int smjestajId)
        {
            Smjestaj s = _db.Smjestaji.Include(x=>x.Grad).Where(x=>x.SmjestajId==smjestajId).FirstOrDefault();

            SmjestajUrediVM model = new SmjestajUrediVM
            {
                NazivHotela = s.Naziv,
                smjestajId = s.SmjestajId,
                stdCijena = s.StandardnaCijena,
                webStranica = s.WebStranica,
                opis = s.Opis,
                grad = s.Grad.Naziv,
                zvjezdice = _dropDown.HotelZvjezdice(s.BrojZvjezdica),
                brZvjezdica=s.BrojZvjezdica
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Uredi(SmjestajUrediVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.zvjezdice = _dropDown.HotelZvjezdice(vm.brZvjezdica);
                vm.grad = _db.Smjestaji.Include(x => x.Grad)
                                       .Where(x => x.SmjestajId == vm.smjestajId)
                                       .FirstOrDefault().Grad.Naziv;
                return View(vm);
            }

            Smjestaj s = _db.Smjestaji.Find(vm.smjestajId);
            s.BrojZvjezdica = vm.brZvjezdica;
            s.Naziv = vm.NazivHotela;
            s.Opis = vm.opis;
            s.StandardnaCijena = vm.stdCijena;
            s.WebStranica = vm.webStranica;

            _db.Smjestaji.Update(s);
            _db.SaveChanges();


            return RedirectToAction("Pregled", new { smjestajId = s.SmjestajId });
        }
        
        public IActionResult Pregled(int smjestajId)
        {
            Smjestaj s = _db.Smjestaji.Find(smjestajId);
            SmjestajPregledVM model = new SmjestajPregledVM
            {
                cijena=s.StandardnaCijena.ToString(),
                smjestajId=s.SmjestajId,
                isAktivan=s.isAktivan,
                opis=s.Opis,
                nazivHotela=s.Naziv

            };
            model.brZvjezdica = getBrojZvjezdica(s.SmjestajId);
            model.lokacija = getSmjestajLokacija(s.SmjestajId);
            model.cover = getCoverPhoto(s.SmjestajId);

            return View(model);
        }


        public string getSmjestajLokacija(int smjestajId)
        {
            Smjestaj s = _db.Smjestaji.Include(x => x.Grad)
                                      .Include(x => x.Grad.Regija)
                                      .Include(x => x.Grad.Regija.Drzava)
                                      .Include(x => x.Grad.Regija.Drzava.Kontinent).Where(x => x.SmjestajId == smjestajId).FirstOrDefault();
            string lokacija = s.Grad.Naziv + ", " + s.Grad.Regija.Drzava.Naziv + ", " + s.Grad.Regija.Drzava.Kontinent.Naziv;
            return lokacija;
        }
        public string getCoverPhoto(int smjestajId)
        {
            Slika s = _db.Slike.Where(x => x.SmjestajId == smjestajId).FirstOrDefault();
            string src = "";
            if (s != null)
            {
                byte[] slika = s.Image;
                var imgType = s.imgType;
                var base64 = Convert.ToBase64String(slika);
                 src= string.Format("data:{0};base64,{1}",imgType,base64);
            }
            return src;
        }
        public string getBrojZvjezdica(int smjestajId)
        {
            int brZvjezdica = _db.Smjestaji.Find(smjestajId).BrojZvjezdica;
            string zvjezdice = "";

            switch (brZvjezdica)
            {
                case 1: {
                        zvjezdice = "★";
                    }; break;
                case 2:
                    {
                        zvjezdice = "★★";
                    }; break;
                case 3:
                    {
                        zvjezdice = "★★★";
                    }; break;
                case 4:
                    {
                        zvjezdice = "★★★★";
                    }; break;
                case 5:
                    {
                        zvjezdice = "★★★★★";
                    }; break;
                case 6:
                    {
                        zvjezdice = "★★★★★★";
                    }; break;
                case 7:
                    {
                        zvjezdice = "★★★★★★★";
                    }; break;
                default: break;
            }
            return zvjezdice;
        }

    }
}