using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace footgolf
{   class footgolf
    {
        //nev,kategoria,csapat,pontok

        public string nev;
        public string kategoria;
        public string csapat;
        public List<int> pont = new List<int>();
        public int osszpontszam;

        public footgolf(string nev, string kategoria, string csapat, List<int> pont, int osszpontszam)
        {
            this.nev = nev;
            this.kategoria = kategoria;
            this.csapat = csapat;
            this.pont = pont;
            this.osszpontszam = osszpontszam;
        }
    }

    class Program
    {
        static List<footgolf> minden = new List<footgolf>();
        static void Beolvas()
        {
            StreamReader sr = null;
            string fajl = @"fob2016.txt";

            using (sr = new StreamReader(fajl, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string[] sorok = sr.ReadLine().Split(';');

                    List<int> pontok = new List<int>();

                    for (int i = 3; i < sorok.Length; i++)
                    {
                        pontok.Add(Convert.ToInt32(sorok[i]));
                    }
                    footgolf temp = new footgolf(sorok[0], sorok[1], sorok[2], pontok, 0);
                    minden.Add(temp);

                }
            }
        }

        static void negyesfeladat()
        {
            double db = 0;
            foreach (var item in minden)
            {
                if (item.kategoria == "Noi")
                {
                    db++;
                }
            }
            double osztok = minden.Count;
            double szamolas = db / minden.Count;
            Console.WriteLine("4. feladat: A női versenyzők aránya: {0}%", Math.Round(szamolas * 100, 2));
        }

        static List<int> rendezes = new List<int>();

        static void otoshatosfeladat()
        {

            int pontszam = 0;

            foreach (var item in minden)
            {
                rendezes = item.pont.OrderBy(x => x).ToList();


                if (rendezes[0] != 0)
                {
                    pontszam += 10;
                }
                if (rendezes[1] != 0)
                {
                    pontszam += 10;
                }

                for (int i = 2; i < rendezes.Count; i++)
                {
                    pontszam += rendezes[i];

                }
                item.osszpontszam = pontszam;
                pontszam = 0;
            }
            List<footgolf> 
            minden2 = minden.OrderByDescending(x => x.osszpontszam).ToList();
            Console.WriteLine("6. feladat: A bajnok női versenyző");
            Console.WriteLine("     Név: {0}",minden2[0].nev);
            Console.WriteLine("     Egyesület: {0}",minden2[0].csapat);
            Console.WriteLine("     Összpontszám: {0}",minden2[0].osszpontszam);
        }

        static void hetesfeladat()
        {
            StreamWriter sw = new StreamWriter("osszpontFF.txt");
            foreach (var item in minden)
            {
                if (item.kategoria == "Felnott ferfi")
                {
                    sw.WriteLine("{0};{1};",item.nev, item.osszpontszam);
                }
            }
            sw.Close();
        }

        static void nyolcasfeladat()
        {
            var egybemindencsapatnev = minden.GroupBy(x => x.csapat).Select(y => new {csapat= y.Key, db=y.Count()}).ToList();

            Console.WriteLine("8. feladat: Egyesület statisztika");
            foreach (var item in egybemindencsapatnev)
            {
                Console.WriteLine("     {0} - {1} fő",item.csapat,item.db);
            }
        }
        

        static void Main(string[] args)
        {
            Beolvas();
            Console.WriteLine("3. feladat: Versenyzők száma: {0}",minden.Count);
            negyesfeladat();
            otoshatosfeladat();
            hetesfeladat();
            nyolcasfeladat();
            Console.ReadLine();
        }
    }
}
