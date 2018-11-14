using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Threading;
namespace Autobazar
{
    class Program
    {
        struct TBazar
        {
            public string vyrobce;
            public string model;
            public int rokVyroby;
            public string prevodovka;
        }
        static void Main(string[] args)
        {
            TBazar[] auta = new TBazar[1000];
            int pocet = 0, i;
            int cislo;
            do
            {
                Console.Clear();
                Console.WriteLine("Autobazar Speedy Cars, s. r. o.");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Pridat auto [1]");
                Console.WriteLine("Odstranit auto [2]");
                Console.WriteLine("Nacist z CSV [3]");
                Console.WriteLine("Ulozit do souboru CSV [4]");
                Console.WriteLine("Vypsat auta [5]");
                Console.WriteLine("Nacist z XML [6]");
                Console.WriteLine("Ulozit do XML [7]");
                Console.WriteLine("Ukoncit program [0]");
                Console.Write("Vase volba: ");
                cislo = Console.ReadKey().KeyChar;

                switch (cislo)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Autobazar Speedy Cars, s. r. o.");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Pridat auto");
                        auta[pocet] = Add();
                        pocet++;
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("Autobazar Speedy Cars, s. r. o.");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Odstranit auto");
                        Console.WriteLine("Zadejte index: ");
                        
                        int.TryParse(Console.ReadLine(), out i);
                        auta = Smazat(auta, pocet, i);
                        pocet--;
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("Autobazar Speedy Cars, s. r. o.");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Nacist auta z CSV");
                        pocet = NactiCSV(ref auta);
                        Console.WriteLine("Hotovo");
                        Console.ReadKey();
                        break;

                    case '4':
                        Console.Clear();
                        Console.WriteLine("Autobazar Speedy Cars, s. r. o.");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Ulozit do souboru");
                        UlozCSV(auta, pocet);
                        Console.WriteLine("Ulozeno");
                        Console.ReadKey();
                        break;
                    case '5':
                        Console.Clear();
                        Console.WriteLine("Autobazar Speedy Cars, s. r. o.");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Vypsat auta");
                        for (int y = 0; y < pocet; y++)
                        {
                            vypisAuto(auta, y);
                        }
                        Console.ReadKey();
                        break;
                    case '6':
                        Console.Clear();
                        Console.WriteLine("Autobazar Speedy Cars, s. r. o.");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Nacti databazi z XML");
                        pocet = NactiXML(ref auta);
                        Console.WriteLine("Nacteno");
                        Console.ReadKey();
                        break;
                    case '7':
                        Console.Clear();
                        Console.WriteLine("Autobazar Speedy Cars, s. r. o.");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Ulozit do XML");
                        UlozXML(pocet, auta);
                        Console.WriteLine("Hotovo");
                        Console.ReadKey();
                        break;

                    case '0':
                        break;


                    default:
                        Console.Clear();
                        Console.WriteLine("Spatna volba");
                        Console.ReadKey();
                        break;
                }

            }
            while (cislo != '0');
            Console.WriteLine();



        }

        static TBazar Add() //pridava auto
        {
            TBazar auto;
            Console.WriteLine("Zadejte vyrobce");
            auto.vyrobce = Console.ReadLine();
            Console.WriteLine("Zadejte model");
            auto.model = Console.ReadLine();
            auto.rokVyroby = NactiCislo("Zadejte rok vyroby: ", 3, 2000);
            Console.WriteLine("Zadej typ prevodovky. Cislo 1 = automat, jina hodnota = manual:");
            {
                int cislo;
                cislo = Console.ReadKey().KeyChar;
                switch (cislo)
                {
                    case '1':
                        auto.prevodovka = "automat";
                        Console.WriteLine(" Zadali jste automat");
                        Console.ReadLine();
                        break;
                    default:
                        auto.prevodovka = "manual";
                        Console.WriteLine(" Zadali jste manual");
                        Console.ReadLine();
                        break;

                }
            }
            return auto;
            Thread.Sleep(3000);
        }


        static int NactiCislo(string text = "Zadejte rok vyroby: ", int pocetPokusu = 3, int jinaHodnota = 0) //nacita pouze cislo
        {
            bool vseOK = true;
            int cislo;
            do
            {
                if (vseOK == false)
                {
                    Console.WriteLine("Zadejte rok vyroby, pocet zbylych pokusu {0}", --pocetPokusu);
                }
                Console.WriteLine(text);
                vseOK = int.TryParse(Console.ReadLine(), out cislo);
            } while (vseOK == false && pocetPokusu != 1);
            if (pocetPokusu == 1)
            {
                Console.WriteLine("Vsechny pokusy vycerpany, nastavena automaticky hodnota {0}.", jinaHodnota);
                cislo = jinaHodnota;
            }
            return cislo;
        }


        static void vypisAuto(TBazar[] auta, int i) //vypisuje auto
        {
            Console.WriteLine("{0}\t {1}\t {2}\t {3}", auta[i].vyrobce, auta[i].model, auta[i].rokVyroby, auta[i].prevodovka);
        }


        static TBazar[] Smazat(TBazar[] auta, int pocet, int i)
        {
            for (int y = i; y < pocet - 1; y++)
            {
                auta[y] = auta[y + 1];
            }
            return auta;
        }


        static int NactiCSV(ref TBazar[] auta) //nacita do konzole csv dokumenty
        {
            int pocet = 0;
            using (StreamReader sr = new StreamReader(@"soubor.txt"))
            {
                string radek;
                string[] pole = new string[4];
                while (!(sr.EndOfStream))
                {
                    radek = sr.ReadLine();
                    pole = radek.Split(';');
                    auta[pocet].vyrobce = pole[0];
                    auta[pocet].model = pole[1];
                    auta[pocet].rokVyroby = int.Parse(pole[2]);
                    auta[pocet].prevodovka = pole[3];
                    pocet++;
                }
                return pocet;
            }
        }
        

        static void UlozCSV(TBazar[] auta, int pocet) //uklada do csv dokumenty
        {
            using (StreamWriter sw = new StreamWriter(@"soubor.txt"))
            {
                for (int i = 0; i < pocet; i++)
                {
                    sw.WriteLine("{0};{1};{2};{3}", auta[i].vyrobce, auta[i].model, auta[i].rokVyroby, auta[i].prevodovka);
                }
                sw.Flush();
            }
        }


        static void UlozXML(int pocet, TBazar[] auta) // uklada databazi do XML dokumentu
        {
            try
            {
                string xmlCesta = (Directory.GetCurrentDirectory() + "\\soubor.xml");
                XDocument xDoc = new XDocument();

                XElement Auta = new XElement("auta");
                for (int i = 0; i < pocet; i++)
                {
                    // vytvari se element
                    XElement auto = new XElement("auto");
                    // vytvari se atribut
                    XAttribute vyrobce = new XAttribute("vyrobce", auta[i].vyrobce);
                    XAttribute model = new XAttribute("model", auta[i].model);
                    XAttribute rokVyroby = new XAttribute("rokVyroby", auta[i].rokVyroby);
                    XAttribute prevodovka = new XAttribute("prevodovka", auta[i].prevodovka);

                    auto.Add(vyrobce);
                    auto.Add(model);
                    auto.Add(rokVyroby);
                    auto.Add(prevodovka);

                    if (i == 0) xDoc.Add(Auta);
                    Auta.Add(auto);
                }
                xDoc.Save(xmlCesta);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        static int NactiXML(ref TBazar[] auta) // nacita databazi z dokumentu formatu XML
        {
            string xmlCesta = (Directory.GetCurrentDirectory() + "\\soubor.xml");
            int pocet = 0;

            XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(xmlCesta);
       
            XmlElement xRoot = xDoc.DocumentElement;

            foreach (XmlNode xnode in xRoot)
            {

                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attributeVyrobce = xnode.Attributes.GetNamedItem("vyrobce");
                    if (attributeVyrobce != null)
                        auta[pocet].vyrobce = attributeVyrobce.Value;

                    XmlNode attributeModel = xnode.Attributes.GetNamedItem("model");
                    if (attributeModel != null)
                        auta[pocet].model = attributeModel.Value;

                    XmlNode attributeRokVyroby = xnode.Attributes.GetNamedItem("rokVyroby");
                    if (attributeRokVyroby != null)
                        auta[pocet].rokVyroby = int.Parse(attributeRokVyroby.Value);

                    XmlNode attributePrevodovka = xnode.Attributes.GetNamedItem("prevodovka");
                    if (attributePrevodovka != null)
                        auta[pocet].prevodovka = attributePrevodovka.Value;
                }
                pocet++;
            }
            return pocet;
        }


        



    }
}