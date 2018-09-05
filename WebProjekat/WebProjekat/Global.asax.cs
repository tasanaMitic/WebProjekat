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
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            string line = "";
            string[] polja;
            Pol pol;

            if (File.Exists(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\dispeceri.txt"))
            {                
                StreamReader file = new StreamReader(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\dispeceri.txt");
                while ((line = file.ReadLine()) != null)
                {
                    polja = line.Split(';');
                    if (polja[4].Equals("MUSKI"))
                    {
                        pol = Pol.MUSKI;
                    }
                    else
                    {
                        pol = Pol.ZENSKI;
                    }
                    Dispecer dispecer = new Dispecer(polja[0], polja[1], polja[2], polja[3], pol, polja[5], polja[6], polja[7] );
                    Korisnici.ListaKorisnika.Add(dispecer);
                    Korisnici.ListaDispecera.Add(dispecer);
                }

                file.Close();
                Ucitaj();
            }            
        }

        public void Ucitaj()
        {
            string line = "";
            string[] polja;
            string[] automobil;
            string[] lokacija;
            Pol pol;
            Uloga uloga;

            if(File.Exists(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\korisnici.txt"))
            {
                StreamReader file = new StreamReader(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\korisnici.txt");
                while ((line = file.ReadLine()) != "")
                {
                    polja = line.Split(';');

                    if(polja[4] == "MUSKI")
                    {
                        pol = Pol.MUSKI;
                    }
                    else
                    {
                        pol = Pol.ZENSKI;
                    }

                    if(polja[8] == "VOZAC")
                    {
                        uloga = Uloga.VOZAC;
                    }
                    else if(polja[8] == "MUSTERIJA")
                    {
                        uloga = Uloga.MUSTERIJA;
                    }
                    else
                    {
                        uloga = Uloga.DISPECER;
                    }

                    Korisnik korisnik = new Korisnik(polja[0], polja[1], polja[2], polja[3], pol, polja[5], polja[6], polja[7]);
                    korisnik.Uloga = uloga;

                    
                    if(!Korisnici.ListaKorisnika.Contains(korisnik))
                    {
                        Korisnici.ListaKorisnika.Add(korisnik);
                    }
                }
                file.Close();
            }

            if(File.Exists(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\vozaci.txt"))
            {
                StreamReader file = new StreamReader(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\vozaci.txt");
                while ((line = file.ReadLine()) != "")
                {
                    polja = line.Split(';');

                    if (polja[4] == "MUSKI")
                    {
                        pol = Pol.MUSKI;
                    }
                    else
                    {
                        pol = Pol.ZENSKI;
                    }

                    Korisnik korisnik = new Korisnik(polja[0], polja[1], polja[2], polja[3], pol, polja[5], polja[6], polja[7]);
                    korisnik.Uloga = Uloga.VOZAC;

                    automobil = polja[8].Split(':');
                    TipAutomobila tipAutomobila;
                    if(automobil[3] == "KOMBI")
                    {
                        tipAutomobila = TipAutomobila.KOMBI;
                    }
                    else
                    {
                        tipAutomobila = TipAutomobila.PUTNICKI;
                    }

                    Automobil auto = new Automobil(null, automobil[1], automobil[2], automobil[0], tipAutomobila);
                    lokacija = polja[9].Split(':');
                    Lokacija lok = new Lokacija("2", "2", new Adresa(lokacija[0], lokacija[1], lokacija[2], lokacija[3]));
                    Vozac vozac = new Vozac(korisnik, auto, lok);
                    if (polja[11] == "False")
                    {
                        vozac.Zauzet = false;
                    }
                    else
                    {
                        vozac.Zauzet = true;
                    }

                    auto.Vozac = vozac;
                    
                    

                    if(!Korisnici.ListaVozaca.Contains(vozac))
                    {
                        Korisnici.ListaVozaca.Add(vozac);
                    }
                }
                file.Close();
            }

            if (File.Exists(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\voznje.txt"))
            {
                StreamReader file = new StreamReader(@"C:\Users\Korisnik\Desktop\WebProjekat\WebProjekat\WebProjekat\voznje.txt");
                while ((line = file.ReadLine()) != "")
                {
                    polja = line.Split(';');
                    DateTime datumVoznje = ToDate(polja[0]);
                    DateTime datumKomentara = ToDate(polja[5]);
                    Vozac vozac = new Vozac();

                    Ocena ocena;

                    if(polja[4] == "NEOCENJEN")
                    {
                        ocena = Ocena.NEOCENJEN;
                    }
                    else if(polja[4] == "VRLOLOSE")
                    {
                        ocena = Ocena.VRLOLOSE;
                    }
                    else if (polja[4] == "LOSE")
                    {
                        ocena = Ocena.LOSE;
                    }
                    else if (polja[4] == "DOBRO")
                    {
                        ocena = Ocena.DOBRO;
                    }
                    else if (polja[4] == "VRLODOBRO")
                    {
                        ocena = Ocena.VRLODOBRO;
                    }
                    else 
                    {
                        ocena = Ocena.ODLICNO;
                    }


                    StatusVoznje statusVoznje;
                    if (polja[14] == "KREIRANA")
                    {
                        statusVoznje = StatusVoznje.KREIRANA;
                    }
                    else if(polja[14] == "FORMIRANA")
                    {
                        statusVoznje = StatusVoznje.FORMIRANA;
                    }
                    else if (polja[14] == "OBRADJENA")
                    {
                        statusVoznje = StatusVoznje.OBRADJENA;
                    }
                    else if (polja[14] == "PRIHVACENA")
                    {
                        statusVoznje = StatusVoznje.PRIHVACENA;
                    }
                    else if (polja[14] == "OTKAZANA")
                    {
                        statusVoznje = StatusVoznje.OTKAZANA;
                    }
                    else if (polja[14] == "NEUSPESNA")
                    {
                        statusVoznje = StatusVoznje.NEUSPESNA;
                    }
                    else if (polja[14] == "USPESNA")
                    {
                        statusVoznje = StatusVoznje.USPESNA;
                    }
                    else
                    {
                        statusVoznje = StatusVoznje.NEMA;
                    }

                    TipAutomobila tipAutomobila;
                    if (polja[15] == "KOMBI")
                    {
                        tipAutomobila = TipAutomobila.KOMBI;
                    }
                    else
                    {
                        tipAutomobila = TipAutomobila.PUTNICKI;
                    }

                    Lokacija lok = new Lokacija("2","2", new Adresa(polja[6], polja[7], polja[8], polja[9]));

                    Voznja voznja = new Voznja(datumVoznje, lok, tipAutomobila);
                    voznja.Odrediste = new Lokacija("2", "2", new Adresa(polja[10], polja[11], polja[12], polja[13]));
                    if(polja[1] != "/")
                    {
                        foreach(Dispecer d in Korisnici.ListaDispecera)
                        {
                            if(d.KorisnickoIme == polja[1])
                            {
                                voznja.Dispecer = d;
                            }
                        }
                    }
                    else
                    {
                        voznja.Dispecer = new Dispecer("/", "/", "/", "/", Pol.MUSKI, "/", "/", "/");
                    }

                    if(polja[16] != "/")
                    {
                        foreach(Vozac v in Korisnici.ListaVozaca)
                        {
                            if (v.KorisnickoIme == polja[16])
                            {
                                voznja.Vozac = v;
                                vozac = v;
                            }
                        }
                    }
                    else
                    {
                        Lokacija loka = new Lokacija("2", "2", new Adresa("/", "/", "/", "/"));
                        voznja.Vozac = new Vozac(new Korisnik("/", "/", "/", "/", Pol.MUSKI, "/", "/", "/"), new Automobil(null, "/", "/", "/", TipAutomobila.KOMBI), loka);
                    }

                    voznja.Iznos = Int32.Parse(polja[2]);
                    voznja.Komentar = new Komentar(polja[3], datumKomentara, null, ocena, vozac);
                    voznja.StatusVoznje = statusVoznje;
                    voznja.Komentar.VoznjaKomentara = voznja;


                    Korisnici.ListaSvihVoznji.Add(voznja);

                    if (voznja.Vozac.KorisnickoIme != "/")
                    {
                        if (Korisnici.ListaVozaca.Contains(vozac))
                        {
                            vozac.ListaVoznji.Add(voznja);
                        }
                    }

                    if(voznja.Dispecer.KorisnickoIme != "/")
                    {
                        if(Korisnici.ListaDispecera.Contains(voznja.Dispecer))
                        {
                            voznja.Dispecer.ListaVoznji.Add(voznja);
                        }
                    }

                }
                file.Close();
            }
        }

        private DateTime ToDate(string datum)
        {
            DateTime dateTime;

            string[] dat = datum.Split('.', '.', ' ', ':', ':');

            int day = Int32.Parse(dat[0]);
            int month = Int32.Parse(dat[1]);

            int year = int.Parse(dat[2]);
            int hour = int.Parse(dat[3]);
            int minute = int.Parse(dat[4]);
            int second = int.Parse(dat[5]);

            dateTime = new DateTime(year, month, day, hour, minute, second);

            return dateTime;

        }
    }    
}
