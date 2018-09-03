using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class MusterijaController : Controller
    {
        // GET: Musterija
        public ActionResult Index()
        {
            Musterija musterija = (Musterija)Session["korisnik"];
            return View("PrikazMusterije", musterija);
        }

        public ActionResult Odjava()
        {
            foreach (Musterija m in Korisnici.ListaMusterija)
            {
                if (m == Session["korisnik"])
                {
                    m.Prijavljen = false;
                    Session["korisnik"] = null;
                }
            }

            return View("Prijava");
        }

        public ActionResult Izmena()
        {
            Musterija musterija = (Musterija)Session["korisnik"];
            return View("Izmena", musterija);
        }

        public ActionResult IzmeniPodatke(string korisnickoIme, string lozinka, string ime, string prezime, string jmbg, string pol, string email, string brojTelefona)
        {
            Musterija musterija = new Musterija();
            Pol p = Pol.MUSKI;

            if (pol == "MUSKI")
            {
                p = Pol.MUSKI;
            }
            else if (pol == "ZENSKI")
            {
                p = Pol.ZENSKI;
            }

            foreach (Musterija m in Korisnici.ListaMusterija)
            {
                if (m.KorisnickoIme == korisnickoIme)
                {
                    m.Ime = ime;
                    m.Prezime = prezime;
                    m.Pol = p;
                    m.Lozinka = lozinka;
                    m.Jmbg = jmbg;
                    m.KontaktTelefon = brojTelefona;
                    m.Email = email;
                    musterija = m;
                    break;
                }
            }

            Korisnik korisnik = new Korisnik();
            foreach (Korisnik k in Korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme == musterija.KorisnickoIme)
                {
                    korisnik = musterija;
                    break;
                }
            }

            return View("PrikazMusterije", musterija);

        }

    }
}