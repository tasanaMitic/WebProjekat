using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Automobil
    {
        public Vozac Vozac { get; set; }
        public string GodisteAutomobila { get; set; }
        public string BrojRegistarskeOznake { get; set; }
        public string BrojTaxiVozila { get; set; }
        public TipAutomobila TipAutomobila { get; set; }

        public Automobil()
        {

        }

        public Automobil(Vozac vozac, string godisteAutomobila, string brojRegistarskeOznake, string brojTaxiVozila, TipAutomobila tipAutomobila)
        {
            Vozac = vozac;
            GodisteAutomobila = godisteAutomobila;
            BrojRegistarskeOznake = brojRegistarskeOznake;
            BrojTaxiVozila = brojTaxiVozila;
            tipAutomobila = TipAutomobila;
        }
    }
}