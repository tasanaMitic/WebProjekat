using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Korisnik
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol Pol { get; set; }
        public string Jmbg { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public Uloga Uloga { get; set; }
        public List<Voznja> ListaVoznji { get; set; }
        public bool Prijavljen { get; set; }
        public Models.Enums.StatusVoznje Filter { get; set; }
        public List<Voznja> SortiraneVoznje { get; set; }

        public Korisnik()
        {
            ListaVoznji = new List<Voznja>();
            SortiraneVoznje = new List<Voznja>();
        }

        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, Pol pol, string jmbg, string kontaktTelefon, string email, Uloga uloga)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Jmbg = jmbg;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            Uloga = uloga;
            ListaVoznji = new List<Voznja>();
            Prijavljen = false;
            Filter = Enums.StatusVoznje.NEMA;
            SortiraneVoznje = new List<Voznja>();
        }
    }
}