using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Vozac : Korisnik
    {
        public Automobil Automobil { get; set; }
        public Lokacija Lokacija { get; set; }
        public bool Zauzet { get; set; }

        public Vozac()
        {
            ListaVoznji = new List<Voznja>();
        }

        public Vozac(Korisnik korisnik, Automobil automobil, Lokacija lokacija)
        {
            this.KorisnickoIme = korisnik.KorisnickoIme;
            this.Lozinka = korisnik.Lozinka;
            this.Ime = korisnik.Ime;
            this.Prezime = korisnik.Prezime;
            this.Pol = korisnik.Pol;
            this.Jmbg = korisnik.Jmbg;
            this.KontaktTelefon = korisnik.KontaktTelefon;
            this.Email = korisnik.Email;
            this.Uloga = Uloga.VOZAC;
            this.Filter = Enums.StatusVoznje.NEMA;
            Zauzet = false;

            this.Automobil = automobil;
            this.Lokacija = lokacija;
            ListaVoznji = new List<Voznja>();
            SortiraneVoznje = ListaVoznji;
        }

    }
}