using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Loim
{
    class Beallitasok
    {
        private bool cheat;

        public bool Cheat { get => cheat; }

        public Beallitasok()
        {
            // beallitasok.txt
            try
            {
                if (!File.Exists("beallitasok.txt"))
                {
                    StreamWriter sw = new StreamWriter("beallitasok.txt", false, Encoding.UTF8);
                    sw.WriteLine(false);
                    sw.Close();
                }
                else
                {
                    StreamReader sr = new StreamReader("beallitasok.txt", Encoding.UTF8);
                    while (!sr.EndOfStream)
                    {
                        this.cheat = bool.Parse(sr.ReadLine());
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("hiba"+e);
            }
        }

        public void setCheat(bool cheat)
        {
            File.Delete("beallitasok.txt");
            StreamWriter sw = new StreamWriter("beallitasok.txt", false, Encoding.UTF8);
            sw.WriteLine(cheat);
            sw.Close();
        }
    }
}
