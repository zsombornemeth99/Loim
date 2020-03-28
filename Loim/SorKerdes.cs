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

        public override string ToString()
        {
            string s = string.Format("[{0}] - {1}\n\n", this.Kategoria, this.Kerdes);

            s += "\tA - " + this.ValaszA + "\n";
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
