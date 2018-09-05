using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class PrijavaController : Controller
    {
        // GET: Prijava
        public ActionResult Index()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];

            if(korisnik == null)
            {
                korisnik = new Korisnik();
                Session["korisnik"] = korisnik;
                return View("Prijava");
            }
            else
            {
                foreach (Dispecer d in Korisnici.ListaDispecera)
                {
                    if (d.KorisnickoIme == korisnik.KorisnickoIme && d.Lozinka != korisnik.Lozinka)
                    {
                        ViewBag.Description = "Prijava je neuspesna. Lozinka nije ispravna!";
                        return View("Prijava");
                    }
                    else if (d.KorisnickoIme == korisnik.KorisnickoIme && d.Lozinka == korisnik.Lozinka)
                    {
                        d.Prijavljen = true;
                        return View("PrikazDispecera", d);
                    }
                }

                foreach (Vozac v in Korisnici.ListaVozaca)
                {
                    if (v.KorisnickoIme == korisnik.KorisnickoIme && v.Lozinka != korisnik.Lozinka)
                    {
                        ViewBag.Description = "Prijava je neuspesna. Lozinka nije ispravna!";
                        return View("Prijava");
                    }
                    else if (v.KorisnickoIme == korisnik.KorisnickoIme && v.Lozinka == korisnik.Lozinka)
                    {
                        v.Prijavljen = true;
                        return View("PrikazVozaca", v);
                    }
                }

                foreach (Musterija m in Korisnici.ListaMusterija)
                {
                    if(m.KorisnickoIme == korisnik.KorisnickoIme && m.Lozinka != korisnik.Lozinka)
                    {
                        ViewBag.Description = "Prijava je neuspesna. Lozinka nije ispravna!";
                        return View("Prijava");
                    }
                    else if(m.KorisnickoIme == korisnik.KorisnickoIme && m.Lozinka == korisnik.Lozinka)
                    {
                        m.Prijavljen = true;
                        return View("PrikazMusterije", m);
                    }
                }               

                ViewBag.Description = "Ne postoji korisnik sa tim korisničkim imenom!";
                return View("Prijava");
            }
        }

        [HttpPost]
        public ActionResult LogIn(string korisnickoIme, string lozinka)
        {
            foreach (Dispecer d in Korisnici.ListaDispecera)
            {
                if (d.KorisnickoIme == korisnickoIme && d.Lozinka != lozinka)
                {
                    ViewBag.Description = "Prijava je neuspesna. Lozinka nije ispravna!";
                    return View("Prijava");
                }
                else if (d.KorisnickoIme == korisnickoIme && d.Lozinka == lozinka)
                {
                    d.Prijavljen = true;
                    Session["korisnik"] = d;
                    return View("PrikazDispecera", d);
                }
            }

            foreach (Vozac v in Korisnici.ListaVozaca)
            {
                if (v.KorisnickoIme == korisnickoIme && v.Lozinka != lozinka)
                {
                    ViewBag.Description = "Prijava je neuspesna. Lozinka nije ispravna!";
                    return View("Prijava");
                }
                else if (v.KorisnickoIme == korisnickoIme && v.Lozinka == lozinka)
                {
                    v.Prijavljen = true;
                    Session["korisnik"] = v;
                    return View("PrikazVozaca", v);
                }
            }

            foreach (Musterija m in Korisnici.ListaMusterija)
            {
                if (m.KorisnickoIme == korisnickoIme && m.Lozinka != lozinka)
                {
                    ViewBag.Description = "Prijava je neuspesna. Lozinka nije ispravna!";
                    return View("Prijava");
                }
                else if (m.KorisnickoIme == korisnickoIme && m.Lozinka == lozinka)
                {
                    m.Prijavljen = true;
                    Session["korisnik"] = m;
                    return View("PrikazMusterije", m);
                }
            }

            ViewBag.Description = "Ne postoji korisnik sa tim korisničkim imenom!";
            return View("Prijava");
        }

        public ActionResult Registracija()
        {
            return View("Registracija");
        }

        [HttpPost]
        public ActionResult Registracija(string ime, string prezime, string pol, string korisnickoIme, string lozinka, string jmbg, string brojTelefona, string email)
        {
            if (ime == "" || prezime == "" || korisnickoIme == "" || lozinka == "" || jmbg == "" || brojTelefona == "" || email == "")
            {
                return View("RegistracijaPonovo");
            }

            Pol p = Pol.MUSKI;
            if(pol == "MUSKI")
            {
                p = Pol.MUSKI;
            }
            else
            {
                p = Pol.ZENSKI;
            }

            Musterija musterija = new Musterija(korisnickoIme, lozinka, ime, prezime, p, jmbg, brojTelefona, email, Uloga.MUSTERIJA);

            if(Korisnici.ListaMusterija != null)
            {
                foreach(Korisnik k in Korisnici.ListaMusterija)
                {
                    if(k.KorisnickoIme == musterija.KorisnickoIme)
                    {
                        return View("RegistracijaNeuspesna");
                    }
                }
            }

            Korisnici.ListaKorisnika.Add(musterija);
            Korisnici.ListaMusterija.Add(musterija as Musterija);

            return View("Prijava");
        }        
    }

}