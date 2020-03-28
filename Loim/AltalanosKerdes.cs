using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loim
{
    class AltalanosKerdes
    {
        private string kerdes;
        private string valaszA;
        private string valaszB;
        private string valaszC;
        private string valaszD;
        private string kategoria;

        public AltalanosKerdes(string kerdes, string valaszA, string valaszB, string valaszC, string valaszD, string kategoria)
        {
            this.Kerdes = kerdes;
            this.ValaszA = valaszA;
            this.ValaszB = valaszB;
            this.ValaszC = valaszC;
            this.ValaszD = valaszD;
            this.Kategoria = kategoria;
        }

        public string Kerdes { get => kerdes; set => kerdes = value; }
        public string ValaszA { get => valaszA; set => valaszA = value; }
        public string ValaszB { get => valaszB; set => valaszB = value; }
        public string ValaszC { get => valaszC; set => valaszC = value; }
        public string ValaszD { get => valaszD; set => valaszD = value; }
        public string Kategoria { get => kategoria; set => kategoria = value; }


    }
}
