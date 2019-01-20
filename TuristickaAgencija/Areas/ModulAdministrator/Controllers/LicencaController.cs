using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Areas.ModulAdministrator.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulAdministrator.Controllers
{
    // [Autorizacija(admin: true, zaposlenik: false,turist:false)]
    public class LicencaController : Controller
    {
        private TuristickaAgencijaDB _db;
        public LicencaController(TuristickaAgencijaDB db)
        {
            _db = db;
        }
        public IActionResult Index(int zaposlenikId)
        {
            LicencaIndexVM model = new LicencaIndexVM
            {
                rows = _db.Licence.Where(x => x.VodicId == zaposlenikId).Select(x => new LicencaIndexVM.Row {
                    licencaId = x.LicencaId,
                    naziv = x.Naziv,
                    serijskiBroj = x.SerijskiBrojLicence,
                    datumStjecanja = x.DatumStjecanja.ToString("dd.MM.yyyy")
                }).ToList(),
                zaposlenikId=zaposlenikId

            };


            return PartialView(model);
        }
        [HttpGet]
        public IActionResult Dodaj(int zaposlenikId)
        {
            LicencaDodajVM model = new LicencaDodajVM
            {
                zaposlenikId=zaposlenikId,
                datumStjecanja=DateTime.Now.Date
            };

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Dodaj(LicencaDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            _db.Licence.Add(new Licenca
            {
                VodicId=vm.zaposlenikId,
                Naziv=vm.Naziv,
                DatumStjecanja=vm.datumStjecanja,
                SerijskiBrojLicence=vm.serijskiBroj
            });
            _db.SaveChanges();
            return RedirectToAction("Index",new {zaposlenikId=vm.zaposlenikId });
        }

        [HttpGet]
        public IActionResult Uredi(int licencaId) {

            Licenca l = _db.Licence.Include(x => x.Vodic).Include(x => x.Vodic.Korisnik).Where(x => x.LicencaId == licencaId).FirstOrDefault();
            LicencaUrediVM model = new LicencaUrediVM
            {
                licencaId = l.LicencaId,
                datumStjecanja = l.DatumStjecanja,
                naziv = l.Naziv,
                serijskiBroj = l.SerijskiBrojLicence,
                zaposlenikId=l.VodicId,
            };

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Uredi(LicencaUrediVM vm)
        {
            Licenca l = _db.Licence.Find(vm.licencaId);

            if (!ModelState.IsValid)
            {
                vm.zaposlenikId = l.VodicId;
                return PartialView(vm);
            }
            l.DatumStjecanja = vm.datumStjecanja;
            l.Naziv = vm.naziv;
            l.SerijskiBrojLicence = vm.serijskiBroj;
            _db.Licence.Update(l);
            _db.SaveChanges();

            return RedirectToAction("Index",new {zaposlenikId=l.VodicId });
        }

        public IActionResult Obrisi(int licencaId)
        {
            Licenca l = _db.Licence.Find(licencaId);
            int zaposlenik = l.VodicId;
            _db.Licence.Remove(l);
            _db.SaveChanges();

            return RedirectToAction("Index", new { zaposlenikId = zaposlenik });
        }
    }
}