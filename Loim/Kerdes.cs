using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loim
{
    class Kerdes : AltalanosKerdes
    {
        private char helyesValasz;

        public Kerdes(string kerdes, string valaszA, string valaszB, string valaszC, string valaszD, char helyesValasz, string kategoria)
            : base(kerdes, valaszA, valaszB, valaszC, valaszD, kategoria)
        {
            this.HelyesValasz = helyesValasz;
        }

        public char HelyesValasz { get => helyesValasz; set => helyesValasz = value; }

        public bool helyesE(char tipp)
        {
            return char.ToUpper(tipp).Equals(this.helyesValasz);
        }

        public override string ToString()
        {
            string s = string.Format("[{0}] - {1}\n\n", this.Kategoria, this.Kerdes);

            s += "\tA - " + this.ValaszA + "\n";
            s += "\tB - " + this.ValaszB + "\n";
            s += "\tC - " + this.ValaszC + "\n";
            s += "\tD - " + this.ValaszD + "\n";
            
            s += "\n\tHelyes válasz: " + this.helyesValasz + "\n";

            return s;
        }
    }
}
