using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poligon
{
    public class Vektor
    {
        public Tacka pocetak, kraj;
        public Vektor() { }
        public Vektor(Tacka pocetak, Tacka kraj)
        {
            this.pocetak = pocetak;
            this.kraj = kraj;
        }
        public Tacka centriraj()
        {
            return new Tacka(kraj.x - pocetak.x, kraj.y - pocetak.y);
        }
        public double duzina()
        {
            return centriraj().R();
        }
        public static double skalarni(Vektor a, Vektor b)
        {
            Tacka prva = a.centriraj();
            Tacka druga = b.centriraj();
            double SP = prva.x * druga.x + prva.y * druga.y;
            return SP;
        }
        public static double VP(Vektor a, Vektor b)
        {
            Tacka A = a.centriraj();
            Tacka B = b.centriraj();
            double k = A.x * B.y - A.y * B.x;
            return k;
        }
        static public int SIS(Vektor a, Tacka C, Tacka D)
        {
            Tacka A = a.pocetak;
            Tacka B = a.kraj;
            Vektor AB = new Vektor(A, B);
            Vektor AC = new Vektor(A, C);
            Vektor AD = new Vektor(A, D);
            double k1 = VP(AB, AC);
            double k2 = VP(AB, AD);
            if (k1 * k2 < 0) return 1;
            if (k1 * k2 > 0) return 0;
            return -1;
        }
        public bool presek(Vektor b)
        {
            if (ugao(this, b) == 0) return false;

            if (SIS(this, b.pocetak, b.kraj) == 1 && SIS(b, this.pocetak, this.kraj) == 1) return true;
            return false;
        }

        static public double ugao(Vektor a, Vektor b)
        {       
            Tacka A = a.centriraj();
            Tacka B = b.centriraj();
            double ugao = B.ugao() - A.ugao();
            return ugao;
        }
    }
}
