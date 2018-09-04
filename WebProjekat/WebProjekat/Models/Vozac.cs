﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    [Serializable]
    public class Vozac : Korisnik
    {
        public Automobil Automobil { get; set; }
        public Lokacija Lokacija { get; set; }
        public bool Zauzet { get; set; }

        public Vozac()
        {
            ListaVoznji = new List<Voznja>();
        }

        public Vozac(Automobil automobil, Lokacija lokacija)
        {
            Automobil = automobil;
            Lokacija = lokacija;
            ListaVoznji = new List<Voznja>();
            SortiraneVoznje = new List<Voznja>();
        }

        public Vozac(string korisnickoIme, string lozinka, string ime, string prezime, Pol pol, string jmbg, string kontaktTelefon, string email, Uloga uloga,bool zauzet, string ulica, string broj, string mesto, string pozivniBroj) : base(korisnickoIme, lozinka, ime, prezime, pol, jmbg, kontaktTelefon, email, uloga)
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
            Zauzet = zauzet;
            ListaVoznji = new List<Voznja>();
            SortiraneVoznje = new List<Voznja>();

            Adresa adresa = new Adresa();
            adresa.Ulica = ulica;
            adresa.Broj = broj;
            adresa.NaseljenoMesto = mesto;
            adresa.PozivniBrojMesta = pozivniBroj;

            Lokacija lokacija = new Lokacija("2","3", adresa);
            Lokacija = lokacija;
        }
    }
}