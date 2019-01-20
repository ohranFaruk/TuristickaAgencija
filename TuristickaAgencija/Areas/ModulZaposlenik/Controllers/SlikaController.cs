using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Areas.ModulZaposlenik.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulZaposlenik.Controllers
{
    //[Autorizacija(admin:true,zaposlenik:true,turist:false)]
    public class SlikaController : Controller
    {
        private TuristickaAgencijaDB _db;

        public SlikaController(TuristickaAgencijaDB db)
        {
            _db = db;
        }

        public IActionResult Index(int? putovanjeId = null, int? smjestajId = null)
        {
            SlikaIndexVM model = new SlikaIndexVM {
                slikaPutovanjeId=putovanjeId,
                slikaSmjestajId=smjestajId,
                rows = new List<SlikaIndexVM.Row>(),
            };

            var slike = _db.Slike.Where(x => x.PutovanjeId == putovanjeId && x.SmjestajId == smjestajId).ToList();

            foreach (var x in slike)
            {
                var base64 = Convert.ToBase64String(x.Image);
                var imgtype = x.imgType;
                string src = string.Format("data:{0};base64,{1}", imgtype, base64);
                model.rows.Add(new SlikaIndexVM.Row {slikaId=x.SlikaId,imgsrc=src });
            }

            return PartialView(model);
        }

        public IActionResult Pregled(int slikaId)
        {
            Slika s = _db.Slike.Find(slikaId);

            TempData["src"] = string.Format("data:{0};base64,{1}", s.imgType, Convert.ToBase64String(s.Image));
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> DodajAsync(int? putovanjeId, int? smjestajId, List<IFormFile> slike)
        {
            foreach (var x in slike)
            {
                
                using (var ms = new MemoryStream())
                {
                    if (x.Length > 0)
                    {
                        await x.CopyToAsync(ms);
                        byte[] slika = ms.ToArray();
                        if (putovanjeId != null)
                            _db.Slike.Add(new Slika { Image = slika, imgType = x.ContentType, PutovanjeId = putovanjeId });
                        else
                            _db.Slike.Add(new Slika { Image = slika, imgType = x.ContentType, SmjestajId = smjestajId });
                    }
                }
            }
            _db.SaveChanges();
            if(putovanjeId!=null)
                return RedirectToAction("Pregled", "Putovanje", new { putovanjeId = putovanjeId });
            else
                return RedirectToAction("Pregled", "Smjestaj", new { smjestajId = smjestajId });

        }


        public IActionResult Obrisi(int slikaId)
        {
            Slika s = _db.Slike.Find(slikaId);



            int? putovanjeId = s.PutovanjeId;
            if (putovanjeId != null)
            {
                var putovanjaSLike = _db.Slike.Where(x => x.PutovanjeId == putovanjeId).Count();
                if(putovanjaSLike<2)
                    return RedirectToAction("Index", new { putovanjeId = putovanjeId });

            }
            int? smjestajId = s.SmjestajId;
            if (putovanjeId != null)
            {
                var smjestajSlike = _db.Slike.Where(x => x.SmjestajId == smjestajId).Count();
                if (smjestajId < 2)
                    return RedirectToAction("Index", new { smjestajId = smjestajId });
            }
            _db.Slike.Remove(s);
            _db.SaveChanges();
            if (putovanjeId != null)
                return RedirectToAction("Index", new {putovanjeId = putovanjeId });

            return RedirectToAction("Index", new { smjestajId = smjestajId });
        }

    }
}