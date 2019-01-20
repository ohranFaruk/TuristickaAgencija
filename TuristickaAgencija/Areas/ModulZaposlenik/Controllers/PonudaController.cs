using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Areas.ModulZaposlenik.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Controllers
{
    //[Autorizacija(admin:true,zaposlenik:true,turist:false)]
    public class PonudaController : Controller
    {
        private TuristickaAgencijaDB _db;

        public PonudaController(TuristickaAgencijaDB db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult PrikaziPonude(string aktivna="da")
        {
            PonudaIndexVM model = new PonudaIndexVM
            {
                rows = _db.Ponude.Where(x=>x.isAktivna==(aktivna=="da")).Select(x => new PonudaIndexVM.Row
                {
                    naziv = x.Naziv,
                    pocetak = x.DatumPocetka.ToString("dd.MM.yyyy"),
                    zavrsetak = x.DatumZavrsetka.ToString("dd.MM.yyyy"),
                    zadnjaIzmjena = x.DatumIzmjene.ToString("dd.MM.yyyy"),
                    ponudaId = x.PonudaId,
                    isAktivna=x.isAktivna,
                    brPutovanja = _db.Putovanja.Where(p => p.PonudaId == x.PonudaId).Count().ToString()
                }).ToList()

            };

            return PartialView(model);
        }
        [HttpGet]
        public IActionResult Dodaj()
        {
            PonudaDodajVM model = new PonudaDodajVM
            {
                pocetak = DateTime.Now.Date,
                kraj=DateTime.Now.Date
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Dodaj(PonudaDodajVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            Ponuda p = new Ponuda
            {
                Naziv=vm.Naziv,
                DatumIzmjene=DateTime.Now,
                DatumPocetka=vm.pocetak,
                DatumZavrsetka=vm.kraj,
                isAktivna=true
            };

            _db.Ponude.Add(p);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult PromjeniStatus(int ponudaId)
        {
            Ponuda p = _db.Ponude.Where(x => x.PonudaId == ponudaId).FirstOrDefault();
            p.isAktivna = !p.isAktivna;
            _db.Ponude.Update(p);

            if (p.isAktivna)
            {
                var putovanja = _db.Putovanja.Where(x => x.PonudaId == ponudaId).ToList();

                foreach (var x in putovanja)
                {
                    x.PonudaId = null;
                    _db.Putovanja.Update(x);
                }
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Uredi(int ponudaId)
        {
            Ponuda p = _db.Ponude.Find(ponudaId);

            PonudaUrediVM model = new PonudaUrediVM
            {
                Naziv=p.Naziv,
                kraj=p.DatumZavrsetka,
                ponudaId=p.PonudaId,
                pocetak=p.DatumPocetka
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Uredi(PonudaUrediVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            Ponuda p = _db.Ponude.Find(vm.ponudaId);

            p.Naziv = vm.Naziv;
            p.DatumPocetka = vm.pocetak;
            p.DatumZavrsetka = vm.kraj;
            p.DatumIzmjene = DateTime.Now;

            _db.Ponude.Update(p);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}