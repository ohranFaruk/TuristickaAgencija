using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Areas.ModulAdministrator.Helper;
using TuristickaAgencija.Areas.ModulAdministrator.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulAdministrator.Controllers
{
    //  [Autorizacija(admin:true,zaposlenik:false,turist:false)]
    public class JezikController : Controller
    {
        private TuristickaAgencijaDB _db;
        private DropDown _dropdown;
        public JezikController(TuristickaAgencijaDB db)
        {
            _db = db;
            _dropdown = new DropDown(_db);
        }

        public IActionResult Index(int zaposlenikId)
        {
            JezikIndexVM model = new JezikIndexVM
            {
                rows = _db.VodiciJezici.Include(x => x.Jezik).Where(x => x.ZaposlenikId == zaposlenikId).Select(x => new JezikIndexVM.Row {
                    vodicJezikId = x.VodicJezikId,
                    nazivJezika = x.Jezik.NazivJezika,
                    stepen = x.Stepen
                }).ToList(),
                zaposlenikId = zaposlenikId
            };

            return PartialView(model);
        }
        [HttpGet]
        public IActionResult Dodaj(int zaposlenikId)
        {

        JezikDodajVM model = new JezikDodajVM
            {
                zaposlenikId=zaposlenikId,
                jezici=_dropdown.Jezici(true,0,zaposlenikId),
                stepeniJezika=_dropdown.StepeniJezika(),
            };


            return PartialView(model);
        }
        [HttpPost]
        public IActionResult Dodaj(JezikDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.jezici = _dropdown.Jezici(true, vm.jezikId,vm.zaposlenikId);
                vm.stepeniJezika = _dropdown.StepeniJezika(true, vm.stepenJezika);
                return PartialView(vm);
            }

            //uvijek vrati False iako su sva potrebna polja popunjena ://

            _db.VodiciJezici.Add(new VodicJezik {JezikId=vm.jezikId,Stepen=vm.stepenJezika,ZaposlenikId=vm.zaposlenikId });
            _db.SaveChanges();

            return RedirectToAction(nameof(Index),new {zaposlenikId=vm.zaposlenikId });
        }

        public IActionResult Obrisi(int vodiciJezikId)
        {
            VodicJezik v = _db.VodiciJezici.Find(vodiciJezikId);
            int zaposlenik = v.ZaposlenikId;

            _db.VodiciJezici.Remove(v);
            _db.SaveChanges();
            return RedirectToAction("Index",new {zaposlenikId=zaposlenik });
        }
        [HttpGet]
        public IActionResult Uredi(int vodiciJezikId)
        {
            VodicJezik v = _db.VodiciJezici.Find(vodiciJezikId);

            JezikUrediVM model = new JezikUrediVM
            {
                vodicJezikId = v.VodicJezikId,
                jezici = _dropdown.Jezici(true, v.JezikId,v.ZaposlenikId),
                stepeni = _dropdown.StepeniJezika(true, v.Stepen),
                zaposlenikId=v.ZaposlenikId
            };
            
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Uredi(JezikUrediVM vm)
        {
            VodicJezik v = _db.VodiciJezici.Find(vm.vodicJezikId);

            if (!ModelState.IsValid)
            {
                vm.jezici = _dropdown.Jezici(true,vm.jezikId,v.ZaposlenikId);
                vm.stepeni = _dropdown.StepeniJezika();
                return PartialView(vm);
            }


            v.JezikId = vm.jezikId;
            v.Stepen = vm.stepen;
            _db.VodiciJezici.Update(v);
            _db.SaveChanges();

            return RedirectToAction("Index", new { zaposlenikId = v.ZaposlenikId });
        }


    }
}