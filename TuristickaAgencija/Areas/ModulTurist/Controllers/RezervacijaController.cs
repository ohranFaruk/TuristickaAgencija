using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Areas.ModulTurist.Models;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;
using TuristickaAgencija.Helpers;

namespace TuristickaAgencija.Areas.ModulTurist.Controllers
{
    [Autorizacija(admin: false, zaposlenik: false, turist: true)]
    public class RezervacijaController : Controller
    {


        private TuristickaAgencijaDB _db;
        private readonly IEmailSender _emailSender;


        public RezervacijaController(TuristickaAgencijaDB turistickaAgencija,IEmailSender emailSender)
        {
            _db = turistickaAgencija;
            _emailSender = emailSender;

        }



        public IActionResult Index(int putovanjeId)
        {

            Putovanje putovanje = _db.Putovanja.Where(x => x.PutovanjeId == putovanjeId).SingleOrDefault();

            RezervacijaDodaj rezervacijaDodaj = new RezervacijaDodaj
            {
                putovanjeId = putovanjeId,
                ukupnaCijena = izracunajCijenu(putovanjeId),
               cijenaBezSmjestaja=putovanje.Cijena,
               trajanjePutovanja=trajanjeDana(putovanje.DatumPolaska,putovanje.DatumPovratka)
              
            };


            Smjestaj najjeftiniji = _db.Smjestaji.Where(x => x.SmjestajId == najjeftinijiHotel(putovanjeId)).SingleOrDefault();
            PutovanjeSmjestaj najjeftinijePutovanjeSmjestaj = _db.PutvanjaSmjestaji.Include(x=>x.Smjestaj).Where(x => x.PutovanjeId == putovanjeId && x.SmjestajId == najjeftiniji.SmjestajId).SingleOrDefault();

            rezervacijaDodaj.putovanjeSmjestaji = new List<SelectListItem>();
            rezervacijaDodaj.putovanjeSmjestaji.Add(new SelectListItem { Value = najjeftinijePutovanjeSmjestaj.Smjestaj.StandardnaCijena.ToString(), Text = najjeftiniji.Naziv });
            rezervacijaDodaj.putovanjeSmjestaji.AddRange(_db.PutvanjaSmjestaji.Where(x => x.PutovanjeId == putovanjeId && x.PutovanjeSmjestajId != najjeftinijePutovanjeSmjestaj.PutovanjeSmjestajId).Select(x => new SelectListItem
            {
                Value = x.Smjestaj.StandardnaCijena.ToString(),
                Text = x.Smjestaj.Naziv

            }).ToList());
         
         

            return View(rezervacijaDodaj);
        }

        public async Task<IActionResult> Snimi(RezervacijaDodaj rezervacijaDodaj,double cijenaSve)
        {
            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();
            Turist turist = _db.Turisti.Where(x => x.Korisnik.KorisnikId == korisnik.KorisnikId).SingleOrDefault();

            KontakPodaci kontakPodaci = _db.KontaktPodaci.Where(x => x.KorisnikId == korisnik.KorisnikId).SingleOrDefault();
            Putovanje putovanje = _db.Putovanja.Include(x=>x.Grad).Where(x => x.PutovanjeId == rezervacijaDodaj.putovanjeId).SingleOrDefault();

            Rezervacija rezervacija = new Rezervacija
            {
                TuristId = turist.TuristId,
                PutovanjeId = rezervacijaDodaj.putovanjeId,
                PutovanjeSmjestajId = vracaId(rezervacijaDodaj.cijena,rezervacijaDodaj.putovanjeId),
                imePutnika = rezervacijaDodaj.imePutnika,
                kontaktTelefon = rezervacijaDodaj.kontaktTelefon,
                prezimePutnika = rezervacijaDodaj.prezimePutnika,
                email = rezervacijaDodaj.email,
                UkupanIznos = cijenaSve,
                DatumRezervacije = DateTime.Now,
                datumRodjenjaPutnika = rezervacijaDodaj.datumRodjenjaPutnika,
                zeljeIprimjedbe = rezervacijaDodaj.zeljeIprimjedbe,
                StanjeId=1




            };




            _db.Rezervacije.Add(rezervacija);
            _db.SaveChanges();



            //slanje mejla putniku
            await _emailSender.SendEmailAsync(rezervacijaDodaj.email, "Konfirmacija rezervisanja putovanja",
                 $"Poštovani " + rezervacija.imePutnika + " " + rezervacija.prezimePutnika + ",\n\nUspješno je rezervisano putovanje "+putovanje.Grad.Naziv+
                 " u iznosu od "+rezervacija.UkupanIznos+" KM "+" na vaše ime \n\n Vaš WTTA tim !");
            


            // slanje mejla turistu za potvrdu rezervacije

            await _emailSender.SendEmailAsync(kontakPodaci.Email, "Konfirmacija o uspjesnosti rezervacije",
                $"Poštovani " + korisnik.Ime + " " + korisnik.Prezime + ",\n\nUspješno ste platili rezervaciju za putovanje "+putovanje.Grad.Naziv+
                " u iznosu od "+rezervacija.UkupanIznos+" na ime "+rezervacijaDodaj.imePutnika+" "+rezervacijaDodaj.prezimePutnika+ "\n\n Vaš WTTA tim !");



            return Redirect("/Modulturist/putovanjetur/detalji?putovanjeId="+rezervacijaDodaj.putovanjeId);
        }


