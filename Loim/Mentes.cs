using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Loim
{
    class Mentes
    {
        private Kerdes k;
        private Jatekos j;
        private List<string> kerdesek;

        internal Kerdes K { get => k; }

        public Mentes(Jatekos j, Kerdes k)
        {
            this.k = k;
            if (!File.Exists("mentes.txt"))
            {
                StreamWriter sw = new StreamWriter("mentes.txt", false, Encoding.UTF8);
                sw.WriteLine(j);
                sw.WriteLine(k.Kerdes);
                sw.Close();
            }
            else
            {
                File.Delete("mentes.txt");
                StreamWriter sw = new StreamWriter("mentes.txt", false, Encoding.UTF8);
                sw.WriteLine(k.Kerdes);
                sw.Close();
            }
        }

        public Mentes()
        {
            string kerdes = "";
            StreamReader sr = new StreamReader("mentes.txt", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                string[] adatok = sr.ReadLine().Split(';');
                this.j = new Jatekos(adatok[0], bool.Parse(adatok[1]), bool.Parse(adatok[2]), bool.Parse(adatok[3]), long.Parse(adatok[4]));
                kerdes = sr.ReadLine();
            }
            sr.Close();

            sr = new StreamReader("kerdes.txt", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                string[] adatok = sr.ReadLine().Split(';');
                if (kerdes == adatok[1])
                {
                    k = new Kerdes(adatok[1], adatok[2], adatok[3], adatok[4], adatok[5], char.Parse(adatok[6]), adatok[7]);
                }
            }
            sr.Close();
        }
    }
}
