using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TuristickaAgencija.Data.Models;
namespace TuristickaAgencija.Data.DAL
{
    public class DBFirstData
    {
        public static string getHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        public static void Popuni(TuristickaAgencijaDB _db)
        {
           if (_db.Kontinenti.Any())
                return;

            List <Kontinent> kontinenti = new List<Kontinent> {
                new Kontinent{ Naziv="Europa"},
                new Kontinent{ Naziv="Azija"},
                new Kontinent{ Naziv="Afrika"},
                new Kontinent{ Naziv="Australija"},
                new Kontinent{ Naziv="Južna Amerika"},
                new Kontinent{ Naziv="Sjeverna Amerika"},
            };

            foreach (var x in kontinenti)
            {
                _db.Kontinenti.Add(new Kontinent { Naziv = x.Naziv });

            }
            _db.SaveChanges();

            List<Drzava> drzave = new List<Drzava> {

                new Drzava{ KontinentId=1,Naziv="Bosna i Hercegovina"},
                new Drzava{ KontinentId=1,Naziv="Hrvatska"},
                new Drzava{ KontinentId=1,Naziv="Srbija"},
                new Drzava{ KontinentId=1,Naziv="Austrija"},
                new Drzava{ KontinentId=1,Naziv="Crna Gora"},
                new Drzava{ KontinentId=1,Naziv="Belgija"},
                new Drzava{ KontinentId=1,Naziv="Švedska"},
                new Drzava{ KontinentId=1,Naziv="Norveška"},
                new Drzava{ KontinentId=1,Naziv="Poljska"},
                new Drzava{ KontinentId=1,Naziv="Njemačka"},
                new Drzava{ KontinentId=1,Naziv="Francuska"},
                new Drzava{ KontinentId=1,Naziv="Italija"},
                new Drzava{ KontinentId=1,Naziv="Španija"},
                new Drzava{ KontinentId=1,Naziv="Portugal"},
                new Drzava{ KontinentId=1,Naziv="Albanija"},
                new Drzava{ KontinentId=1,Naziv="Rumunija"},
                new Drzava{ KontinentId=1,Naziv="Mađarska"},
                new Drzava{ KontinentId=1,Naziv="Slovenija"},
                new Drzava{ KontinentId=1,Naziv="Holandija"},
                new Drzava{ KontinentId=1,Naziv="Luksemburg"},
                new Drzava{ KontinentId=1,Naziv="Andora"},
                new Drzava{ KontinentId=1,Naziv="Bjelorusija"},
                new Drzava{ KontinentId=1,Naziv="Bugarska"},
                new Drzava{ KontinentId=1,Naziv="Kipar"},
                new Drzava{ KontinentId=1,Naziv="Češka"},
                new Drzava{ KontinentId=1,Naziv="Danska"},
                new Drzava{ KontinentId=1,Naziv="Estonija"},
                new Drzava{ KontinentId=1,Naziv="Finska"},
                new Drzava{ KontinentId=1,Naziv="Island"},
                new Drzava{ KontinentId=1,Naziv="Irska"},
                new Drzava{ KontinentId=1,Naziv="Kosovo"},
                new Drzava{ KontinentId=1,Naziv="Makedonija"},
                new Drzava{ KontinentId=1,Naziv="Litvanija"},
                new Drzava{ KontinentId=1,Naziv="Malta"},
                new Drzava{ KontinentId=1,Naziv="Monaco"},
                new Drzava{ KontinentId=1,Naziv="Moldavija"},
                new Drzava{ KontinentId=1,Naziv="Rumunija"},
                new Drzava{ KontinentId=1,Naziv="San Marino"},
                new Drzava{ KontinentId=1,Naziv="Slovačka"},
                new Drzava{ KontinentId=1,Naziv="Ukrajina"},
                new Drzava{ KontinentId=1,Naziv="Grčka"},
                new Drzava{ KontinentId=1,Naziv="Vatikan"},
                new Drzava{ KontinentId=1,Naziv="Engleska"},
                new Drzava{ KontinentId=1,Naziv="Vels"},
                new Drzava{ KontinentId=1,Naziv="Sjeverna Irska"},
                new Drzava{ KontinentId=1,Naziv="Škotska"},

                new Drzava{ KontinentId=2,Naziv="Šri lanka"},
                new Drzava{ KontinentId=2,Naziv="Indija"},
                new Drzava{ KontinentId=2,Naziv="Kina"},
                new Drzava{ KontinentId=2,Naziv="Indonezija"},
                new Drzava{ KontinentId=2,Naziv="Vijetnam"},
                new Drzava{ KontinentId=2,Naziv="Kambodža"},
                new Drzava{ KontinentId=2,Naziv="Iran"},
                new Drzava{ KontinentId=2,Naziv="Azerbejdžan"},
                new Drzava{ KontinentId=2,Naziv="Dubai"},
                new Drzava{ KontinentId=2,Naziv="Libanon"},
                new Drzava{ KontinentId=2,Naziv="Izrael"},
                new Drzava{ KontinentId=2,Naziv="Džordan"},
                new Drzava{ KontinentId=2,Naziv="Japan"},
                new Drzava{ KontinentId=2,Naziv="Južna Koreja"},
                new Drzava{ KontinentId=2,Naziv="Sjeverna Koreja"},
                new Drzava{ KontinentId=2,Naziv="Mianmar"},
                new Drzava{ KontinentId=2,Naziv="Nepal"},
                new Drzava{ KontinentId=2,Naziv="Uzbekistan"},
                new Drzava{ KontinentId=2,Naziv="Rusija"},
                new Drzava{ KontinentId=2,Naziv="Mongolija"},
                new Drzava{ KontinentId=2,Naziv="Filipini"},
                new Drzava{ KontinentId=2,Naziv="Malezija"},
                new Drzava{ KontinentId=2,Naziv="Maldivi"},
                new Drzava{ KontinentId=2,Naziv="UAE"},
                new Drzava{ KontinentId=2,Naziv="Palestina"},
                new Drzava{ KontinentId=2,Naziv="Turska"},
                new Drzava{ KontinentId=2,Naziv="Saudijska Arabija"},
                new Drzava{ KontinentId=2,Naziv="Singapur"},
                new Drzava{ KontinentId=2,Naziv="Katar"},

                new Drzava{ KontinentId=3,Naziv="Egipat"},
                new Drzava{ KontinentId=3,Naziv="JAR"},
                new Drzava{ KontinentId=3,Naziv="Nambija"},
                new Drzava{ KontinentId=3,Naziv="Zambija"},
                new Drzava{ KontinentId=3,Naziv="Maroko"},
                new Drzava{ KontinentId=3,Naziv="Tunis"},
                new Drzava{ KontinentId=3,Naziv="Zimbabve"},
                new Drzava{ KontinentId=3,Naziv="Kenija"},
                new Drzava{ KontinentId=3,Naziv="Svaziland"},
                new Drzava{ KontinentId=3,Naziv="Mauricijus"},

                new Drzava{ KontinentId=4,Naziv="Australija"},
                new Drzava{ KontinentId=4,Naziv="Novi Zeland"},
                new Drzava{ KontinentId=4,Naziv="Fidži"},
                new Drzava{ KontinentId=4,Naziv="Kiribati"},

                new Drzava{ KontinentId=5,Naziv="Argentina"},
                new Drzava{ KontinentId=5,Naziv="Ekvador"},
                new Drzava{ KontinentId=5,Naziv="Bolivija"},
                new Drzava{ KontinentId=5,Naziv="Brazil"},
                new Drzava{ KontinentId=5,Naziv="Urugvaj"},
                new Drzava{ KontinentId=5,Naziv="Čile"},
                new Drzava{ KontinentId=5,Naziv="Kolumbija"},
                new Drzava{ KontinentId=5,Naziv="Paragvaj"},
                new Drzava{ KontinentId=5,Naziv="Peru"},
                new Drzava{ KontinentId=5,Naziv="Venecuela"},

                new Drzava{ KontinentId=6,Naziv="SAD"},
                new Drzava{ KontinentId=6,Naziv="Kuba"},
                new Drzava{ KontinentId=6,Naziv="Kosta Rika"},
                new Drzava{ KontinentId=6,Naziv="Kanada"},
                new Drzava{ KontinentId=6,Naziv="Dominikanska Republika"},
                new Drzava{ KontinentId=6,Naziv="Meksiko"},
                new Drzava{ KontinentId=6,Naziv="Jamajka"},
                new Drzava{ KontinentId=6,Naziv="Honduras"},
                new Drzava{ KontinentId=6,Naziv="Panama"},
                new Drzava{ KontinentId=6,Naziv="Bahami"},
                new Drzava{ KontinentId=6,Naziv="Belize"},
                new Drzava{ KontinentId=6,Naziv="Trinidad & Tobago"},
            };


            foreach (var x in drzave)
            {
                _db.Drzave.Add(new Drzava { KontinentId=x.KontinentId,Naziv = x.Naziv });
            }
            _db.SaveChanges();

            List<Regija> regije = new List<Regija> {
                 new Regija{ DrzavaId=1, Naziv="Unsko-sanski kanton"},
                 new Regija{ DrzavaId=1, Naziv="Posavksi kanton"},
                 new Regija{ DrzavaId=1, Naziv="Tuzlanski kanton"},
                 new Regija{ DrzavaId=1, Naziv="Zeničko-dobojski kanton"},
                 new Regija{ DrzavaId=1, Naziv="Bosansko-podrinjski kanton"},
                 new Regija{ DrzavaId=1, Naziv="Srednjobosanski kanton"},
                 new Regija{ DrzavaId=1, Naziv="Hercegovačko-neretvanski kanton"},
                 new Regija{ DrzavaId=1, Naziv="Zapadnohercegovački kanton"},
                 new Regija{ DrzavaId=1, Naziv="Kanton Sarajevo"},
                 new Regija{ DrzavaId=1, Naziv="Kanton 10"},
                 new Regija{ DrzavaId=1, Naziv="RS"},


             };

            foreach (var x in regije)
            {
                _db.Regije.Add(new Regija { DrzavaId = x.DrzavaId, Naziv = x.Naziv });
            }
            _db.SaveChanges();

            List<Grad> gradovi = new List<Grad>
            {
                new Grad{ RegijaId=1,Naziv="Bihać"},
                new Grad{ RegijaId=2,Naziv="Orašje"},
                new Grad{ RegijaId=3,Naziv="Tuzla"},
                new Grad{ RegijaId=4,Naziv="Zenica"},
                new Grad{ RegijaId=5,Naziv="Goražde"},
                new Grad{ RegijaId=6,Naziv="Travnik"},
                new Grad{ RegijaId=7,Naziv="Mostar"},
                new Grad{ RegijaId=8,Naziv="Široki Brijeg"},
                new Grad{ RegijaId=9,Naziv="Sarajevo"},
                new Grad{ RegijaId=10,Naziv="Livno"},
                new Grad{ RegijaId=6,Naziv="Jajce"},
                new Grad{ RegijaId=6,Naziv="Bugojno"},
                new Grad{ RegijaId=6,Naziv="Donji Vakuf"},
                new Grad{ RegijaId=6,Naziv="Gornji Vakuf"},
                new Grad{ RegijaId=7,Naziv="Konjic"},
                new Grad{ RegijaId=7,Naziv="Jablanica"},
                new Grad{ RegijaId=11,Naziv="Banja Luka"},
            };

            foreach (var x in gradovi)
            {
                _db.Gradovi.Add(new Grad { RegijaId = x.RegijaId, Naziv = x.Naziv });
            }
            _db.SaveChanges();

            List<StepenVodica> stepeniVodica = new List<StepenVodica>
            {
                new StepenVodica{ Stepen="Pionir"},
                new StepenVodica{ Stepen="Junior"},
                new StepenVodica{ Stepen="Kadet"},
                new StepenVodica{ Stepen="Senior"},
                new StepenVodica{ Stepen="Veteran"},
            };

            foreach (var x in stepeniVodica)
            {
                _db.StepeniVodica.Add(new StepenVodica { Stepen = x.Stepen });
            }
            _db.SaveChanges();

            List<Jezik> jezici = new List<Jezik>
            {
                new Jezik{NazivJezika="Engleski"},
                new Jezik{NazivJezika="Francuski"},
                new Jezik{NazivJezika="Španski"},
                new Jezik{NazivJezika="Portugalski"},
                new Jezik{NazivJezika="Hindski"},
                new Jezik{NazivJezika="Japanski"},
                new Jezik{NazivJezika="Kineski"},
                new Jezik{NazivJezika="Ruski"},
                new Jezik{NazivJezika="Bengalski"},
                new Jezik{NazivJezika="Njemački"},
                new Jezik{NazivJezika="Poljski"},
                new Jezik{NazivJezika="Švedski"},
            };

            foreach (var x in jezici)
            {
                _db.Jezici.Add(new Jezik { NazivJezika = x.NazivJezika });
            }
            _db.SaveChanges();

            List<Status> statusi = new List<Status>
            {
                new Status{ Naziv="Na obradi"},
                new Status{ Naziv="Odobren"},
                new Status{ Naziv="Odbijen"},
            };

            foreach (var x in statusi)
            {
                _db.Statusi.Add(new Status { Naziv = x.Naziv });
            }

            _db.SaveChanges();

            _db.Korisnici.Add(new Korisnik
            {
                Ime = "Admin",
                Prezime = "Admin",
                Spol = "",
                Adresa = "",
                isAktivan = false,
                DatumKreiranja = DateTime.Now,
                DatumRodjenja = DateTime.Now,
                DatumZadnjePrijave = DateTime.Now,
                GradId = 1,
                isPromjenoLozinku = true,
                JMBG = "Admin",
                KorisnickoIme = "admin",
                Lozinka = getHash("admin"),
            });
            _db.SaveChanges();
        }

    }

}