        public  IActionResult PrikazRezervacija(int korisnikId)
        {
         


            Korisnik korisnik = _db.Korisnici.Where(x => x.KorisnikId == korisnikId).SingleOrDefault();


            RezervacijaPrikaz rezervacijaPrikaz = new RezervacijaPrikaz
            {
                korisnikId = korisnikId,
                redovi = _db.Rezervacije.Where(x => x.TuristId == korisnikId).Select(x => new RezervacijaPrikaz.row
                {
                    rezervacijaId=x.RezervacijaId,
                    datumRezervacije=x.DatumRezervacije.ToString("dd.MM.yyyy"),
                    destinacija=x.Putovanje.Grad.Naziv,
                    stanje=x.Stanje.StanjeRezervacije,
                    UkupanIznos=x.UkupanIznos,
                    turist=x.imePutnika,
                    putovanjeId=x.PutovanjeId,
                    


                }).ToList()


            };

            foreach (var x in rezervacijaPrikaz.redovi)
            {

                x.slika = getCoverSlikaSrc(x.putovanjeId);
            }


            return View(rezervacijaPrikaz);

        }

       public IActionResult Obrisi(int rezervacijaId)
        {
            Rezervacija rezervacija = _db.Rezervacije.Include(x=>x.Turist.Korisnik).Where(x => x.RezervacijaId == rezervacijaId).SingleOrDefault();
            Korisnik korisnik = _db.Korisnici.Where(x => x.KorisnikId == rezervacija.Turist.Korisnik.KorisnikId).SingleOrDefault();

            _db.Rezervacije.Remove(rezervacija);
            _db.SaveChanges();

            return Redirect("/modulturist/rezervacija/prikazrezervacija?korisnikId="+korisnik.KorisnikId);

        }




        //funckije
        public int trajanjeDana(DateTime polazak,DateTime povratak)
            {

            TimeSpan timeSpan = povratak - polazak;
            int dana = timeSpan.Days;

            return dana;
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
            return cijenaHotelaNoc * trajanje + cijenaPutovanja;
        }

        public int najjeftinijiHotel(int putovanjeId)
        {

            Putovanje putovanje = _db.Putovanja.Where(x => x.PutovanjeId == putovanjeId).SingleOrDefault();
            List<PutovanjeSmjestaj> putovanjeSmjestaj = _db.PutvanjaSmjestaji.Include(x => x.Smjestaj).Where(x => x.PutovanjeId == putovanjeId).ToList();

            double cijenaHotelaNoc = 8543585;

            int najjeftinijiId = 0;

            foreach (var x in putovanjeSmjestaj)
            {
                if (cijenaHotelaNoc > x.Smjestaj.StandardnaCijena)
                {
                    cijenaHotelaNoc = x.Smjestaj.StandardnaCijena;
                    najjeftinijiId = x.SmjestajId;
                }


            }


            return najjeftinijiId;
        }

        public int vracaId(double cijena,int putovanjeId)
        {

            PutovanjeSmjestaj putovanje = _db.PutvanjaSmjestaji.Where(x => x.Smjestaj.StandardnaCijena == cijena&&x.PutovanjeId==putovanjeId).SingleOrDefault();

            return putovanje.PutovanjeSmjestajId;
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