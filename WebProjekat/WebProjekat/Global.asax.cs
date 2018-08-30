using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml;
using System.Xml.Serialization;
using WebProjekat.App_Start;
using WebProjekat.Models;
using WebProjekat.Models.Enums;

namespace WebProjekat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Korisnici korisnici = new Korisnici();
            Ucitavanje();
        }

        public static XmlElement SerializeToXmlElement(object o)
        {
            XmlDocument doc = new XmlDocument();
            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(o.GetType()).Serialize(writer, o);
            }
            return doc.DocumentElement;
        }

        private void Ucitavanje()
        {
            if (File.Exists(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\korisnici.xml"))
            {
                string ime = "";
                string prezime = "";
                string korisnickoIme = "";
                string lozinka = "";
                string jmbg = "";
                string telefon = "";
                string email = "";
                Pol pol = Pol.MUSKI;
                Uloga uloga = Uloga.DISPECER;
                string x = "";
                string y = "";
                string brojUlice = "";
                string pozivniBroj = "";
                string ulica = "";
                string grad = "";
                string godiste = "";
                string reg = "";
                TipAutomobila tip = TipAutomobila.KOMBI;
                string taxiBr = "";

                using (XmlReader reader = XmlReader.Create(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\korisnici.xml"))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Name.Equals("Korisnik"))
                        {
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            ime = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            prezime = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            jmbg = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            korisnickoIme = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            lozinka = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            switch (reader.Value)
                            {
                                case "MUSKI":
                                    pol = Pol.MUSKI;
                                    break;
                                case "ZENSKI":
                                    pol = Pol.ZENSKI;
                                    break;
                            }
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            email = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            telefon = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            switch (reader.Value)
                            {
                                case "DISPECER":
                                    uloga = Uloga.DISPECER;
                                    break;
                                case "VOZAC":
                                    uloga = Uloga.VOZAC;
                                    break;
                                case "MUSTERIJA":
                                    uloga = Uloga.MUSTERIJA;
                                    break;
                            }

                            if (uloga == Uloga.MUSTERIJA)
                            {
                                Musterija musterija = new Musterija(korisnickoIme, lozinka, ime, prezime, pol, jmbg, telefon, email, uloga);
                                Korisnici.ListaMusterija.Add(musterija);
                                Korisnik korisnik = musterija;
                                Korisnici.ListaKorisnika.Add(korisnik);
                            }
                            else if (uloga == Uloga.DISPECER)
                            {
                                Dispecer dispecer = new Dispecer(korisnickoIme, lozinka, ime, prezime, pol, jmbg, telefon, email, uloga);
                                Korisnici.ListaDispecera.Add(dispecer);
                                Korisnik korisnik = dispecer;
                                Korisnici.ListaKorisnika.Add(korisnik);
                            }
                            else if (uloga == Uloga.VOZAC)
                            {
                                Vozac vozac = new Vozac(korisnickoIme, lozinka, ime, prezime, pol, jmbg, telefon, email, uloga, ulica, brojUlice, grad, pozivniBroj);


                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                godiste = reader.Value;
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reg = reader.Value;
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                taxiBr = reader.Value;
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                switch (reader.Value)
                                {
                                    case "PUTNICKI":
                                        tip = TipAutomobila.PUTNICKI;
                                        break;
                                    case "KOMBI":
                                        tip = TipAutomobila.KOMBI;
                                        break;
                                }
                                Automobil automobil = new Automobil(vozac, godiste, reg, taxiBr, tip);

                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                ulica = reader.Value;
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                brojUlice = reader.Value;
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                grad = reader.Value;
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                pozivniBroj = reader.Value;
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                x = reader.Value;
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                reader.Read();
                                y = reader.Value;
                                Adresa adresa = new Adresa(ulica, brojUlice, grad, pozivniBroj);
                                Lokacija lokacija = new Lokacija(x, y, adresa);

                                vozac.Automobil = automobil;
                                vozac.Lokacija = lokacija;

                                Korisnici.ListaVozaca.Add(vozac);
                                Korisnik korisnik = vozac;
                                Korisnici.ListaKorisnika.Add(korisnik);
                            }
                        }
                    }

                }
            }

            if (Korisnici.ListaDispecera.Count() == 0)
            {
                string line;
                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\dispeceri.txt");

                while ((line = file.ReadLine()) != null)
                {
                    string[] polja = line.Split(';');
                    //int i = 0;
                   // while (polja[i * 9] != null)
                   // {
                        Dispecer dispecer = new Dispecer();

                        dispecer.KorisnickoIme = polja[0];
                        dispecer.Lozinka = polja[1];
                        dispecer.Ime = polja[2];
                        dispecer.Prezime = polja[3];
                        if (polja[4].Equals("MUSKI"))
                            dispecer.Pol = Pol.MUSKI;
                        else
                            dispecer.Pol = Pol.ZENSKI;
                        dispecer.Jmbg = polja[5];
                        dispecer.KontaktTelefon = polja[6];
                        dispecer.Email = polja[7];
                        dispecer.Uloga = Uloga.DISPECER;

                        Korisnici.ListaKorisnika.Add(dispecer);
                        Korisnici.ListaDispecera.Add(dispecer);
                       // i++;
                   // }
                }

                file.Close();

                string path = @"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\korisnici.xml";
                XmlWriter writer = null;
                try
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.IndentChars = ("\t");
                    settings.OmitXmlDeclaration = true;

                    writer = XmlWriter.Create(path, settings);
                    writer.WriteStartElement("Prijavljeni");
                    foreach (Korisnik k in Korisnici.ListaKorisnika)
                    {
                        k.Jmbg.ToString();
                        writer.WriteStartElement("Korisnik");
                        writer.WriteElementString("Ime", k.Ime);
                        writer.WriteElementString("Prezime", k.Prezime);
                        writer.WriteElementString("Jmbg", k.Jmbg.ToString());
                        writer.WriteElementString("KorisnickoIme", k.KorisnickoIme);
                        writer.WriteElementString("Lozinka", k.Lozinka);
                        writer.WriteElementString("Pol", k.Pol.ToString());
                        writer.WriteElementString("E-Mail", k.Email);
                        writer.WriteElementString("KontaktTelefon", k.KontaktTelefon);
                        writer.WriteElementString("Uloga", k.Uloga.ToString());
                        writer.WriteStartElement("Voznje");
                        int i = 1;
                        foreach (Voznja v in k.ListaVoznji)
                        {
                            writer.WriteStartElement("VoznjaBroj" + i.ToString());
                            writer.WriteElementString("DatumPorudzbine", v.DatumVremePorudzbine.ToString());
                            writer.WriteElementString("PocetnaLokacija", v.LokacijaNaKojuTaxiDolazi.ToString());
                            writer.WriteElementString("KrajnjaLokacija", v.Odrediste.ToString());
                            writer.WriteElementString("TipVozila", v.TipAutomobila.ToString());
                            writer.WriteElementString("MusterijaIme", v.Musterija.Ime);
                            writer.WriteElementString("MusterijaPrezime", v.Musterija.Prezime);
                            writer.WriteElementString("VozacIme", v.Vozac.Ime);
                            writer.WriteElementString("VozacPrezime", v.Vozac.Prezime);
                            writer.WriteElementString("DispecerIme", v.Dispecer.Ime);
                            writer.WriteElementString("DispececrPrezime", v.Dispecer.Prezime);
                            writer.WriteElementString("Iznos", v.Iznos.ToString());
                            writer.WriteEndElement();
                            i++;
                        }
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.Flush();
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
            else
            {
                UcitajVoznje();
            }
        }

        private void UcitajVoznje()
        {
            if (File.Exists(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\voznje.xml"))
            {
                string datum = "";
                string pocetna = "";
                string krajnja = "";
                TipAutomobila tip = TipAutomobila.KOMBI;
                string musterijaI = "";
                string musterijaP = "";
                string vozacI = "";
                string vozacP = "";
                string dispecerI = "";
                string dispecerP = "";
                StatusVoznje status = StatusVoznje.FORMIRANA;
                string komentar = "";
                string komentarDatum = "";
                Ocena ocena = Ocena.NEOCENJEN;
                string iznos = "0";

                using (XmlReader reader = XmlReader.Create(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\voznje.xml"))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Name.Equals("Voznja"))
                        {
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            datum = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            pocetna = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            krajnja = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            switch (reader.Value)
                            {
                                case "PUTNICKI":
                                    tip = TipAutomobila.PUTNICKI;
                                    break;
                                case "KOMBI":
                                    tip = TipAutomobila.KOMBI;
                                    break;
                            }
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            musterijaI = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            musterijaP = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            vozacI = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            vozacP = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            dispecerI = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            dispecerP = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            switch (reader.Value)
                            {
                                case "KREIRANA":
                                    status = StatusVoznje.KREIRANA;
                                    break;
                                case "FORMIRANA":
                                    status = StatusVoznje.FORMIRANA;
                                    break;
                                case "NEUSPESNA":
                                    status = StatusVoznje.NEUSPESNA;
                                    break;
                                case "OBRADJENA":
                                    status = StatusVoznje.OBRADJENA;
                                    break;
                                case "OTKAZANA":
                                    status = StatusVoznje.OTKAZANA;
                                    break;
                                case "PRIHVACENA":
                                    status = StatusVoznje.PRIHVACENA;
                                    break;
                                case "USPESNA":
                                    status = StatusVoznje.USPESNA;
                                    break;
                            }
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            komentar = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            komentarDatum = reader.Value;
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            switch (reader.Value)
                            {
                                case "neocenjen":
                                    ocena = Ocena.NEOCENJEN;
                                    break;
                                case "dobro":
                                    ocena = Ocena.DOBRO;
                                    break;
                                case "lose":
                                    ocena = Ocena.LOSE;
                                    break;
                                case "odlicno":
                                    ocena = Ocena.ODLICNO;
                                    break;
                                case "veomaDobro":
                                    ocena = Ocena.VRLODOBRO;
                                    break;
                                case "veomaLose":
                                    ocena = Ocena.VRLOLOSE;
                                    break;
                            }
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            iznos = reader.Value;

                            SacuvajVoznju(datum, pocetna, krajnja, tip, musterijaI, musterijaP, vozacI, vozacP, dispecerI, dispecerP, status, komentar, komentarDatum, ocena, iznos);
                        }
                    }
                }
            }
        }

        private void SacuvajVoznju(string datum, string pocetna, string krajnja, TipAutomobila tip, string musterijaI, string musterijaP, string vozacI, string vozacP, string dispecerI, string dispecerP, StatusVoznje status, string komentar, string komentarDatum, Ocena ocena, string iznos)
        {
            DateTime date = ToDate("29-Avg-18 14:04:19");
            string[] podela = pocetna.Split(',');
            string[] ulica = podela[0].Split('_');
            string[] grad = podela[1].Split('_');

            Adresa adresa = new Adresa(ulica[0], ulica[1], grad[0], grad[1]);
            Lokacija poc = new Lokacija("1", "1", adresa);

            podela = krajnja.Split(',');
            ulica = podela[0].Split('_');
            grad = podela[1].Split('_');

            Adresa adresaa = new Adresa(ulica[0], ulica[1], grad[0], grad[1]);
            Lokacija kraj = new Lokacija("1", "1", adresaa);

            Musterija musterija = new Musterija();
            Vozac vozac = new Vozac();
            Dispecer dispecer = new Dispecer();
            if (musterijaI != "nema" && musterijaP != "nema")
            {
                foreach (Musterija m in Korisnici.ListaMusterija)
                {
                    if (musterijaI == m.Ime && musterijaP == m.Prezime)
                    {
                        musterija = m;
                        break;
                    }
                }
            }
            else
            {
                musterija = new Musterija("nema", "nema", "nema", "nema", Pol.MUSKI, "000", "nema", "nema", Uloga.MUSTERIJA);
            }

            if (vozacI != "nema" && vozacP != "nema")
            {
                foreach (Vozac m in Korisnici.ListaVozaca)
                {
                    if (vozacI == m.Ime && vozacP == m.Prezime)
                    {
                        vozac = m;
                        break;
                    }
                }
            }
            else
            {
                vozac = new Vozac("nema", "nema", "nema", "nema", Pol.MUSKI, "0000", "nema", "nema", Uloga.VOZAC, ulica[0], ulica[1], grad[0], grad[1]);
            }

            if (dispecerI != "nema" && dispecerP != "nema")
            {
                foreach (Dispecer m in Korisnici.ListaDispecera)
                {
                    if (dispecerI == m.Ime && dispecerP == m.Prezime)
                    {
                        dispecer = m;
                        break;
                    }
                }
            }
            else
            {
                dispecer = new Dispecer("nema", "nema", "nema", "nema", Pol.MUSKI, "0000", "nema", "nema", Uloga.DISPECER);
            }
            Voznja voznja = new Voznja();
            DateTime kom = ToDate(komentarDatum);
            Komentar k = new Komentar();
            if (status == StatusVoznje.OTKAZANA)
            {
                k = new Komentar(komentar, kom, voznja, ocena, musterija);
            }
            else if (status == StatusVoznje.NEUSPESNA)
            {
                k = new Komentar(komentar, kom, voznja, ocena, musterija);
            }
            else if (status == StatusVoznje.USPESNA)
            {
                k = new Komentar(komentar, kom, voznja, ocena, musterija);
            }
            else
            {
                k = new Komentar("bez opisa", kom, voznja, Ocena.NEOCENJEN, new Korisnik("nema", "nema", "nema", "nema", Pol.MUSKI, "0000", "nema", "nema", Uloga.DISPECER));
            }

            voznja = new Voznja(date, poc, tip, musterija, kraj, dispecer, vozac, iznos, k, status);

            k.VoznjaKomentara = voznja;
            voznja.Komentar = k;

            if (musterijaI != "nema" && musterijaP != "nema")
            {
                foreach (Musterija m in Korisnici.ListaMusterija)
                {
                    if (m.KorisnickoIme == musterija.KorisnickoIme)
                    {
                        m.ListaVoznji.Add(voznja);
                    }
                }
            }


            if (vozacI != "nema" && vozacP != "nema")
            {
                foreach (Vozac m in Korisnici.ListaVozaca)
                {
                    if (m.KorisnickoIme == vozac.KorisnickoIme)
                    {
                        m.ListaVoznji.Add(voznja);
                    }
                }
            }


            if (dispecerI != "nema" && dispecerP != "nema")
            {
                foreach (Dispecer m in Korisnici.ListaDispecera)
                {
                    if (m.KorisnickoIme == dispecer.KorisnickoIme)
                    {
                        m.ListaVoznji.Add(voznja);
                    }
                }
            }

            Korisnici.ListaSvihVoznji.Add(voznja);


            foreach (Korisnik kor in Korisnici.ListaKorisnika)
            {
                if (kor.KorisnickoIme == musterija.KorisnickoIme)
                {
                    kor.ListaVoznji = musterija.ListaVoznji;
                }
                else if (kor.KorisnickoIme == vozac.KorisnickoIme)
                {
                    kor.ListaVoznji = vozac.ListaVoznji;
                }
                else if (kor.KorisnickoIme == dispecer.KorisnickoIme)
                {
                    kor.ListaVoznji = dispecer.ListaVoznji;
                }
            }
        }

        private DateTime ToDate(string datum)
        {
            DateTime ret;

            string[] dat = datum.Split(' ', '-', ':', '/');

            int day = int.Parse(dat[0]);
            int month = 0;
            switch (dat[1])
            {
                case "Jan":
                    month = 1;
                    break;
                case "Feb":
                    month = 2;
                    break;
                case "Mar":
                    month = 3;
                    break;
                case "Apr":
                    month = 4;
                    break;
                case "May":
                    month = 5;
                    break;
                case "Jun":
                    month = 6;
                    break;
                case "Jul":
                    month = 7;
                    break;
                case "Aug":
                    month = 8;
                    break;
                case "Sep":
                    month = 9;
                    break;
                case "Oct":
                    month = 10;
                    break;
                case "Nov":
                    month = 11;
                    break;
                case "Dec":
                    month = 12;
                    break;
            }

            int year = int.Parse(dat[2]);
            year = year + 2000;
            int hour = int.Parse(dat[3]);
            int minute = int.Parse(dat[4]);
            int second = int.Parse(dat[5]);

            ret = new DateTime(year, month, day, hour, minute, second);

            return ret;

        }

    }
    
}
