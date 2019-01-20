using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Data.DAL;

namespace TuristickaAgencija.Areas.ModulAdministrator.Helper
{
    public class DropDown
    {

        private  TuristickaAgencijaDB _db;

        public DropDown(TuristickaAgencijaDB db)
        {
            _db = db;
        }


        public List<SelectListItem> HotelZvjezdice(int? selected = null)
        {
            List<SelectListItem> lista = new List<SelectListItem>{

                new SelectListItem{ Value="0", Text=">>Broj zvjezdica<<"},
                new SelectListItem{ Value="1", Text="★", Selected=(selected==1)},
                new SelectListItem{ Value="2", Text="★★", Selected=(selected==2)},
                new SelectListItem{ Value="3", Text="★★★", Selected=(selected==3)},
                new SelectListItem{ Value="4", Text="★★★★", Selected=(selected==4)},
                new SelectListItem{ Value="5", Text="★★★★★", Selected=(selected==5)},
                new SelectListItem{ Value="6", Text="★★★★★★", Selected=(selected==6)},
                new SelectListItem{ Value="7", Text="★★★★★★★", Selected=(selected==7)}
            };
            return lista;
        }

        public List<SelectListItem> Spolovi(string selected=null)
        {
            List<SelectListItem> lista = new List<SelectListItem>{

                new SelectListItem{ Value=string.Empty, Text=">>Odaberite spol<<"},
                new SelectListItem{ Value="M", Text="M", Selected=selected=="M"},
                new SelectListItem{ Value="Ž", Text="Ž", Selected=selected=="Ž"}

            };
            return lista;
        }

        public  List<SelectListItem> Kontinenti(bool praznaLista = true, int selected=0)
        {
            var kontinenti =  _db.Kontinenti.OrderBy(x=>x.Naziv).ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            if (praznaLista)
                lista.Add(new SelectListItem { Value = "0", Text = ">>Odaberite kontinent<<" });

            lista.AddRange(kontinenti.Select(x => new SelectListItem { Value = x.KontinentId.ToString(), Text = x.Naziv, Selected=x.KontinentId==selected }));

            return lista;
        }

        public List<SelectListItem> Drzave(bool praznaLista = true, int selected = 0)
        {
            var drzave = _db.Drzave.OrderBy(x => x.Naziv).ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            if (praznaLista)
                lista.Add(new SelectListItem { Value = "0", Text = ">>Odaberite drzavu<<" });

            lista.AddRange(drzave.Select(x => new SelectListItem { Value = x.DrzavaId.ToString(), Text = x.Naziv, Selected=x.DrzavaId==selected }));

            return lista;
        }

        public List<SelectListItem> Regije(bool praznaLista = true, int selected = 0)
        {
            var regije = _db.Regije.OrderBy(x => x.Naziv).ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            if (praznaLista)
                lista.Add(new SelectListItem { Value = "0", Text = ">>Odaberite regiju<<" });

            lista.AddRange(regije.Select(x => new SelectListItem { Value = x.RegijaId.ToString(), Text = x.Naziv,Selected=x.RegijaId==selected }));

            return lista;
        }

        public List<SelectListItem> Gradovi(bool praznaLista = true, int selected = 0)
        {
            var gradovi = _db.Gradovi.OrderBy(x => x.Naziv).ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            if (praznaLista)
                lista.Add(new SelectListItem { Value = "0", Text = ">>Odaberite grad<<" });

            lista.AddRange(gradovi.Select(x => new SelectListItem { Value = x.GradId.ToString(), Text = x.Naziv, Selected=x.GradId==selected }));

            return lista;
        }

        public List<SelectListItem> Jezici(bool praznaLista = true, int selected = 0, int zaposlenikId=0)
        {
            var jezici = _db.Jezici.OrderBy(x => x.NazivJezika).ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            if (praznaLista)
                lista.Add(new SelectListItem { Value = "0", Text = ">>Odaberi Jezik<<" });

            lista.AddRange(jezici.Select(x => new SelectListItem { Value = x.JezikId.ToString(), Text = x.NazivJezika,Selected=x.JezikId==selected }));
            if (zaposlenikId > 0)
            {
                var nepotrebni = _db.VodiciJezici.Include(x => x.Jezik).Where(x => x.ZaposlenikId == zaposlenikId).Select(x => new SelectListItem { Value = x.JezikId.ToString(), Text = x.Jezik.NazivJezika, Selected=x.JezikId==selected }).ToList();

                foreach (var x in nepotrebni)
                {
                    if (!x.Selected)
                    {
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (x.Value == lista[i].Value)
                                lista.RemoveAt(i);

                        }
                    }
                }
            }

            return lista;
        }

        public List<SelectListItem> StepeniJezika(bool praznaLista = true, string selected=null)
        {
            List<SelectListItem> lista = new List<SelectListItem> {
                new SelectListItem { Value = string.Empty, Text = ">>Stepen<<" },
                new SelectListItem { Value = "A1", Text = "A1", Selected=selected=="A1" },
                new SelectListItem { Value = "A2", Text = "A2", Selected=selected=="A2" },
                new SelectListItem { Value = "B1", Text = "B1", Selected=selected=="B1" },
                new SelectListItem { Value = "B2", Text = "B2", Selected=selected=="B2" },
                new SelectListItem { Value = "C1", Text = "C1", Selected=selected=="C1" },
                new SelectListItem { Value = "C2", Text = "C2", Selected=selected=="C2" },
            };
            return lista;
        }

        public List<SelectListItem> Ponude(bool praznaLista = true, int? selected = 0)
        {
            var ponude = _db.Ponude.OrderBy(x=>x.Naziv).ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            if (praznaLista)
                lista.Add(new SelectListItem { Value = "0", Text = ">>Odaberite ponudu<<" });

            lista.AddRange(ponude.Select(x => new SelectListItem { Value = x.PonudaId.ToString(), Text = x.Naziv, Selected = x.PonudaId == selected }));

            return lista;
        }

        public List<SelectListItem> PrevoznaSredstva(bool praznaLista = true, int selected = 0)
        {
            var prevozi = _db.PrevoznaSredstva.ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            if (praznaLista)
                lista.Add(new SelectListItem { Value = "0", Text = ">>Odaberite prevoz<<" });

            lista.AddRange(prevozi.Select(x => new SelectListItem { Value = x.PrevoznoSredstvoId.ToString(), Text = x.Naziv, Selected = x.PrevoznoSredstvoId == selected }));

            return lista;
        }

    }
}
