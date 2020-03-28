﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loim
{
    class Jatekos
    {
        private string nev;
        private bool telefonosSegitseg;
        private bool felezoSegitseg;
        private bool kozonsegSegitseg;
        private DateTime jatekKezdete;
        private List<int> elertSzintek;
        private List<long> elertIdok;

        public Jatekos(string nev)
        {
            this.nev = nev;

            this.telefonosSegitseg = false;
            this.felezoSegitseg = false;
            this.kozonsegSegitseg = false;
            this.jatekKezdete = DateTime.Now;
            elertSzintek = new List<int>();
            elertIdok = new List<long>();
        }

        public string Nev { get => nev; set => nev = value; }
        public bool TelefonosSegitseg { get => telefonosSegitseg; set => telefonosSegitseg = value; }
        public bool FelezoSegitseg { get => felezoSegitseg; set => felezoSegitseg = value; }
        public bool KozonsegSegitseg { get => kozonsegSegitseg; set => kozonsegSegitseg = value; }
        public DateTime JatekKezdete { get => jatekKezdete; set => jatekKezdete = value; }

        public void telefonosSegitsegetHasznal()
        {
            this.telefonosSegitseg = true;
        }

        public void felezoSegitsegetHasznal()
        {
            this.felezoSegitseg = true;
        }

        public void kozonsegSegitsegetHasznal()
        {
            this.kozonsegSegitseg = true;
        }

        public long getJatekIdo(DateTime jatekVege)
        {
            return (long)jatekVege.Subtract(this.jatekKezdete).TotalSeconds;
        }

        

    }
}
