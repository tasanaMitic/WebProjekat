using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjekat.Models.Enums;

namespace WebProjekat.Models
{
    public class Voznja
    {
        public DateTime DatumVremePorudzbine { get; set; } 
        public Lokacija LokacijaNaKojuTaxiDolazi { get; set; }
        public TipAutomobila TipAutomobila { get; set; }
        public Lokacija Odrediste { get; set; }
        public Dispecer Dispecer { get; set; }
        public Vozac Vozac { get; set; }
        public int Iznos { get; set; }
        public Komentar Komentar { get; set; }
        public StatusVoznje StatusVoznje { get; set; }

        public Voznja()
        {

        }

        public Voznja(DateTime datumVreme, Lokacija lokacija, TipAutomobila tipAutomobila)
        {
            DatumVremePorudzbine = datumVreme;
            LokacijaNaKojuTaxiDolazi = lokacija;
            TipAutomobila = tipAutomobila;
        }
    }
}