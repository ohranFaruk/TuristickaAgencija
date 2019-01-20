using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;

namespace TuristickaAgencija.Helpers
{
    public static class Autentifikacija
    {
        private const string logiraniKorisnik = "logiraniKorisnik";
        public const string adminUsername = "admin";
        public const string adminPassword = "admin";



        public static void SetLogiraniKorisnik(this HttpContext context, Korisnik korisnik, bool zapamti = true)
        {
            context.Session.Set(logiraniKorisnik, korisnik);

            if (zapamti)
            {
                TuristickaAgencijaDB db = context.RequestServices.GetService<TuristickaAgencijaDB>();

                string token = Guid.NewGuid().ToString();
                db.AutoriacijskiTokeni.Add(new AutorizacijskiToken
                {
                    Vrijednost = token,
                    KorisnikId = korisnik.KorisnikId,
                    VrijemeEvidentiranja = DateTime.Now
                });
                db.SaveChanges();

                context.Response.SetCookieJson(logiraniKorisnik, token);
            }
            else
            {
                context.Response.SetCookieJson(logiraniKorisnik, null);
            }


        }

        public static Korisnik GetLogiraniKorisnik(this HttpContext context)
        {
            Korisnik k= context.Session.Get<Korisnik>(logiraniKorisnik);
            if (k == null)
            {

                TuristickaAgencijaDB db = context.RequestServices.GetService<TuristickaAgencijaDB>();

                string token = context.Request.GetCookieJson<string>(logiraniKorisnik);
                if (token == null)
                    return null;

                AutorizacijskiToken autorizacijskiToken = db.AutoriacijskiTokeni.Include(x => x.Korisnik)
                                                                                .SingleOrDefault(x => x.Vrijednost == token);

                if (autorizacijskiToken != null)
                {

                    context.Session.Set(logiraniKorisnik, autorizacijskiToken.Korisnik);
                    k = autorizacijskiToken.Korisnik;
                }
            }
            return k;
        }

        public static string getHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
