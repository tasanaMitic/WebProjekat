using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class VozacController : Controller
    {
        // GET: Vozac
        public ActionResult Index()
        {
            Vozac vozac = (Vozac)Session["korisnik"];

            return View("PrikazVozaca", vozac);
        }

        public ActionResult Odjava()
        {
            foreach (Vozac v in Korisnici.ListaVozaca)
            {
                if (v == HttpContext.Application["Vozac"])
                {
                    v.Prijavljen = false;
                    HttpContext.Application["Vozac"] = null;
                }
            }

            return View("Prijava");
        }

        public ActionResult Izmena()
        {
            Vozac vozac = (Vozac)Session["korisnik"];
            return View("Izmena", vozac);
        }

        public ActionResult IzmeniPodatke(string korisnickoIme, string lozinka, string ime, string prezime, string jmbg, string pol, string email, string brojTelefona)
        {
            Vozac vozac = new Vozac();
            Pol p = Pol.MUSKI;

            if (pol == "MUSKI")
            {
                p = Pol.MUSKI;
            }
            else if (pol == "ZENSKI")
            {
                p = Pol.ZENSKI;
            }

            foreach (Vozac v in Korisnici.ListaVozaca)
            {
                if (v.KorisnickoIme == korisnickoIme)
                {
                    v.Ime = ime;
                    v.Prezime = prezime;
                    v.Pol = p;
                    v.Lozinka = lozinka;
                    v.Jmbg = jmbg;
                    v.KontaktTelefon = brojTelefona;
                    v.Email = email;
                    vozac = v;
                    break;
                }
            }

            Korisnik korisnik = new Korisnik();
            foreach (Korisnik k in Korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme == vozac.KorisnickoIme)
                {
                    korisnik = vozac;
                    break;
                }
            }

            return View("PrikazVozaca", vozac);
        }

        public ActionResult Lokacija()
        {
            Vozac vozac = (Vozac)Session["korisnik"];
            return View("Lokacija", vozac);
        }

        public ActionResult IzmeniLokaciju(string ulica, string broj, string mesto, string pozivniBroj, string vozac)
        {
            if (ulica == "" || broj == "" || mesto == "" || pozivniBroj == "" || vozac == "")
            {
                return View("LokacijaPonovo");
            }

            Adresa adresa = new Adresa(ulica, broj, mesto, pozivniBroj);
            Lokacija lokacija = new Lokacija("3", "4", adresa);

            Vozac v = new Vozac();

            foreach(Vozac vo in Korisnici.ListaVozaca)
            {
                if(vo.KorisnickoIme == vozac)
                {
                    v = vo;
                }
            }

            v.Lokacija = lokacija;

            return View("UspesnaLokacija", v);
        }
    }
}