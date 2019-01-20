using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Areas.ModulTurist.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulTurist.Controllers
{
   

    public class PonudaTurController : Controller
    {
        private TuristickaAgencijaDB  _db;

        public PonudaTurController(TuristickaAgencijaDB turistickaAgencijaDB)
        {
            _db = turistickaAgencijaDB;
        }


        public IActionResult Prikaz()
        {

            PonudaTurPrikaziVM model = new PonudaTurPrikaziVM
            {
                redovi = _db.Ponude.Select(x => new PonudaTurPrikaziVM.row
                {
                    ponudaId = x.PonudaId,
                    naziv = x.Naziv,
                    datumDo=x.DatumZavrsetka.ToString("dd.MM.yyyy"),
                    datumOd=x.DatumPocetka.ToString("dd.MM.yyyy")


                }).ToList()
            };



            foreach (var x in model.redovi)
            {

                x.slika = getCoverSlikaSrc(x.ponudaId);
            }


            return View(model);
        }






        //funkcija za slike
        public string getCoverSlikaSrc(int ponudaId)
        {
            var imagesrc = "";

            var s = _db.Slike.Where(x => x.PonudaId == ponudaId).FirstOrDefault();
            if (s != null)
            {
                byte[] slikaByte = s.Image;
                var type = s.imgType;
                var base64 = Convert.ToBase64String(slikaByte);
                imagesrc = string.Format("data:{0};base64,{1}", type, base64);
            }
            else
                imagesrc = "#";
            return imagesrc;
        }




    }
}