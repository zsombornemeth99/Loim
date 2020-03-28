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
        private List<string> kerdesek;

        internal Kerdes K { get => k; }

        public Mentes(Kerdes k)
        {
            this.k = k;
            if (!File.Exists("mentes.txt"))
            {
                StreamWriter sw = new StreamWriter("mentes.txt", false, Encoding.UTF8);
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
            StreamReader sr = new StreamReader("kerdes.txt", Encoding.UTF8);
            // melyik az a kérdés, ami azonos a többi kérdések közül az egyikkel
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                string[] adatok = sor.Split(';');

                kerdesek.Add(adatok[1]);
            }

            sr.Close();
        }
    }
}
