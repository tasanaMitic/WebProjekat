using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class DispecerController : Controller
    {
        // GET: Dispecer
        public ActionResult Index()
        {
            Dispecer dispecer = (Dispecer)Session["korisnik"];
            return View("PrikazDispecera", dispecer);
        }

        public ActionResult Odjava()
        {
            foreach (Dispecer d in Korisnici.ListaDispecera)
            {
                if (d == (Dispecer)Session["korisnik"])
                {
                    d.Prijavljen = false;
                    Session["korisnik"] = null;
                }
            }
            return View("Prijava");
        }

        public ActionResult Izmena()
        {
            Dispecer dispecer = (Dispecer)Session["korisnik"];
            return View("Izmena", dispecer);
        }


        public ActionResult IzmeniPodatke(string ime, string prezime, string pol, string korisnickoIme, string lozinka, string jmbg, string brojTelefona, string email)
        {
            Dispecer dispecer = new Dispecer();
            Pol p = Pol.MUSKI;

            if (pol == "MUSKI")
            {
                p = Pol.MUSKI;
            }
            else if (pol == "ZENSKI")
            {
                p = Pol.ZENSKI;
            }

            foreach (Dispecer d in Korisnici.ListaDispecera)
            {
                if (d.KorisnickoIme == korisnickoIme)
                {
                    d.Ime = ime;
                    d.Prezime = prezime;
                    d.Pol = p;
                    d.Lozinka = lozinka;
                    d.Jmbg = jmbg;
                    d.KontaktTelefon = brojTelefona;
                    d.Email = email;
                    dispecer = d;
                    break;
                }
            }

            Korisnik korisnik = new Korisnik();
            foreach (Korisnik k in Korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme == dispecer.KorisnickoIme)
                {
                    korisnik = dispecer;
                    break;
                }
            }

            return View("PrikazDispecera", dispecer);
        }

        public ActionResult Vozac()
        {
            return View("Vozac");
        }

        public ActionResult VozacNapravi(string ime, string prezime, string pol, string korisnickoIme, string lozinka, string jmbg, string brojTelefona, string email, string ulica, string broj, string mesto, string pozivniBroj, string godiste, string registracija, string taxi, string tipVozila)
        {
            if (ime == "" || prezime == "" || korisnickoIme == "" || lozinka == "" || jmbg == "" || brojTelefona == "" || email == "" || ulica == "" || broj == "" || mesto == "" || pozivniBroj == "" || godiste == "" || registracija == "" || taxi == "")
            {
                return View("VozacPonovo");
            }
            Pol p = Pol.MUSKI;

            if (pol == "MUSKI")
            {
                p = Pol.MUSKI;
            }
            else if (pol == "ZENSKI")
            {
                p = Pol.ZENSKI;
            }

            Vozac vozac = new Vozac(korisnickoIme, lozinka, ime, prezime, p, jmbg, brojTelefona, email, Uloga.VOZAC, false, ulica, broj, mesto, pozivniBroj);

            TipAutomobila tip = TipAutomobila.PUTNICKI;

            if (tipVozila == "KOMBI")
            {
                tip = TipAutomobila.KOMBI;
            }
            else if (tipVozila == "PUTNICKI")
            {
                tip = TipAutomobila.PUTNICKI;
            }
            Automobil automobil = new Automobil(vozac, godiste, registracija, taxi, tip);
            vozac.Automobil = automobil;

            List<string> korisnici = new List<string>();
            foreach (Korisnik k in Korisnici.ListaKorisnika)
            {
                korisnici.Add(k.KorisnickoIme);
            }

            if (korisnici.Contains(vozac.KorisnickoIme))
            {
                foreach (Dispecer d in Korisnici.ListaDispecera)
                {
                    if (d.KorisnickoIme == vozac.KorisnickoIme)
                    {
                        return View("DispecerPostoji");
                    }
                }

                foreach (Musterija m in Korisnici.ListaMusterija)
                {
                    if (m.KorisnickoIme == vozac.KorisnickoIme)
                    {
                        return View("MusterijaPostoji");
                    }
                }
            }
            else
            {
                Korisnici.ListaKorisnika.Add(vozac);
            }

            foreach (Vozac v in Korisnici.ListaVozaca)
            {
                if (v.KorisnickoIme == korisnickoIme)
                {
                    return View("VozacPostoji");
                }
            }

            Korisnici.ListaVozaca.Add(vozac);

            return View("UspesnoVozac", vozac);
        }

        public ActionResult Voznja()
        {
            Dispecer dispecer = (Dispecer)Session["korisnik"];
            return View("Voznja", dispecer);
        }

        public ActionResult VoznjaNapravi(string ulica, string broj, string mesto, string pozivniBroj, string tipVozila, string dispecer)
        {
            if (ulica == "" || broj == "" || mesto == "" || pozivniBroj == "" || dispecer == "")
            {
                return View("VoznjaPonovo");
            }

            Adresa adresa = new Adresa(ulica, broj, mesto, pozivniBroj);
            Lokacija lokacija = new Lokacija("3", "4", adresa);

            Voznja voznja = new Voznja();
            voznja.DatumVremePorudzbine = DateTime.Now;

            Dispecer d = new Dispecer();
            foreach (Dispecer di in Korisnici.ListaDispecera)
            {
                if (di.KorisnickoIme == dispecer)
                {
                    d = di;
                }
            }

            voznja.Dispecer = d;
            voznja.LokacijaNaKojuTaxiDolazi = lokacija;

            TipAutomobila tip = TipAutomobila.PUTNICKI;
            if (tipVozila == "PUTNICKI")
            {
                tip = TipAutomobila.PUTNICKI;
            }
            else if (tipVozila == "KOMBI")
            {
                tip = TipAutomobila.KOMBI;
            }

            voznja.TipAutomobila = tip;

            voznja.StatusVoznje = Models.Enums.StatusVoznje.FORMIRANA;

            d.ListaVoznji.Add(voznja);

            Korisnici.ListaSvihVoznji.Add(voznja);           

            return View("UspesnoVoznja", voznja);
        }

        public ActionResult VoznjaObrada(string dispecer, string index)
        {
            Voznja voznja = new Voznja();
            int i = Int32.Parse(index);

            foreach (Dispecer d in Korisnici.ListaDispecera)
            {
                if(d.KorisnickoIme == dispecer)
                {
                    voznja = d.ListaVoznji[i - 1];
                }
            }

            return View("VoznjaObradi", voznja);
        }

        public ActionResult VoznjaObradjena(string slobodanVozac, string index)
        {
            Voznja voznja = new Voznja();
            Vozac vozac = new Vozac();
            int i = Int32.Parse(index);

            voznja = Korisnici.ListaSvihVoznji[i];

            foreach(Vozac v in Korisnici.ListaVozaca)
            {
                if(v.KorisnickoIme == slobodanVozac)
                {
                    v.Zauzet = true;
                    vozac = v;
                }
            }

            voznja.Vozac = vozac;
            voznja.StatusVoznje = Models.Enums.StatusVoznje.OBRADJENA;

            Korisnici.ListaSvihVoznji[i] = voznja;

            foreach (Vozac v in Korisnici.ListaVozaca)
            {
                if (v.KorisnickoIme == slobodanVozac)
                {
                    v.ListaVoznji.Add(voznja);
                }
            }

            return View("UspesnoObradjenaVoznja", voznja);
        }

        public ActionResult VoznjaInfo(string dispecer, string index)
        {
            Voznja voznja = new Voznja();
            int i = Int32.Parse(index);

            foreach (Dispecer d in Korisnici.ListaDispecera)
            {
                if (d.KorisnickoIme == dispecer)
                {
                    voznja = d.ListaVoznji[i - 1];
                }
            }
            return View("PrikazVoznje", voznja);
        }

        public ActionResult SveVoznjeInfo(string index)
        {
            Voznja voznja = new Voznja();
            int i = Int32.Parse(index);

            voznja = Korisnici.ListaSvihVoznji[i - 1];

            return View("PrikazVoznje", voznja);
        }



    }
}