using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProjekat.Models;
using WebProjekat.Models.Enums;

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
                if (v == Session["korisnik"])
                {
                    v.Prijavljen = false;
                    Session["korisnik"] = null;
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

            foreach (Vozac vo in Korisnici.ListaVozaca)
            {
                if (vo.KorisnickoIme == vozac)
                {
                    v = vo;
                }
            }

            v.Lokacija = lokacija;

            return View("UspesnaLokacija", v);
        }

        public ActionResult VoznjaInfo(string vozac, string index)
        {
            Voznja voznja = new Voznja();
            int i = Int32.Parse(index);

            foreach (Vozac v in Korisnici.ListaVozaca)
            {
                if (v.KorisnickoIme == vozac)
                {
                    voznja = v.ListaVoznji[i - 1];
                }
            }
            return View("PrikazVoznje", voznja);
        }

        public ActionResult SveVoznjeInfo(string index)
        {
            Voznja voznja = new Voznja();
            int i = Int32.Parse(index);

            foreach (Voznja v in Korisnici.ListaSvihVoznji)
            {
                if (Korisnici.ListaSvihVoznji[i - 1] == v)
                {
                    voznja = v;
                }
            }
            return View("PrikazVoznje", voznja);
        }

        public ActionResult StatusVoznje(string index, string vozac)
        {
            Voznja voznja = new Voznja();
            int i = Int32.Parse(index);

            foreach (Vozac v in Korisnici.ListaVozaca)
            {
                if (v.KorisnickoIme == vozac)
                {
                    voznja = v.ListaVoznji[i - 1];
                }
            }
            return View("StatusVoznje", voznja);
        }

        public ActionResult StatusVoznjePromenjen(string statusVoznje, string index)
        {            
            int i = Int32.Parse(index);
            Voznja voznja = Korisnici.ListaSvihVoznji[i];
            if(statusVoznje == "NEUSPESNA")
            {
                voznja.StatusVoznje = Models.Enums.StatusVoznje.NEUSPESNA;
                return View("NeuspesnaVoznja", voznja);
            }
            else
            {
                voznja.StatusVoznje = Models.Enums.StatusVoznje.USPESNA;
                return View("UspesnaVoznja", voznja);
            }         
        }

        public ActionResult UspesnaVoznja(string ulica, string broj, string mesto, string pozivniBroj, string iznos, string index)
        {
            int i = Int32.Parse(index);
            Voznja voznja = Korisnici.ListaSvihVoznji[i];

            if (ulica == "" || broj == "" || mesto == "" || pozivniBroj == "" || iznos == "")
            {
                return View("VoznjaPonovo", voznja);
            }                     

            Adresa adresa = new Adresa(ulica, broj, mesto, pozivniBroj);
            Lokacija lokacija = new Lokacija("3", "4", adresa);

            voznja.Odrediste = lokacija;
            voznja.Iznos = iznos;
            voznja.Vozac.Zauzet = false;

            return View("PrikazVoznje", voznja);
        }

        public ActionResult NeuspesnaVoznja(string ocena, string komentar, string index)
        {
            int i = Int32.Parse(index);
            Voznja voznja = Korisnici.ListaSvihVoznji[i];
            Komentar kom = new Komentar();

            if (ocena == "" || komentar == "")
            {
                return View("VoznjaPonovo", voznja);
            }

            Ocena o = Ocena.NEOCENJEN;
            if(ocena == "NEOCENJEN")
            {
                o = Ocena.NEOCENJEN;
            }
            else if(ocena == "VRLOLOSE")
            {
                o = Ocena.VRLOLOSE;
            }
            else if(ocena == "LOSE")
            {
                o = Ocena.LOSE;
            }
            else if (ocena == "DOBRO")
            {
                o = Ocena.DOBRO;
            }
            else if (ocena == "VRLODOBRO")
            {
                o = Ocena.VRLODOBRO;
            }
            else if (ocena == "ODLICNO")
            {
                o = Ocena.ODLICNO;
            }

            kom.Ocena = o;
            kom.DatumObjave = DateTime.Now;
            kom.KorisnikKomentara = voznja.Vozac;
            kom.Opis = komentar;
            kom.VoznjaKomentara = voznja;

            voznja.Komentar = kom;
            voznja.Vozac.Zauzet = false;

            Korisnici.ListaSvihVoznji[i] = voznja;

            return View("PrikazVoznje", voznja);
        }

        public ActionResult VoznjaPreuzmi(string index, string vozac)
        {
            Voznja voznja = new Voznja();
            int i = Int32.Parse(index);

            voznja = Korisnici.ListaSvihVoznji[i - 1];

            foreach(Vozac v in Korisnici.ListaVozaca)
            {
                if(v.KorisnickoIme == vozac)
                {
                    voznja.Vozac = v;
                    voznja.Vozac.Zauzet = true;
                    voznja.StatusVoznje = Models.Enums.StatusVoznje.PRIHVACENA;
                    voznja.Vozac.ListaVoznji.Add(voznja);
                }
            }

            Korisnici.ListaSvihVoznji[i - 1] = voznja;

            return View("UspesnoPreuzetaVoznja", voznja);
        }
    }
}