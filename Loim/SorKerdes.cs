using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loim
{
    class SorKerdes : AltalanosKerdes
    {
        private string helyesSorrend;

        public SorKerdes(string kerdes, string valaszA, string valaszB, string valaszC, string valaszD, string helyesSorrend, string kategoria)
            : base(kerdes, valaszA, valaszB, valaszC, valaszD, kategoria)
        {
            this.HelyesSorrend = helyesSorrend;
        }

        public string HelyesSorrend { get => helyesSorrend; set => helyesSorrend = value; }

        public bool helyesE(string tipp)
        {
            return tipp.ToUpper().Equals(this.helyesSorrend);
        }

        public string getSorKerdes()
        {
            return this.Kerdes;
        }

        public string getSorKerdesKategoria()
        {
            return this.Kategoria;
        }

        public string getHelyesSorrend()
        {
            return this.helyesSorrend;
        }

        public string getSorKerdesValaszok()
        {
            string s = "\tA - " + this.ValaszA + "\n";
            s += "\tB - " + this.ValaszB + "\n";
            s += "\tC - " + this.ValaszC + "\n";
            s += "\tD - " + this.ValaszD + "\n";
            if (new Beallitasok().Cheat)
            {
                s += "\n\tHelyes sorrend: " + this.helyesSorrend + "\n";
            }
            return s;
        }
    }
}
