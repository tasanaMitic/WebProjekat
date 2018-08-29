using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Lokacija
    {
        public string KoordinataX { get; set; }
        public string KoordinataY { get; set; }
        public Adresa Adresa { get; set; }

        public Lokacija()
        {

        }

        public Lokacija(string x, string y, Adresa adresa)
        {
            KoordinataX = x;
            KoordinataY = y;
            Adresa = adresa;
        }
    }
}