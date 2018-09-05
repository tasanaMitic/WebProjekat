using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Dispecer : Korisnik
    {
        public Dispecer()
        {
            ListaVoznji = new List<Voznja>();
        }

        public Dispecer(string korisnickoIme, string lozinka, string ime, string prezime, Pol pol, string jmbg, string kontaktTelefon, string email) : base(korisnickoIme, lozinka, ime, prezime, pol, jmbg, kontaktTelefon, email)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Jmbg = jmbg;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            Uloga = Uloga.DISPECER;
            ListaVoznji = new List<Voznja>();
            SortiraneVoznje = ListaVoznji;
            Prijavljen = false;
        }
    }
}