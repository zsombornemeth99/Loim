using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Loim
{
    class Kerdesek
    {
        private List<Kerdes>[] kerdesLista;

        public Kerdesek(string fajl)
        {
            this.kerdesLista = new List<Kerdes>[15];

            for (int i = 0; i < 15; i++)
            {
                this.kerdesLista[i] = new List<Kerdes>();
            }

            StreamReader r = new StreamReader(fajl, Encoding.UTF8);

            while (!r.EndOfStream)
            {
                string sor = r.ReadLine();
                string[] adatok = sor.Split(';');

                int nehezseg = int.Parse(adatok[0]);

                Kerdes k = new Kerdes(adatok[1], adatok[2], adatok[3], adatok[4], adatok[5],
                    char.Parse(adatok[6]), adatok[7]);

                this.kerdesLista[nehezseg - 1].Add(k);
            }

            r.Close();
        }

        public int getKerdesekSzama(int nehezsegSzintje)
        {
            return this.kerdesLista[nehezsegSzintje - 1].Count;
        }

        public Kerdes getVeletlenKerdes(int nehezsegSzintje)
        {
            int veletlenIndex = Program.rnd.Next(0, this.kerdesLista[nehezsegSzintje - 1].Count);
            return this.kerdesLista[nehezsegSzintje - 1][veletlenIndex];
        }
    }
}
