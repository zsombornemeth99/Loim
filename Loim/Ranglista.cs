using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loim
{
    class Ranglista
    {
        private String nev;
        private int eredmeny;
        private long jatszottMasodperc;

        public Ranglista(String sor)
        {
            String[] adatok = sor.Split(';');
            this.nev = adatok[0];
            this.eredmeny = int.Parse(adatok[1]);
            this.jatszottMasodperc = long.Parse(adatok[2]);
        }

        public Ranglista(string nev, int eredmeny, long jatszottMasodperc)
        {
            this.nev = nev;
            this.eredmeny = eredmeny;
            this.jatszottMasodperc = jatszottMasodperc;
        }

        public string Nev { get => nev; set => nev = value; }
        public long JatszottMasodperc { get => jatszottMasodperc; set => jatszottMasodperc = value; }
        public int Eredmeny { get => eredmeny; set => eredmeny = value; }
    }
}
