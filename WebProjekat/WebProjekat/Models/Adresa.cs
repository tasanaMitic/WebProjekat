using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Adresa
    {
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string NaseljenoMesto { get; set; }
        public string PozivniBrojMesta { get; set; }

        public Adresa()
        {

        }

        public Adresa(string ulica, string broj, string naseljenoMesto, string pozivniBroj)
        {
            Ulica = ulica;
            Broj = broj;
            NaseljenoMesto = naseljenoMesto;
            PozivniBrojMesta = pozivniBroj;
        }
    }
}