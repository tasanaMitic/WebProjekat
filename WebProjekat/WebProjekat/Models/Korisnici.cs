using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Korisnici
    {
        //public static List<Korisnik> ListaKorisnika { get; set; }
        public static List<Korisnik> ListaKorisnika = new List<Korisnik>();
        public static List<Musterija> ListaMusterija = new List<Musterija>();
        public static List<Dispecer> ListaDispecera = new List<Dispecer>();
        public static List<Vozac> ListaVozaca = new List<Vozac>();
        public static List<Voznja> ListaSvihVoznji = new List<Voznja>();

        public static void UpisKorisnika()
        {
            string line = "";

            if (File.Exists(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\korisnici.txt"))
            {
                StreamReader fileRead = new StreamReader(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\korisnici.txt");

                foreach (Korisnik k in ListaKorisnika)
                {
                    if (k.Uloga != Uloga.DISPECER)
                    {
                        line += string.Format(k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol.ToString() + ";" + k.Jmbg + ";" + k.KontaktTelefon + ";" + k.Email + ";" + k.Uloga.ToString() + "\n");
                    }
                }
                fileRead.Close();

                StreamWriter fileWrite = new StreamWriter(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\korisnici.txt");
                fileWrite.WriteLine(line);
                fileWrite.Close();
            }
            else
            {
                foreach (Korisnik k in ListaKorisnika)
                {
                    if (k.Uloga != Uloga.DISPECER)
                    {
                        line += string.Format(k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol.ToString() + ";"
                            + k.Jmbg + ";" + k.KontaktTelefon + ";" + k.Email + ";" + k.Uloga.ToString() + "\n");
                    }
                }

                StreamWriter fileWrite = new StreamWriter(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\korisnici.txt");
                fileWrite.WriteLine(line);
                fileWrite.Close();
            }
        }

        public static void UpisVozaca()
        {
            string line = "";

            if (File.Exists(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\vozaci.txt"))
            {
                StreamReader fileRead = new StreamReader(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\vozaci.txt");

                foreach(Vozac v in ListaVozaca)
                {
                    line += string.Format(v.KorisnickoIme + ";" + v.Lozinka + ";" + v.Ime + ";" + v.Prezime + ";" + v.Pol.ToString() + ";"
                        + v.Jmbg + ";" + v.KontaktTelefon + ";" + v.Email + ";" + v.Automobil.BrojTaxiVozila + ":" + v.Automobil.GodisteAutomobila + ":"
                        + v.Automobil.BrojRegistarskeOznake + ":" + v.Automobil.TipAutomobila.ToString() + ";" + v.Lokacija.Adresa.Ulica + ":"
                        + v.Lokacija.Adresa.Broj + ":" + v.Lokacija.Adresa.NaseljenoMesto + ":" + v.Lokacija.Adresa.PozivniBrojMesta + ";" + v.Uloga.ToString() + ";" + v.Zauzet.ToString() +  "\n");
                }

                fileRead.Close();

                StreamWriter fileWrite = new StreamWriter(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\vozaci.txt");
                fileWrite.WriteLine(line);
                fileWrite.Close();
            }
            else
            {
                if(ListaVozaca.Count != 0)
                {
                    foreach (Vozac v in ListaVozaca)
                    {
                        line += string.Format(v.KorisnickoIme + ";" + v.Lozinka + ";" + v.Ime + ";" + v.Prezime + ";" + v.Pol.ToString() + ";"
                            + v.Jmbg + ";" + v.KontaktTelefon + ";" + v.Email + ";" + v.Automobil.BrojTaxiVozila + ":" + v.Automobil.GodisteAutomobila + ":"
                            + v.Automobil.BrojRegistarskeOznake + ":" + v.Automobil.TipAutomobila.ToString() + ";" + v.Lokacija.Adresa.Ulica + ":"
                            + v.Lokacija.Adresa.Broj + ":" + v.Lokacija.Adresa.NaseljenoMesto + ":" + v.Lokacija.Adresa.PozivniBrojMesta + ";" + v.Uloga.ToString() + ";" + v.Zauzet.ToString() + "\n");
                    }

                    StreamWriter fileWrite = new StreamWriter(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\vozaci.txt");
                    fileWrite.WriteLine(line);
                    fileWrite.Close();
                }
            }
        }

        public static void UpisVoznje()
        {
            string line = "";

            if(File.Exists(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\voznje.txt"))
            {
                StreamReader fileRead = new StreamReader(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\voznje.txt");

                foreach(Voznja v in ListaSvihVoznji)
                {
                    line += string.Format(v.DatumVremePorudzbine.ToString() + ";" + v.Dispecer.KorisnickoIme + ";" + v.Iznos + ";" + v.Komentar.Opis + ";"
                        + v.Komentar.Ocena.ToString() + ";" + v.Komentar.DatumObjave.ToString() + ";" +  v.LokacijaNaKojuTaxiDolazi.Adresa.Ulica + ";" 
                        + v.LokacijaNaKojuTaxiDolazi.Adresa.Broj + ";" + v.LokacijaNaKojuTaxiDolazi.Adresa.NaseljenoMesto + ";" 
                        + v.LokacijaNaKojuTaxiDolazi.Adresa.PozivniBrojMesta + ";" + v.Odrediste.Adresa.Ulica + ";" + v.Odrediste.Adresa.Broj + ";" 
                        + v.Odrediste.Adresa.NaseljenoMesto + ";" + v.Odrediste.Adresa.PozivniBrojMesta + ";" + v.StatusVoznje.ToString() + ";" 
                        + v.TipAutomobila.ToString() + ";" + v.Vozac.KorisnickoIme + "\n");
                }

                fileRead.Close();

                StreamWriter fileWrite = new StreamWriter(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\voznje.txt");
                fileWrite.WriteLine(line);
                fileWrite.Close();
            }
            else
            {
                foreach (Voznja v in ListaSvihVoznji)
                {
                    line += string.Format(v.DatumVremePorudzbine.ToString() + ";" + v.Dispecer.KorisnickoIme + ";" + v.Iznos + ";" + v.Komentar.Opis + ";"
                        + v.Komentar.Ocena.ToString() + ";" + v.Komentar.DatumObjave.ToString() + ";" + v.LokacijaNaKojuTaxiDolazi.Adresa.Ulica + ";"
                        + v.LokacijaNaKojuTaxiDolazi.Adresa.Broj + ";" + v.LokacijaNaKojuTaxiDolazi.Adresa.NaseljenoMesto + ";"
                        + v.LokacijaNaKojuTaxiDolazi.Adresa.PozivniBrojMesta + ";" + v.Odrediste.Adresa.Ulica + ";" + v.Odrediste.Adresa.Broj + ";"
                        + v.Odrediste.Adresa.NaseljenoMesto + ";" + v.Odrediste.Adresa.PozivniBrojMesta + ";" + v.StatusVoznje.ToString() + ";"
                        + v.TipAutomobila.ToString() + ";" + v.Vozac.KorisnickoIme + "\n");
                }
                

                StreamWriter fileWrite = new StreamWriter(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\voznje.txt");
                fileWrite.WriteLine(line);
                fileWrite.Close();
            }
        }        
    }
}