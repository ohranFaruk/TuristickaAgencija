using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Data.DAL;
using TuristickaAgencija.Data.Models;

namespace TuristickaAgencija.Helpers
{
    public class Autorizacija : TypeFilterAttribute
    {
        public Autorizacija(bool admin, bool zaposlenik,bool turist) : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { admin, zaposlenik,turist };
        }

        public class MyAuthorizeImpl : IAsyncActionFilter
        {

            public MyAuthorizeImpl(bool admin, bool zaposlenik,bool turist)
            {
                _admin = admin;
                _zaposlenik = zaposlenik;
                _turist = turist;
            }

            private readonly bool _admin;
            private readonly bool _zaposlenik;
            private readonly bool _turist;



            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                Korisnik k = context.HttpContext.GetLogiraniKorisnik();

                if (k == null)
                {
                    if (context.Controller is Controller controller)
                    {
                        controller.ViewData["error_poruka"] = "Niste logirani";
                    }
                    context.Result = new RedirectToActionResult("LoginPage", "Login", new { area = "" });
                    return;
                }

                TuristickaAgencijaDB db = context.HttpContext.RequestServices.GetService<TuristickaAgencijaDB>();

                if (_admin && k.isAdmin)
                {
                    await next();
                    return;
                }

                if (_zaposlenik && db.Zaposlenici.Any(x => x.ZaposlenikId == k.KorisnikId && x.IsVodic == false))
                {
                    await next();
                    return;
                }

                if (_zaposlenik && db.Zaposlenici.Any(x => x.ZaposlenikId == k.KorisnikId && x.IsVodic == true))
                {
                    await next();
                    return;
                }

                if (_turist && db.Turisti.Any(x => x.TuristId == k.KorisnikId))
                {
                    await next();
                    return;
                }

                if (context.Controller is Controller c1)
                {
                    c1.TempData["error_poruka"] = "Nemate pravo pristupa";
                    context.Result = new RedirectToActionResult("LoginPage", "Login", new { area = "" });

                }


            }
        }
    }
}
