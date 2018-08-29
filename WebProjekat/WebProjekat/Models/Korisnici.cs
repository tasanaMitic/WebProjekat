using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Korisnici
    {
        public static List<Korisnik> ListaKorisnika { get; set; }
        public static List<Musterija> ListaMusterija { get; set; }
        public static List<Dispecer> ListaDispecera { get; set; }
        public static List<Vozac> ListaVozaca { get; set; }
        public static List<Voznja> ListaSvihVoznji { get; set; }

        public Korisnici()
        {
            ListaDispecera = new List<Dispecer>();
            ListaKorisnika = new List<Korisnik>();
            ListaMusterija = new List<Musterija>();
            ListaSvihVoznji = new List<Voznja>();
            ListaVozaca = new List<Vozac>();
        }
    }
}