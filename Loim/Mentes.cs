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
            if (File.Exists("mentes.txt"))
            {
                string kerdes = "";
                StreamReader sr = new StreamReader("mentes.txt", Encoding.UTF8);
                while (!sr.EndOfStream)
                {
                    kerdes = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader("kerdes.txt", Encoding.UTF8);
                // melyik az a kérdés, ami azonos a többi kérdések közül az egyikkel
                while (!sr.EndOfStream)
                {
                    string[] adatok = sr.ReadLine().Split(';');
                    if (kerdes == adatok[1])
                    {
                        int nehezseg = int.Parse(adatok[0]);
                        Kerdes k = new Kerdes(adatok[1], adatok[2], adatok[3], adatok[4], adatok[5],
                            char.Parse(adatok[6]), adatok[7]);
                        this.k = k;
                    }
                }
                sr.Close();
            }
            else
            {
                Console.WriteLine("\tNincs betöltött mentés.");
                Console.ReadLine();
            }
        }
    }
}
