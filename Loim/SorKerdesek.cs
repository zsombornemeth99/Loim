using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Loim
{
    class SorKerdesek
    {
        private List<SorKerdes> sorKerdesLista;

        public SorKerdesek(string fajl)
        {
            this.sorKerdesLista = new List<SorKerdes>();

            StreamReader r = new StreamReader(fajl, Encoding.UTF8);

            while (!r.EndOfStream)
            {
                string sor = r.ReadLine();
                string[] adatok = sor.Split(';');

                SorKerdes sk = new SorKerdes(adatok[0], adatok[1], adatok[2], adatok[3], adatok[4],
                    adatok[5], adatok[6]);

                this.sorKerdesLista.Add(sk);
            }

            r.Close();
        }


        public int getKerdesekSzama()
        {
            return this.sorKerdesLista.Count;
        }

        public SorKerdes getVeletlenSorKerdes()
        {
            int veletlenIndex = Program.rnd.Next(0, this.sorKerdesLista.Count);
            return this.sorKerdesLista[veletlenIndex];
        }
    }
}
