using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Areas.ModulTurist.Models;
using TuristickaAgencija.Data.DAL;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulTurist.Controllers
{
 
    public class PutovanjeTurController : Controller
    {
      
       

        private TuristickaAgencijaDB _db;

        public  PutovanjeTurController(TuristickaAgencijaDB turistickaAgencija)
        {
            _db = turistickaAgencija;

        }


        public IActionResult Prikaz(int ponudaId)
        {

            PutovanjeTurPrikaziVM putovanjeTurPrikaziVM = new PutovanjeTurPrikaziVM
            {


                ponudaId=ponudaId,
               
                redovi = _db.Putovanja.Where(x => x.PonudaId == ponudaId).Select(x => new PutovanjeTurPrikaziVM.row
                {
                    putovanjeId = x.PutovanjeId,
                    ponudaId = x.PonudaId,
                    gradId = x.GradId,
                    nazivGrada = x.Grad.Naziv,
                    trajanje = trajanjeDana(x.DatumPolaska,x.DatumPovratka),
                    datumPolaska =x.DatumPolaska.ToString("dd.MM.yyyy"),
                    datumPovratka=x.DatumPovratka.ToString("dd.MM.yyyy"),
                    opis=x.Opis,                  
                    prevoznoSredstvo=x.PrevnoznoSredstvo.Naziv,
                    drzava=x.Grad.Regija.Drzava.Naziv,
                    cijenaBezSmjestaja=x.Cijena
                
                    
                  


                }).ToList()
            };


           

            foreach (var x in putovanjeTurPrikaziVM.redovi)
            {
                x.cijena = izracunajCijenu(x.putovanjeId);
            }




            foreach (var x in putovanjeTurPrikaziVM.redovi)
            {

                x.slika = getCoverSlikaSrc(x.putovanjeId);
            }


            return View(putovanjeTurPrikaziVM);
        }



        public IActionResult Detalji(int putovanjeId)
        {
            Putovanje putovanje=_db.Putovanja.Include(x => x.Grad).Where(x => x.PutovanjeId == putovanjeId).SingleOrDefault();



            PutovanjeTurDetalji model = new PutovanjeTurDetalji
            {
                putovanjeId = putovanje.PutovanjeId,
                lokacija=putovanje.Grad.Naziv,
                gradId = putovanje.GradId,
                opis = putovanje.Opis,
                cijena = izracunajCijenu(putovanje.PutovanjeId),
                cijenaBezSmjestaja=putovanje.Cijena,
                datumPolaska=putovanje.DatumPolaska.ToString("dd.MM"),
                datumPovratka=putovanje.DatumPovratka.ToString("dd.MM.yyyy"),
                
                redovi=_db.Slike.Where(x=>x.PutovanjeId==putovanje.PutovanjeId).Select(x=> new PutovanjeTurDetalji.row
                {
                    slikaId=x.SlikaId
                }).ToList(),

                hoteli=_db.PutvanjaSmjestaji.Where(m=>m.PutovanjeId==putovanjeId).Select(m=> new PutovanjeTurDetalji.hotel
                {
                    smjestajId=m.SmjestajId,
                    nazivHotela=m.Smjestaj.Naziv,
                    cijenaNoc=m.Smjestaj.StandardnaCijena
                }).ToList()

                
            };

            foreach (var x in model.redovi)
            {
                x.slika = getCoverSlikaSrc2(x.slikaId);

            }
          
           

           
            return View(model);
        }


      
        public IActionResult Odjava(int putovanjeId)
        {

            

            HttpContext.Session.Clear();

            return Redirect("/ModulTurist/PutovanjeTur/detalji?putovanjeId="+putovanjeId);
        }



        public IActionResult PrikazBezPonuda()
        {


            PutovanjeTurPrikaziVM putovanjeTurPrikaziVM = new PutovanjeTurPrikaziVM
            {


             

                redovi = _db.Putovanja.Where(x=>x.PonudaId==null).Select(x => new PutovanjeTurPrikaziVM.row
                {
                    putovanjeId = x.PutovanjeId,                  
                    gradId = x.GradId,
                    nazivGrada = x.Grad.Naziv,
                    trajanje = trajanjeDana(x.DatumPolaska, x.DatumPovratka),
                    datumPolaska = x.DatumPolaska.ToString("dd.MM.yyyy"),
                    datumPovratka = x.DatumPovratka.ToString("dd.MM.yyyy"),
                    opis = x.Opis,
                    prevoznoSredstvo = x.PrevnoznoSredstvo.Naziv,
                    drzava = x.Grad.Regija.Drzava.Naziv,
                    cijenaBezSmjestaja = x.Cijena





                }).ToList()
            };




            foreach (var x in putovanjeTurPrikaziVM.redovi)
            {
                x.cijena = izracunajCijenu(x.putovanjeId);
            }




            foreach (var x in putovanjeTurPrikaziVM.redovi)
            {

                x.slika = getCoverSlikaSrc(x.putovanjeId);
            }



            return View("Prikaz",putovanjeTurPrikaziVM);
        }





        public IActionResult Pretraga(string ime)
        {
            PutovanjeTurPrikaziVM putovanjeTurPrikaziVM = new PutovanjeTurPrikaziVM
            {




                redovi = _db.Putovanja.Where(x => x.Grad.Naziv.Contains(ime)||ime==null).Select(x => new PutovanjeTurPrikaziVM.row
                {
                    putovanjeId = x.PutovanjeId,
                    gradId = x.GradId,
                    nazivGrada = x.Grad.Naziv,
                    trajanje = trajanjeDana(x.DatumPolaska, x.DatumPovratka),
                    datumPolaska = x.DatumPolaska.ToString("dd.MM.yyyy"),
                    datumPovratka = x.DatumPovratka.ToString("dd.MM.yyyy"),
                    opis = x.Opis,
                    prevoznoSredstvo = x.PrevnoznoSredstvo.Naziv,
                    drzava = x.Grad.Regija.Drzava.Naziv,
                    cijenaBezSmjestaja = x.Cijena





                }).ToList()
            };




            foreach (var x in putovanjeTurPrikaziVM.redovi)
            {
                x.cijena = izracunajCijenu(x.putovanjeId);
            }




            foreach (var x in putovanjeTurPrikaziVM.redovi)
            {

                x.slika = getCoverSlikaSrc(x.putovanjeId);
            }



            return View("Prikaz", putovanjeTurPrikaziVM);
        }


        public IActionResult PrikazZavrsenihPutovanja(string ime, string prezime)
        {


            PrikazZavrsenihVM prikazZavrsenihVM = new PrikazZavrsenihVM
            {
                ime=ime,
                prezime=prezime,
                redovi = _db.Rezervacije.Where(x => x.imePutnika == ime && x.prezimePutnika == prezime).Select(x => new PrikazZavrsenihVM.row
                {
                    lokacija=x.Putovanje.Grad.Naziv,
                    datumPolaska=x.Putovanje.DatumPolaska.ToString("dd.MM.yyyy"),
                    datumPovratka = x.Putovanje.DatumPovratka.ToString("dd.MM.yyyy"),
                    datumRezervisanja=x.DatumRezervacije.ToString("dd.MM.yyyy"),
                    ukupnaCijena=x.UkupanIznos,
                    putovanjeId=x.PutovanjeId,
                    trajanje=trajanjeDana(x.Putovanje.DatumPolaska,x.Putovanje.DatumPovratka),
                    rezervacijaId=x.RezervacijaId
                    


                }).ToList()
            };


            foreach (var x in prikazZavrsenihVM.redovi)
            {

                x.slika = getCoverSlikaSrc(x.putovanjeId);
            }



            return View(prikazZavrsenihVM);
        }


       public IActionResult snimiKomentar(string komentar,int ocjena,int rezervacijaId,string prezime,string ime)
        {

            Recenzija recenzija = new Recenzija
            {
                Ocjena = ocjena,
                Komentar = komentar,
                RezervacijaId = rezervacijaId,
                DatumKomentara = DateTime.Now

            };

            _db.Recenzije.Add(recenzija);
            _db.SaveChanges();


            return Redirect("/modulturist/putovanjeTur/PrikazZavrsenihPutovanja?ime="+ime+"&"+"prezime="+prezime);
        }






        //funkcije

        public int trajanjeDana(DateTime polazak,DateTime povratak)
            {

            TimeSpan timeSpan = povratak - polazak;
            int dana = timeSpan.Days;

            return dana;
            }

        public string getCoverSlikaSrc2(int slikaId)
        {
            var imagesrc = "";

            var s = _db.Slike.Where(x => x.SlikaId == slikaId).FirstOrDefault();
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


        public double izracunajCijenu(int putovanjeId)
        {

           double cijenaHotelaNoc = 8543585;

            Putovanje putovanje = _db.Putovanja.Where(x => x.PutovanjeId == putovanjeId).SingleOrDefault();
            double cijenaPutovanja = putovanje.Cijena;

            TimeSpan trajanjeP = putovanje.DatumPovratka - putovanje.DatumPolaska;

            int trajanje = trajanjeP.Days;

          
            List<PutovanjeSmjestaj> putovanjeSmjestaj = _db.PutvanjaSmjestaji.Include(x => x.Smjestaj).Where(x => x.PutovanjeId == putovanjeId).ToList();


            foreach (var x in putovanjeSmjestaj)
            {
                if (cijenaHotelaNoc > x.Smjestaj.StandardnaCijena)
                {
                    cijenaHotelaNoc = x.Smjestaj.StandardnaCijena;
                }


            }
            return cijenaHotelaNoc*trajanje+cijenaPutovanja;
        }



        public string getCoverSlikaSrc(int putovanjeId)
        {
            var imagesrc = "";

            var s = _db.Slike.Where(x => x.PutovanjeId == putovanjeId).FirstOrDefault();
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