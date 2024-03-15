using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Poligon
{
    internal class Program
    {

        /*public static void ucitaj()
        {
            Console.WriteLine("Koje je ime fajla");
            string imefajla = Console.ReadLine();
            StreamReader zika = new StreamReader(imefajla);

            n = Convert.ToInt32(zika.ReadLine());
            lista = new List<grana>[n];

            cvor = new string[n];

            for (int i = 0; i < n; i++)
            {
                cvor[i] = zika.ReadLine();
                lista[i] = new List<grana>();

            }

            for (int i = 0; i < n; i++)
            {
                string ppoc = cvor[i];
                string pkr = "";
                double tezina = 0;

                while (pkr != "-")
                {

                    pkr = zika.ReadLine();
                    if (pkr == "-") break;

                    tezina = Convert.ToDouble(zika.ReadLine());

                    grana p = new grana(ppoc, pkr, tezina);

                    lista[i].Add(p);

                }
            }
        }


        public static void sacuvaj()
        {
            Console.WriteLine("Koje je ime fajla");
            string imefajla = Console.ReadLine();
            StreamWriter pera = new StreamWriter(imefajla);

            pera.WriteLine(n);

            for (int i = 0; i < n; i++)
            {
                pera.WriteLine(cvor[i]);
            }

            for (int i = 0; i < n; i++)
            {

                foreach (grana g in lista[i])
                {
                    pera.WriteLine(g.toStringZaCuvanje());
                }
                pera.WriteLine("-");
            }
            pera.Close();
        }

        public static void unos()
        {
            Console.WriteLine("Koliko ima cvorova?");
            n = Convert.ToInt32(Console.ReadLine());
            lista = new List<grana>[n];

            cvor = new string[n];

            for (int i = 0; i < n; i++)
            {
                cvor[i] = Console.ReadLine();
                lista[i] = new List<grana>();

            }



            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Unesi sa kojim cvorovima je povezan ovaj cvor {cvor[i]}");


                string ppoc = cvor[i];
                string pkr = "";
                double tezina = 0;

                while (true)
                {

                    pkr = Console.ReadLine();
                    if (pkr == "-") break;

                    tezina = Convert.ToDouble(Console.ReadLine());

                    grana p = new grana(ppoc, pkr, tezina);

                    lista[i].Add(p);

                }
            }
        }*/

        static public int n;
        static public List<Tacka> t = new List<Tacka>();
        static public List<Vektor> v = new List<Vektor>();

        static void unos()
        {
            Console.WriteLine("Koliko ima tacaka?");
            n = Convert.ToInt16(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Tacka pera = new Tacka();
                Console.WriteLine("Unesite koordinatu X");
                pera.x = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("Unesite koordinatu Y");
                pera.y = Convert.ToInt16(Console.ReadLine());
                t.Add(pera);
            }

            for (int i = 0; i < t.Count-1; i++)
            {
                Vektor zika = new Vektor(t[i], t[i + 1]);
                v.Add(zika);
            }
            Vektor mika = new Vektor(t[t.Count - 1], t[0]);
            v.Add(mika);


        }

        static void save()
        {
            Console.WriteLine("Unesite ime fajla");
            string s = Console.ReadLine();
            s = s + ".txt";
            StreamWriter fajl = new StreamWriter(s);

            fajl.WriteLine(n);
            for (int i = 0; i < t.Count; i++)
            {
                fajl.WriteLine(t[i].x);
                fajl.WriteLine(t[i].y);
            }

            fajl.Close();
        }

        static void load()
        {
            Console.WriteLine("Unesite ime fajla");
            string s = Console.ReadLine();
            s = s + ".txt";
            StreamReader fajl = new StreamReader(s);

            n = Convert.ToInt16(fajl.ReadLine());
            t.Clear();
            for (int i = 0; i < n; i++)
            {
                Tacka pera = new Tacka();

                pera.x = Convert.ToDouble(fajl.ReadLine());
                pera.y = Convert.ToDouble(fajl.ReadLine());

                t.Add(pera);
            }


            v.Clear();
            for (int i = 0; i < t.Count - 1; i++)
            {
                Vektor zika = new Vektor(t[i], t[i + 1]);
                v.Add(zika);
            }
            Vektor mika = new Vektor(t[t.Count - 1], t[0]);
            v.Add(mika);
        }

        static void prost()
        {
            for (int i = 0; i < v.Count; i++)
            {
                for (int j = i+1; j < v.Count; j++)
                {
                    if (v[i].kraj.x == v[j].pocetak.x && v[i].kraj.y == v[j].pocetak.y)
                    {
                        continue;
                    }
                    else
                    {
                        if (v[i].presek(v[j]) == true)
                        {
                            Console.WriteLine("nije prost");
                            return;
                        }
                    }
                    
                }
            }

            Console.WriteLine("prost je");

        }

        static void obim()
        {
            double obim = 0;
            for (int i = 0; i < v.Count; i++)
            {
                obim += v[i].duzina();
            }
            Console.WriteLine("obim je " + obim);
        }

        static void povrsina()
        {
            double b1 = 0;
            double b2 = 0;
            for (int i = 0; i < t.Count-1; i++)
            {
                b1 += t[i].x * t[i + 1].y;
            }
            b1 += t[t.Count - 1].x * t[0].y;

            for (int i = 0; i < t.Count - 1; i++)
            {
                b2 += t[i].y * t[i + 1].x;
            }
            b2 += t[t.Count - 1].y * t[0].x;

            Console.WriteLine("povrsina je " + (b1 - b2) / 2);
        }

        static void konveksan()
        {
            int p = 0;
            for (int i = 0; i < v.Count - 1; i++)
            {
                if (Vektor.VP(v[i], v[i + 1]) > 0)
                {
                    p++;
                }
                else if (Vektor.VP(v[i], v[i + 1]) < 0)
                {
                    p--;
                }
            }

            if (Vektor.VP(v[v.Count-1],v[0]) > 0)
            {
                p++;
            }
            else if (Vektor.VP(v[v.Count - 1], v[0]) < 0)
            {
                p--;
            }


            if (Math.Abs(p) == t.Count)
            {
                Console.WriteLine("Konveksan je");
            }
            else
            {
                Console.WriteLine("Nije konveksan");
            }


        }

        void tackaUnutra()
        {
            bool veceOd0 = false;
            bool pravilo = false;

            Tacka Tpomoc = new Tacka();

            Console.WriteLine("Unesite koordinatu X");
            Tpomoc.x = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Unesite koordinatu X");
            Tpomoc.y = Convert.ToInt16(Console.ReadLine());



            Vektor Vpomoc = new Vektor(v[0].pocetak, Tpomoc);
            if (Vektor.VP(v[0],Vpomoc) > 0)
            {
                veceOd0 = true;
            }
            else if (Vektor.VP(v[0], Vpomoc) > 0)
            {
                veceOd0 = false;
            }

            if (Vektor.VP(v[0], v[1]) > 0)
            {
                pravilo = true;
            }
            else if (Vektor.VP(v[0], Vpomoc) > 0)
            {
                pravilo = false;
            }


            
            for (int i = 1; i < v.Count; i++)
            {
                Vektor zoran = new Vektor(v[i].pocetak, Tpomoc);
                Vektor Prezorana = new Vektor(v[i-1].pocetak, Tpomoc);

                if (Vektor.VP(v[i],zoran) > 0 && Vektor.VP(v[i-1], Prezorana) > 0)
                {
                
                }

            }
            


        }

        void konveksanOmotac()
        {

        }

        static void Main(string[] args)
        {
            //unos();
            //save();
            load();
            prost();
            obim();
            povrsina();
            konveksan();

            Console.WriteLine("test");
            Console.ReadKey();
        }
    }
}
