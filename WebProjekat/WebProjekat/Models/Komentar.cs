using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjekat.Models.Enums;

namespace WebProjekat.Models
{
    public class Komentar
    {
        public string Opis { get; set; }
        public DateTime DatumObjave { get; set; }
        public Korisnik KorisnikKomentara { get; set; }
        public Voznja VoznjaKomentara { get; set; }
        public Ocena Ocena { get; set; }

        public Komentar()
        {

        }

        public Komentar(string opis, DateTime datumObjave, Voznja voznja, Ocena ocena, Korisnik korisnik)
        {
            Opis = opis;
            DatumObjave = datumObjave;
            KorisnikKomentara = korisnik;
            VoznjaKomentara = voznja;
            Ocena = ocena;
        }
    }
}