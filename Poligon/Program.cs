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

        static bool konveksan()
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
                return true;
            }
            else
            {
                Console.WriteLine("Nije konveksan");
                return false;
            }

        }


        static int tackaLevo()
        {
            double x = t[0].x;
            double y = t[0].y;
            int indeks = 0; 
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].x < x)
                {
                    x = t[i].x;
                    y = t[i].y;
                    indeks = i;
                    continue;
                }
                if (t[i].x == x)
                {
                    if (t[i].y > y)
                    {
                        x = t[i].x;
                        y = t[i].y;
                        indeks = i;
                    }
                }
            }

            return indeks;
        }


        static void tackaUnutra()
        {

            Tacka Tpomoc = new Tacka();

            Console.WriteLine("Unesite koordinatu X");
            Tpomoc.x = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Unesite koordinatu X");
            Tpomoc.y = Convert.ToInt16(Console.ReadLine());

            Tacka Tpomoc2 = new Tacka();
            Tpomoc2.x = t[tackaLevo()].x-1;
            Tpomoc2.x = Tpomoc.y;



            Vektor zoran = new Vektor(Tpomoc, Tpomoc2);
            int p = 0;
            for (int i = 0; i < v.Count; i++)
            {
                if (zoran.presek(v[i]))
                {
                    p++;
                }
            }

            if (p % 2 == 1)
            {
                Console.WriteLine("unutra je");
            }
            else
            {
                Console.WriteLine("napolju je");
            }
            return;
        }


        static double pov(List<Tacka> t)
        {
            double b1 = 0;
            double b2 = 0;
            for (int i = 0; i < t.Count - 1; i++)
            {
                b1 += t[i].x * t[i + 1].y;
            }
            b1 += t[t.Count - 1].x * t[0].y;

            for (int i = 0; i < t.Count - 1; i++)
            {
                b2 += t[i].y * t[i + 1].x;
            }
            b2 += t[t.Count - 1].y * t[0].x;

            return Math.Abs((b1 - b2)/2);
        }


        static void konveksanOmotac()
        {
            List<Tacka> tVece = new List<Tacka>();
            List<Tacka> tManje = new List<Tacka>();
            


            
            for (int i = 0; i < v.Count; i++)
            {
                if (Vektor.VP(v[i], v[(i + 1) % v.Count]) > 0)
                {
                    tVece.Add(t[(i + 1) % t.Count]);
                }
                else if (Vektor.VP(v[i], v[(i + 1) % v.Count]) < 0)
                {
                    tManje.Add(t[(i + 1) % t.Count]);
                }
            }

            

            if (pov(tVece) > pov(tManje))
            {
                for (int i = 0; i < tVece.Count; i++)
                {
                    Console.WriteLine("X = " + tVece[i].x + "   Y = " + tVece[i].y);
                }
            }


            if (pov(tManje) > pov(tVece))
            {
                for (int i = 0; i < tManje.Count; i++)
                {
                    Console.WriteLine("X = " + tManje[i].x + "   Y = " + tManje[i].y);
                }
            }




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
            tackaUnutra();
            konveksanOmotac();
            


            Console.WriteLine("test");
            Console.ReadKey();
        }
    }
}
