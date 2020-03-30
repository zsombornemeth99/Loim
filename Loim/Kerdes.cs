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
        private string nehezsegiSzint;

        public Kerdes(string kerdes, string valaszA, string valaszB, string valaszC, string valaszD, char helyesValasz, string kategoria)
            : base(kerdes, valaszA, valaszB, valaszC, valaszD, kategoria)
        {
            this.HelyesValasz = helyesValasz;
        }

        public Kerdes(string nehezsegiSzint, string kerdes, string valaszA, string valaszB, string valaszC, string valaszD, char helyesValasz, string kategoria)
                    : base(kerdes, valaszA, valaszB, valaszC, valaszD, kategoria)
        {
            this.HelyesValasz = helyesValasz;
            this.nehezsegiSzint = nehezsegiSzint;
        }

        public char HelyesValasz { get => helyesValasz; set => helyesValasz = value; }
        public string NehezsegiSzint { get => nehezsegiSzint; }

        public bool helyesE(char tipp)
        {
            return char.ToUpper(tipp).Equals(this.helyesValasz);
        }

        public string getKerdes()
        {
            return this.Kerdes;
        }

        public string getKerdesKategoria()
        {
            return this.Kategoria;
        }

        public string getKerdesValaszok()
        {
            
            string s = "\tA - " + this.ValaszA + "\n";
            s += "\tB - " + this.ValaszB + "\n";
            s += "\tC - " + this.ValaszC + "\n";
            s += "\tD - " + this.ValaszD + "\n";
            if (new Beallitasok().Cheat)
                s += "\n\tHelyes válasz: " + this.helyesValasz + "\n";

            return s;
        }
    }
}
