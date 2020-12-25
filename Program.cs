/*
 * -----------------------------------------------
 * 
 * Autor: Roko Kovač
 * Projekt: Evidencija automobila u auto-kući
 * Predmet: Osnove programiranja
 * Ustanova: VŠMTI
 * Godina: 2020.
 * 
 * -----------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using ConsoleTables;
using System.Globalization;


namespace Evidencija_automobila_u_auto_kući
{
    class Program
    {
        public static DateTime date = DateTime.Now;
        public static List<Ispis> automobili1 = new List<Ispis>();
        public static List<Ispis> automobili2 = new List<Ispis>();
        public static List<Ispis> neregistrirani = new List<Ispis>();
        public static List<Ispis> registrirani = new List<Ispis>();
        public static bool obrisan = false;
        static public bool prijavljen1 = false;
        static public bool prijavljen2 = false;

        public struct Ispis
        {
            /*
             * ----------------------------------------------
             * 
             * Struktura koja učitava podatke u listu  
             * automobila iz xml datoteke.
             * 
             * ----------------------------------------------
            */
            public int redniBroj;
            public string id;
            public string marka;
            public string model;
            public string motor;
            public string obujam;
            public int snaga;
            public int km;
            public string godina;
            public string registracija;

            public Ispis(int rbr,string i, string ma, string mod, string mot, string ob, int s, int k, string g, string r)
            {
                redniBroj = rbr;
                id = i;
                marka = ma;
                model = mod;
                motor = mot;
                obujam = ob;
                snaga = s;
                km = k;
                godina = g;
                registracija = r;
            }
        }
        public struct Prijava
        {
            /*
             * ----------------------------------------------
             * 
             * Struktura koja prepoznaje korisničko ime i 
             * povezuje ga sa samo jednom valjanom lozinkom.
             * 
             * ----------------------------------------------
            */

            public string ime;
            public string lozinka;
            public Prijava(string i, string l)
            {
                ime = i;
                lozinka = l;
            }
        }

        public static float Provjera2()
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja provjerava je li unos broj, a ako nije
            * vraća ponovni unos te su dopušteni decimalni brojevi.
            * 
            * -----------------------------------------------------
            */

            float k;
            while (!float.TryParse(Console.ReadLine(), out k))
            {
                Console.Write("Niste unijeli broj, unesite ponovno: ");
            }
            return k;
        }

        public static int Provjera1()
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja provjerava je li unos broj, a ako nije
            * vraća ponovni unos.
            * 
            * -----------------------------------------------------
            */

            int x;
            while (!int.TryParse(Console.ReadLine(), out x))
            {
                Console.Write("Niste unijeli broj, unesite ponovno: ");
            }
            return x;
        } 

        public static void IspisSortiraneListe()
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja ispisuje listu koja će biti sortirana
            * prema odabiru korisnika.
            * 
            * -----------------------------------------------------
            */

            var table = new ConsoleTable("Rbr", "Marka", "Model", "Motor", "Obujam", "Snaga", "KM", "Godina", "Registracija");
            int rbr = 1;
            foreach (Ispis elementi in automobili1)
             {
                table.AddRow(rbr++ + ".", elementi.marka, elementi.model, elementi.motor, elementi.obujam, elementi.snaga, elementi.km, elementi.godina, elementi.registracija);
             }
            table.Write();
        }
        public static void Prikaz1(bool prviPrikaz)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja prikazuje izbornik samo za admina 
            * koji ima dodatne mogućnosti.
            * 
            * -----------------------------------------------------
            */

            if (prviPrikaz == false)
            {
                string x="";
                while(x!="da" && x != "ne") { 
                Console.WriteLine("Želite li nastaviti s programom? (Da/Ne)");
                x = Console.ReadLine();
                if (x.ToLower() == "da") {
                    Console.Clear();
                    Console.WriteLine("Odaberi opciju izbornika! \n 1-Prikaži sve automobile\n 2-Prikaži sve automobile prema odabranom kriteriju" +
                    "\n 3-Dodaj automobil\n 4-Obriši automobil\n 5-Registracija\n 6-Odjava");
                }
                else if(x.ToLower()=="ne")
                {
                    Odjava();
                    Logiraj("odjava programa");
                }
                else
                {
                    Console.WriteLine("Niste unijeli valjanju opciju!");
                }
              }
            }
            else
            {
                Console.WriteLine("Odaberi opciju izbornika! \n 1-Prikaži sve automobile\n 2-Prikaži sve automobile prema odabranom kriteriju" +
                   "\n 3-Dodaj automobil\n 4-Obriši automobil\n 5-Registracija\n 6-Odjava");
                prviPrikaz = false;
            }
            
        }

        public static void Prikaz2(bool prviPrikaz)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja prikazuje izbornik samo za korisnika 
            * kojemu su ograničene mogućnosti.
            * 
            * -----------------------------------------------------
            */

            if (prviPrikaz == false)
            {
                string x="";
                while(x!="da" && x != "ne") { 
                Console.WriteLine("Želite li nastaviti s programom? (Da/Ne)");
                x = Console.ReadLine();
                if (x.ToLower() == "da")
                {
                    Console.Clear();
                    Console.WriteLine("Odaberi opciju izbornika! \n 1-Prikaži sve automobile\n 2-Prikaži sve automobile prema odabranom kriteriju" +
                        "\n 5-Registracija\n 6-Odjava");
                }
                else if (x.ToLower() == "ne")
                    {
                        Odjava();
                        Logiraj("odjava programa");
                    }
                else
                {                   
                   Console.WriteLine("Niste unijeli valjanju opciju!");
                }
              }
            }
            else
            {
                Console.WriteLine("Odaberi opciju izbornika! \n 1-Prikaži sve automobile\n 2-Prikaži sve automobile prema odabranom kriteriju" +
                   "\n 5-Registracija\n 6-Odjava");
                prviPrikaz = false;
            }
        }

        public static void Registracija()
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija ispisuje sve neregistrirane aute te omogućava 
            * produljavanje iste.
            * 
            * -----------------------------------------------------
            */
            DateTime aDate = DateTime.Now;
            Console.WriteLine(aDate.ToString("dd/MM/yyyy"));
            UcitajXML();
            string path2 = @"automobili.xml";
            string sXml = "";
            StreamReader oSr = new StreamReader(path2);
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/automobil");
            var table = new ConsoleTable("Rbr","ID", "Marka", "Model", "Motor", "Obujam", "Snaga", "KM", "Godina", "Registracija");
            int rbr = 1;
            int rednibroj1 = 1;
            int rednibroj2 = 1;
            foreach (Ispis elementi in automobili1)
            {
                DateTime date = DateTime.Now;
                DateTime date1 = DateTime.Parse(elementi.registracija);
                if (date1 < date)
                {
                    neregistrirani.Add(new Ispis(rednibroj1++,elementi.id, elementi.marka, elementi.model, elementi.motor, elementi.obujam, elementi.snaga, elementi.km, elementi.godina, elementi.registracija));
                    table.AddRow(rbr++ + ".", elementi.id, elementi.marka, elementi.model, elementi.motor, elementi.obujam, elementi.snaga, elementi.km, elementi.godina, elementi.registracija);

                }
                if (date > date1)
                {
                    registrirani.Add(new Ispis(rednibroj2++,elementi.id, elementi.marka, elementi.model, elementi.motor, elementi.obujam, elementi.snaga, elementi.km, elementi.godina, elementi.registracija));
                }

            }
            Console.WriteLine("\nPopis automobila kojima je registracija istekla!\n");
            table.Write();
            Console.WriteLine("Želite li registrirati neki automobil? (Da/Ne)");
            string x=Console.ReadLine();
            if (x.ToLower() == "da")
            {
                Console.WriteLine("Ako želite registrirati automobil odaberite ID automobila!");
                Console.Write("ID automobila: ");
                int y= Provjera1();
                Console.WriteLine("\n");
                foreach(Ispis element in neregistrirani)
                {                    
                    if (y == Convert.ToInt32(element.id))
                    {
                        DateTime date = DateTime.Now;
                        DateTime noviDatum = date.AddYears(1);
                        Console.WriteLine(noviDatum);
                        Console.WriteLine("Vaš automobil pod rednim brojem: {0} uspješno je registriran, registracija vrijedi do {1}!\n", y,noviDatum);                        
                        registrirani.Add(new Ispis(rednibroj1++,element.id, element.marka, element.model, element.motor, element.obujam, element.snaga, element.km, element.godina, Convert.ToString(noviDatum)));
                        foreach (XmlNode oNode in oNodes)
                        {
                            if (oNode.Attributes["id"].Value == y.ToString())
                            {
                                oNode.Attributes["Registracija"].Value = noviDatum.ToString("dd/MM/yyyy");
                            }
                        }
                            Logiraj("registracija automobila");                       
                    }                   
                }           
            }
            else if (x.ToLower() == "ne")
            {
                
            }
            else
            {
                Console.WriteLine("Niste unijeli valjanju opciju!");
            }
            oXml.Save("automobili.xml");
        }

        public static void UcitajXML()
        {
            /*
            * -----------------------------------------------------
            * 
            * Ova funkcija učitava xml datoteku.
            * 
            * -----------------------------------------------------
            */

            automobili1.Clear();
            string path2 = @"automobili.xml";
            string sXml = "";
            StreamReader oSr = new StreamReader(path2);
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/automobil");
            foreach (XmlNode oNode in oNodes)
            {
                automobili1.Add(new Ispis(0,oNode.Attributes["id"].Value, oNode.Attributes["Marka"].Value, oNode.Attributes["Model"].Value, oNode.Attributes["Motor"].Value, oNode.Attributes["Obujam"].Value, Convert.ToInt32(oNode.Attributes["Snaga"].Value), Convert.ToInt32(oNode.Attributes["KM"].Value), oNode.Attributes["Godina"].Value, oNode.Attributes["Registracija"].Value));
            }
        }   

        public static void DodajAutomobil()
        {
           /*
           * -----------------------------------------------------
           * 
           * Ova funkcija dodaje automobile u xml datoteku.
           * 
           * -----------------------------------------------------
          */
            UcitajXML();
            Console.WriteLine("Za unos automobila unesi marku, model, motor, obujam, snagu, kilometre i godinu automobila!"); 
            Console.Write("Marka: ");
            string marka =Console.ReadLine();
            Console.Write("Model: ");
            string model = Console.ReadLine();
            Console.Write("Motor: ");
            string motor = Console.ReadLine();
            Console.Write("Obujam: ");
            float obujam = Provjera2();
            Console.Write("Snaga: ");
            int snaga = Provjera1();
            Console.Write("Kilometri: ");
            int km = Provjera1();
            Console.Write("Godina: ");
            int godina = Provjera1();
            var doc = XDocument.Load("automobili.xml");
            var count = doc.Descendants("automobil").Count();
            var newElement = new XElement("automobil",
                               new XAttribute("id", count + 1),
                               new XAttribute("Marka", marka),
                               new XAttribute("Model", model),
                               new XAttribute("Motor", motor),
                               new XAttribute("Obujam", obujam),
                               new XAttribute("Snaga", snaga),
                               new XAttribute("KM", km),
                               new XAttribute("Godina", godina));
            doc.Element("data").Add(newElement);
            doc.Save("automobili.xml");
            Console.WriteLine("Uspješno dodavanje automobila!");
            Logiraj("dodavanje automobila");
        }

        public static void Logiraj(string tekst)
        {
            /*
            * ----------------------------------------------
            * 
            * Funkcija koja svaki parametar koji se šalje  
            * ovoj funkciji zapisuje u log datoteku.
            * 
            * ----------------------------------------------
           */
            string path1 = @"logovi.txt";
            StreamWriter Log = new StreamWriter(path1, true);
            Log.Write(DateTime.Now + " - {0} " + "\n", tekst);
            Log.Flush();
            Log.Close();
        }

        public static void PrikaziSveAutomobile()
        {
            /*
            * -----------------------------------------------------
            * 
            * Ova funkcija ispisuje sve automobile koji su 
            * upisani u xml datoteku iz koje se pozivaju na ekran.
            * 
            * -----------------------------------------------------
           */
            
            var table = new ConsoleTable("Rbr", "Marka","Model","Motor","Obujam","Snaga","KM","Godina","Registracija");
            int rbr = 1;
            UcitajXML();
            if (obrisan==false) 
            { 
                foreach(Ispis elementi in automobili1)
                 {
                table.AddRow(rbr++ + ".", elementi.marka, elementi.model, elementi.motor,elementi.obujam, elementi.snaga, elementi.km, elementi.godina, elementi.registracija);
                 }
            }
            else
            {
                foreach (Ispis elementi in automobili2)
                {
                    table.AddRow(rbr++ + ".", elementi.marka, elementi.model, elementi.motor,elementi.obujam, elementi.snaga, elementi.km, elementi.godina, elementi.registracija);
                }
            }
            Logiraj("prikaz automobila");
            table.Write();       
        }

        public static void Odjava()
        {
            /*
            * ----------------------------------------------
            * Ova funkcija odjavljuje korisnika.
            * ----------------------------------------------
           */

            Logiraj("odjava programa");
            Environment.Exit(0);                
        }

        public static void Izbornik1(int a)
        {
            /*
             * ----------------------------------------------
             * 
             * Funkcija koja poziva druge funkcije ovisno o 
             * korisničkom izboru.
             * 
             * ----------------------------------------------
            */

            Console.Clear();
            switch (a)
            {
                case 1:
                    PrikaziSveAutomobile();
                    break;
                case 2:
                    Kriterij();
                    break;
                case 3:
                    DodajAutomobil();
                    break;
                case 4:
                    ObrisiAutomobil();
                    break;
                case 5:
                    Registracija();
                    break;
                case 6:
                    Odjava();
                    break;
                default:
                    Console.WriteLine("Odabrali ste opciju koja ne postoji!");
                    break;
            }
        }

        public static void Izbornik2(int b)
        {
            /*
             * ----------------------------------------------
             * 
             * Funkcija koja poziva druge funkcije ovisno o 
             * korisničkom izboru.
             * 
             * ----------------------------------------------
            */

            Console.Clear();
            switch (b)
            {
                case 1:                   
                    PrikaziSveAutomobile();
                    break;
                case 2:
                    Kriterij();
                    break;
                case 5:
                    Registracija();
                    break;
                case 6:
                    Odjava();
                    break;
                default:
                    Console.WriteLine("Odabrali ste opciju koja ne postoji!");
                    break;
            }
        }

        public static bool Login(string ime, string lozinka)
        {
            /*
            * ----------------------------------------------
            * 
            * Funkcija prijavljuje korisnika nakon sto upiše 
            * točno korisničko ime i lozinku.
            * 
            * ----------------------------------------------
           */

            bool tocnoKorisnickoIme = false;
            bool tocnaLozinka = false;
            string path = @"login.xml";
            List<Prijava> loginPodatci = new List<Prijava>();
            string sadrzaj_xml = "";
            StreamReader oSr = new StreamReader(path);
            using (oSr)
            {
                sadrzaj_xml = oSr.ReadToEnd();
            }
            XmlDocument xml_datoteka = new XmlDocument();
            xml_datoteka.LoadXml(sadrzaj_xml);
            XmlNodeList atributi = xml_datoteka.SelectNodes("//data/login");
            foreach (XmlNode korisnik in atributi)
            {
                loginPodatci.Add(new Prijava(korisnik.Attributes["ime"].Value, korisnik.Attributes["lozinka"].Value));
            }
            oSr.Close();
            foreach (Prijava element in loginPodatci)
            {
                if (element.ime == ime)
                {
                    tocnoKorisnickoIme = true;
                }
                if (element.lozinka == lozinka)
                {
                    tocnaLozinka = true;
                }
            }
            if (tocnaLozinka == true && tocnoKorisnickoIme == true)
            {
                Logiraj("prijava " + ime);
                return true;
            }
            else
            {
                return false;
            }

        }

        public static void ObrisiAutomobil()
        {
            /*
            * ------------------------------------
            * 
            * Ova funkcija briše automobile prema 
            * njihovom ID-ju
            * 
            * ------------------------------------
           */
            int signal = 0;
            string path = @"automobili.xml";
            string sXml = "";
            StreamReader oSr = new StreamReader("automobili.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/automobil");
            Console.Write("Unesite ID automobila kojeg želite obrisati: ");
            int d=Provjera1();
            foreach (XmlNode oNode in oNodes)
            {
                int id = Convert.ToInt32(oNode.Attributes["id"].Value);
                if (id == d)
                {
                    oXml.DocumentElement.RemoveChild(oNode);
                   
                    if (id == d)
                    {
                        Console.WriteLine("Vaš ID {0} uspješno je obrisan!", oNode.Attributes["id"].Value);
                        signal = 1;
                        Logiraj("brisanje ID-a");
                    }
                }
            }
            automobili2.Clear();
            obrisan = false;
            foreach (Ispis elementi in automobili1.ToList())
            {
                automobili2.Add(new Ispis(0,elementi.id, elementi.marka, elementi.model, elementi.motor,elementi.obujam, elementi.snaga, elementi.km, elementi.godina, elementi.registracija));
                if (d == Convert.ToInt32(elementi.id))
                {
                    automobili2.Remove(elementi);
                    obrisan = true;
                }
            }
            if (signal == 0)
            {
                Console.WriteLine("ID je nepostojeći!");
            }
            oXml.Save(path);
        }
        public static void Kriterij()
        {
            /*
            * ----------------------------------------------
            * 
            * Funkcija koja ispisuje kriterije sortiranja 
            * te sortira elemente u tablici prema odabiru.
            * 
            * ----------------------------------------------
           */

            Logiraj("prikaz kriterija");
            Console.WriteLine("Odaberi kriterij prema kojem želiš prikazati automobile!");
            Console.WriteLine(" 1.Marka\n 2.Motor\n 3.Obujam\n 4.Snaga\n 5.Kilometraža\n 6.Godina ");
            int c=Provjera1();
            switch (c)
            {
                case 1:
                    {
                        Console.Clear();
                        UcitajXML();
                        automobili1 = automobili1.OrderBy(o => o.marka).ToList();
                        IspisSortiraneListe();
                        prijavljen1 = true;
                        prijavljen2 = true;
                        break;
                    }   
                   
                case 2:
                    {
                        Console.Clear();
                        UcitajXML();
                        automobili1 = automobili1.OrderBy(o => o.motor).ToList();
                        IspisSortiraneListe();
                        prijavljen1 = true;
                        prijavljen2 = true;
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        UcitajXML();
                        automobili1 = automobili1.OrderBy(o => o.obujam).ToList();
                        IspisSortiraneListe();
                        prijavljen1 = true;
                        prijavljen2 = true;
                        break;
                    }

                case 4:
                    {
                        Console.Clear();
                        UcitajXML();
                        automobili1 = automobili1.OrderBy(o => o.snaga).ToList();
                        IspisSortiraneListe();
                        prijavljen1 = true;
                        prijavljen2 = true;
                        break;
                    }
                case 5:
                    {
                        Console.Clear();
                        UcitajXML();
                        automobili1 = automobili1.OrderBy(o => o.km).ToList();
                        IspisSortiraneListe();
                        prijavljen1 = true;
                        prijavljen2 = true;
                        break;
                    }
                case 6:
                    {
                        Console.Clear();
                        UcitajXML();
                        automobili1 = automobili1.OrderBy(o => o.godina).ToList();
                        IspisSortiraneListe();
                        prijavljen1 = true;
                        prijavljen2 = true;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Nepostojeći kriterij!");
                        break;
                    }                    
            }
        }

        static void Main(string[] args)
        {
            int b=0;
            string x = "Student";
            string y = "Admin";
            Logiraj("pokretanje programa");
            bool prviPrikaz = true;
            while (prijavljen1 == false && prijavljen2 == false)
            {
                Console.WriteLine("Prijavi se!\nUpiši korisničko ime i lozinku!");
                Console.Write("Unesi korisničko ime: ");
                string ime = Console.ReadLine();
                Console.Write("Unesi lozinku: ");
                string lozinka = Console.ReadLine();

                if (Login(ime, lozinka) == true)
                {
                    Console.Clear();
                    if (ime == y)
                    {
                        Prikaz1(prviPrikaz);
                        int a = Provjera1();
                        Izbornik1(a);
                        prijavljen1 = true;
                    }
                    else if (ime == x)
                    {
                        Prikaz2(prviPrikaz);
                        int a = Provjera1();
                        Izbornik2(a);
                        prijavljen2 = true;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Krivo ime ili lozinka!");
                }
            }
            do
            {
                if (prijavljen1 == true)
                {
                    prviPrikaz = false;
                    Prikaz1(prviPrikaz);
                    b = Provjera1();
                    Izbornik1(b);
                    prijavljen1 = true;              
                }
                else if (prijavljen2 == true)
                {
                    prviPrikaz = false;
                    Prikaz2(prviPrikaz);
                    b = Provjera1();
                    Izbornik2(b);
                    prijavljen2 = true;          
                }
            } while (b != 6);
        }
    }
}

