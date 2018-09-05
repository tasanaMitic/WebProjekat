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
                }
            }

            foreach (Korisnik k in Korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme == vozac.KorisnickoIme)
                {
                    k.Ime = ime;
                    k.Prezime = prezime;
                    k.Pol = p;
                    k.Lozinka = lozinka;
                    k.Jmbg = jmbg;
                    k.KontaktTelefon = brojTelefona;
                    k.Email = email;
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
            Vozac vo = new Vozac();

            foreach (Vozac v in Korisnici.ListaVozaca)
            {
                if (v.KorisnickoIme == vozac)
                {
                    v.Lokacija = lokacija;
                    vo = v;
                }
            }

            return View("UspesnaLokacija", vo);
        }

        public ActionResult VoznjaInfo(string vozac, string index)
        {
            int i = Int32.Parse(index);
            Voznja voznja = new Voznja();

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
            if(statusVoznje == "NEUSPESNA")
            {
                Korisnici.ListaSvihVoznji[i].StatusVoznje = Models.Enums.StatusVoznje.NEUSPESNA;
                return View("NeuspesnaVoznja", Korisnici.ListaSvihVoznji[i]);
            }
            else
            {
                Korisnici.ListaSvihVoznji[i].StatusVoznje = Models.Enums.StatusVoznje.USPESNA;
                return View("UspesnaVoznja", Korisnici.ListaSvihVoznji[i]);
            }         
        }

        public ActionResult UspesnaVoznja(string ulica, string broj, string mesto, string pozivniBroj, string iznos, string index, string ocena, string komentar)
        {
            int i = Int32.Parse(index);
            Voznja voznja = Korisnici.ListaSvihVoznji[i];

            if (ulica == "" || broj == "" || mesto == "" || pozivniBroj == "" || iznos == "")
            {
                return View("VoznjaPonovo", voznja);
            }

            Ocena o = Ocena.NEOCENJEN;
            if (ocena == "NEOCENJEN")
            {
                o = Ocena.NEOCENJEN;
            }
            else if (ocena == "VRLOLOSE")
            {
                o = Ocena.VRLOLOSE;
            }
            else if (ocena == "LOSE")
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

            Adresa adresa = new Adresa(ulica, broj, mesto, pozivniBroj);
            Lokacija lokacija = new Lokacija("3", "4", adresa);

            if(komentar == "")
            {
                voznja.Komentar.Opis = "Nema komentara";
            }
            else
            {
                voznja.Komentar.Opis = komentar;
            }

            voznja.Odrediste = lokacija;
            voznja.Iznos = Int32.Parse(iznos);
            voznja.Vozac.Zauzet = false;
            voznja.Komentar.KorisnikKomentara = voznja.Vozac;
            voznja.Komentar.Ocena = o;
            voznja.Komentar.DatumObjave = DateTime.Now;
            voznja.Vozac.SortiraneVoznje = voznja.Vozac.ListaVoznji;

            Korisnici.ListaSvihVoznji[i] = voznja;

            return View("PrikazVoznje", voznja);
        }

        public ActionResult NeuspesnaVoznja(string ocena, string komentar, string index)
        {
            int i = Int32.Parse(index);
            Voznja voznja = new Voznja();
            Komentar kom = new Komentar();
            voznja = Korisnici.ListaSvihVoznji[i];

            if (komentar == "")
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
            voznja.Vozac.SortiraneVoznje = voznja.Vozac.ListaVoznji;

            Korisnici.ListaSvihVoznji[i] = voznja;

            return View("PrikazVoznje", Korisnici.ListaSvihVoznji[i]);
        }

        public ActionResult VoznjaPreuzmi(string index, string vozac)
        {
            int i = Int32.Parse(index);

            foreach(Vozac v in Korisnici.ListaVozaca)
            {
                if(v.KorisnickoIme == vozac)
                {
                    Korisnici.ListaSvihVoznji[i - 1].Vozac = v;
                    Korisnici.ListaSvihVoznji[i - 1].Vozac.Zauzet = true;
                    Korisnici.ListaSvihVoznji[i - 1].StatusVoznje = Models.Enums.StatusVoznje.PRIHVACENA;
                    Korisnici.ListaSvihVoznji[i - 1].Dispecer = null;
                    Korisnici.ListaSvihVoznji[i - 1].Vozac.ListaVoznji.Add(Korisnici.ListaSvihVoznji[i - 1]);
                    Korisnici.ListaSvihVoznji[i - 1].Vozac.SortiraneVoznje = Korisnici.ListaSvihVoznji[i - 1].Vozac.ListaVoznji;

                    v.Zauzet = true;
                    v.ListaVoznji.Add(Korisnici.ListaSvihVoznji[i - 1]);
                    v.SortiraneVoznje = v.ListaVoznji;
                }
            }

            return View("UspesnoPreuzetaVoznja", Korisnici.ListaSvihVoznji[i - 1]);
        }

        public ActionResult Tabela(string filter, string sort, string vozac, string odDatum, string doDatum, string odOcena, string doOcena, string odCena, string doCena)
        {
            Vozac vo = new Vozac();
            WebProjekat.Models.Enums.StatusVoznje statusVoznje = Models.Enums.StatusVoznje.FORMIRANA;

            switch (filter)
            {
                case "FORMIRANA":
                    statusVoznje = Models.Enums.StatusVoznje.FORMIRANA;
                    break;
                case "OBRADJENA":
                    statusVoznje = Models.Enums.StatusVoznje.OBRADJENA;
                    break;
                case "PRIHVACENA":
                    statusVoznje = Models.Enums.StatusVoznje.PRIHVACENA;
                    break;
                case "NEUSPESNA":
                    statusVoznje = Models.Enums.StatusVoznje.NEUSPESNA;
                    break;
                case "USPESNA":
                    statusVoznje = Models.Enums.StatusVoznje.USPESNA;
                    break;
                case "SVE":
                    statusVoznje = Models.Enums.StatusVoznje.NEMA;
                    break;
            }

            foreach (Vozac v in Korisnici.ListaVozaca)
            {
                if (v.KorisnickoIme == vozac)
                {
                    v.Filter = statusVoznje;
                    v.SortiraneVoznje = v.ListaVoznji;
                    vo = v;
                }
            }

            switch (sort)
            {
                case "DATUM":
                    vo.SortiraneVoznje = vo.ListaVoznji.OrderByDescending(v => v.DatumVremePorudzbine).ToList();
                    break;
                case "OCENA":
                    vo.SortiraneVoznje = vo.ListaVoznji.OrderByDescending(v => v.Komentar.Ocena).ToList();
                    break;
                case "SVE":
                    vo.SortiraneVoznje = vo.ListaVoznji;
                    break;
            }

            //PRETRAGA
            //OD DATUMA
            List<Voznja> izbaciti = new List<Voznja>();

            if (odDatum != "prazno")
            {
                DateTime datumOd = new DateTime();

                foreach (Voznja v in vo.SortiraneVoznje)
                {
                    if (v.DatumVremePorudzbine.ToString() == odDatum)
                    {
                        datumOd = v.DatumVremePorudzbine;
                    }
                }

                foreach (Voznja v in vo.SortiraneVoznje)
                {
                    if (v.DatumVremePorudzbine.CompareTo(datumOd) < 0)
                    {
                        if (izbaciti.Contains(v))
                        {

                        }
                        else
                        {
                            izbaciti.Add(v);
                        }
                    }
                }
            }

            //DO DATUM
            if (doDatum != "prazno")
            {
                DateTime datumDo = new DateTime();

                foreach (Voznja v in vo.SortiraneVoznje)
                {
                    if (v.DatumVremePorudzbine.ToString() == doDatum)
                    {
                        datumDo = v.DatumVremePorudzbine;
                    }
                }

                foreach (Voznja v in vo.SortiraneVoznje)
                {
                    if (v.DatumVremePorudzbine.CompareTo(datumDo) > 0)
                    {
                        if (izbaciti.Contains(v))
                        {

                        }
                        else
                        {
                            izbaciti.Add(v);
                        }
                    }
                }
            }

            //OCENA
            WebProjekat.Models.Enums.Ocena ocenaOd = Models.Enums.Ocena.NEOCENJEN;
            WebProjekat.Models.Enums.Ocena ocenaDo = Models.Enums.Ocena.ODLICNO;

            switch (odOcena)
            {
                case "NEOCENJEN":
                    ocenaOd = Models.Enums.Ocena.NEOCENJEN;
                    break;
                case "VRLOLOSE":
                    ocenaOd = Models.Enums.Ocena.VRLOLOSE;
                    break;
                case "LOSE":
                    ocenaOd = Models.Enums.Ocena.LOSE;
                    break;
                case "DOBRO":
                    ocenaOd = Models.Enums.Ocena.DOBRO;
                    break;
                case "VRLODOBRO":
                    ocenaOd = Models.Enums.Ocena.VRLODOBRO;
                    break;
                case "ODLICNO":
                    ocenaOd = Models.Enums.Ocena.ODLICNO;
                    break;
            }

            switch (doOcena)
            {
                case "NEOCENJEN":
                    ocenaDo = Models.Enums.Ocena.NEOCENJEN;
                    break;
                case "VRLOLOSE":
                    ocenaDo = Models.Enums.Ocena.VRLOLOSE;
                    break;
                case "LOSE":
                    ocenaDo = Models.Enums.Ocena.LOSE;
                    break;
                case "DOBRO":
                    ocenaDo = Models.Enums.Ocena.DOBRO;
                    break;
                case "VRLODOBRO":
                    ocenaDo = Models.Enums.Ocena.VRLODOBRO;
                    break;
                case "ODLICNO":
                    ocenaDo = Models.Enums.Ocena.ODLICNO;
                    break;
            }

            foreach (Voznja v in vo.SortiraneVoznje)
            {
                if (v.Komentar.Ocena < ocenaOd)
                {
                    if (izbaciti.Contains(v))
                    {

                    }
                    else
                    {
                        izbaciti.Add(v);
                    }
                }

                if (v.Komentar.Ocena > ocenaDo)
                {
                    if (izbaciti.Contains(v))
                    {

                    }
                    else
                    {
                        izbaciti.Add(v);
                    }
                }
            }

            //CENA
            foreach (Voznja v in vo.SortiraneVoznje)
            {
                if (odCena != "")
                {
                    if (v.Iznos < Int32.Parse(odCena))
                    {
                        if (izbaciti.Contains(v))
                        {

                        }
                        else
                        {
                            izbaciti.Add(v);
                        }
                    }
                }

                if (doCena != "")
                {
                    if (v.Iznos > Int32.Parse(doCena))
                    {
                        if (izbaciti.Contains(v))
                        {

                        }
                        else
                        {
                            izbaciti.Add(v);
                        }
                    }
                }
            }

            foreach (Voznja v in izbaciti)
            {
                if (vo.SortiraneVoznje.Contains(v))
                {
                    vo.SortiraneVoznje = vo.SortiraneVoznje.Except(izbaciti).ToList();
                }
            }

            return View("PrikazVozaca", vo);
        }
    }
}