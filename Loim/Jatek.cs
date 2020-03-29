using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Loim
{
    class Jatek
    {
        private SorKerdesek sorKerdesek;
        private Kerdesek kerdesek;
        private Kerdes k;
        private Kerdes ks;
        private SorKerdes sk;
        private Jatekos jatekos;
        private long jatekIdo;
        private int szint;
        private string nev;
        private bool helyesE=true;
        private int menuPont;

        internal Jatekos Jatekos { get => jatekos; set => jatekos = value; }

        public Jatek()
        {
            this.sorKerdesek = new SorKerdesek("sorkerdes.txt");
            this.kerdesek = new Kerdesek("kerdes.txt");

            //általános tájékoztató

            int menuPont;
            do
            {
                menuPont = this.menu();

                switch (menuPont)
                {
                    case 1: this.jatekInditasa(); break;
                    case 2:
                        Kerdes k = new Mentes().K;
                        if (File.Exists("mentes.txt"))
                        {
                            jatekBetoltes(); break;
                        }
                        else
                        {
                            break;
                        }
                    case 3: ranglistaMegjelenites(); break;
                    case 4: osszegek();break;
                    case 5:
                        Beallitasok b = new Beallitasok();
                        if (b.Cheat)
                            b.setCheat(false);
                        else
                            b.setCheat(true);
                        break;
                    case 6: Console.WriteLine("Köszönjük, hogy részt vett a játékban"); ; break;
                }
            } 
            while (menuPont != 6);
        }

        private int menu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine();
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                string s = "LEGYEN ÖN IS MILLIOMOS!";
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.WriteLine(s);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\tMenü");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\t1 - Játék indítása");
                if(File.Exists("mentes.txt"))
                    Console.WriteLine("\t2 - Mentés betöltése");
                else
                    Console.WriteLine("\t2 - Nincs mentett játéka");
                Console.WriteLine("\t3 - Dicsőséglista");
                Console.WriteLine("\t4 - Nyereményfa");
                if (!new Beallitasok().Cheat)
                    Console.WriteLine("\t5 - Cheat bekapcsolása");
                else
                    Console.WriteLine("\t5 - Cheat kikapcsolása");
                Console.WriteLine("\t6 - Kilépés");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\tKérem válasszon menüpontot: ");
                try
                {
                    while (!int.TryParse(Console.ReadLine(), out menuPont) || menuPont < 1 || menuPont > 6)
                    {

                        MessageBox.Show("Hiba, nem létező menüpontot választott!");
                        break;
                    }           
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.ResetColor();
            } 
            while (menuPont < 1 || menuPont > 6);

            return menuPont;
        }

        private void nevBekeres()
        {
           
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            string s = "LEGYEN ÖN IS MILLIOMOS!";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
            Console.ResetColor();
            Console.Write("\n\n\tKérem adja meg a nevét: ");
            this.nev = Console.ReadLine();
            Console.Clear();
            Jatekos = new Jatekos(nev);
            szint = 1;
            Console.Clear();
        }

        private void segitseg()
        {
            Console.WriteLine("Elérhető segítségek:");
            if (!Jatekos.KozonsegSegitseg)
            {
                Console.WriteLine("\tK - Közönség segítsége");
            }
            if (!Jatekos.FelezoSegitseg)
            {
                Console.WriteLine("\tF - Számítógép segítsége");
            }
            if (!Jatekos.TelefonosSegitseg)
            {
                Console.WriteLine("\tT - Telefon segítsége");
            }
        }

        private void valaszEllnenorzes()
        {
            char valasz;
            string lehetsegesValaszok = "ABCD";
            string segitsegek = "KFT";
            string mentes = "M";
            do
            {
                Console.Write("\nKérem adja meg a helyes választ: ");
                valasz = char.Parse(Console.ReadLine());
                valasz = Char.ToUpper(valasz);

                if (valasz == 'K' && !Jatekos.KozonsegSegitseg)
                {
                    kozonsegSegitseg(ks);
                    Jatekos.kozonsegSegitsegetHasznal();
                }
                else if (valasz == 'K' && Jatekos.KozonsegSegitseg)
                {
                    Console.WriteLine("Ezt a segitséget már használta!");
                }
                if (valasz == 'F' && !Jatekos.FelezoSegitseg)
                {
                    felezoSegitseg(ks);
                    Jatekos.felezoSegitsegetHasznal();
                }
                else if (valasz == 'F' && Jatekos.FelezoSegitseg)
                {
                    Console.WriteLine("Ezt a segitséget már használta!");
                }
                if (valasz == 'T' && !Jatekos.TelefonosSegitseg)
                {
                    telefonosSegitseg(ks);
                    Jatekos.telefonosSegitsegetHasznal();
                }
                else if (valasz == 'T' && Jatekos.TelefonosSegitseg)
                {
                    Console.WriteLine("Ezt a segitséget már használta!");
                }
                if (valasz == 'M')
                {
                    new Mentes(Jatekos, ks);
                    Console.WriteLine("Játék mentése...");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("A játék mentésre került");
                    break;
                }
                if (!lehetsegesValaszok.Contains(valasz) && !segitsegek.Contains(valasz) && !mentes.Contains(valasz))
                {
                    Console.WriteLine("Érvénytelen karaktert adott meg!");
                    Console.ReadLine();
                }
            } 
            while (!lehetsegesValaszok.Contains(valasz));

            if (ks.helyesE(valasz) && valasz!='M')
            {
                szint++;
                Console.WriteLine("Gratulálunk, sikeresen válaszolt!");
                if (szint == 15)
                {
                    Console.WriteLine("Gratulálunk megnyerte a főnyereményt!!");
                }
            }
            else if(!ks.helyesE(valasz) && valasz != 'M')
            {
                helyesE = false;
                Console.WriteLine("Sajnáljuk, de rossz választ adott!");
                Console.WriteLine("A helyes válasz ez lett volna: " + ks.HelyesValasz);
                switch (szint)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: Console.WriteLine("Az ön nyereménye: 0"); break;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10: Console.WriteLine("Az ön nyereménye: 250 000 Ft"); break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15: Console.WriteLine("Az ön nyereménye: 2 000 000 Ft"); break;
                }          

            }
            else if (valasz == 'M')
            {
                Console.WriteLine("Nyomjon egy ENTER-t a kilépéshez!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.ReadKey();
        }

        public void valaszEllnenorzesBetoltesnel()
        {
            char valasz;
            string lehetsegesValaszok = "ABCD";
            string segitsegek = "KFT";
            string mentes = "M";
            do
            {
                Console.Write("\nKérem adja meg a helyes választ: ");
                valasz = char.Parse(Console.ReadLine());
                valasz = Char.ToUpper(valasz);

                if (valasz == 'K' && !Jatekos.KozonsegSegitseg)
                {
                    kozonsegSegitseg(k);
                    jatekos.kozonsegSegitsegetHasznal();
                }
                else if (valasz == 'K' && jatekos.KozonsegSegitseg)
                {
                    Console.WriteLine("Ezt a segitséget már használta!");
                }
                if (valasz == 'F' && !jatekos.FelezoSegitseg)
                {
                    felezoSegitseg(k);
                    jatekos.felezoSegitsegetHasznal();
                }
                else if (valasz == 'F' && jatekos.FelezoSegitseg)
                {
                    Console.WriteLine("Ezt a segitséget már használta!");
                }
                if (valasz == 'T' && !jatekos.TelefonosSegitseg)
                {
                    telefonosSegitseg(k);
                    jatekos.telefonosSegitsegetHasznal();
                }
                else if (valasz == 'T' && jatekos.TelefonosSegitseg)
                {
                    Console.WriteLine("Ezt a segitséget már használta!");
                }
                if (valasz == 'M')
                {
                    new Mentes(jatekos, k);
                }
                if (!lehetsegesValaszok.Contains(valasz) && !segitsegek.Contains(valasz) && !mentes.Contains(valasz))
                {
                    Console.WriteLine("Érvénytelen karaktert adott meg!");
                    Console.ReadLine();
                }
            }
            while (!lehetsegesValaszok.Contains(valasz));

            if (k.helyesE(valasz))
            {
                szint++;
                Console.WriteLine("Gratulálunk, sikeresen válaszolt!");
                if (szint == 15)
                {
                    Console.WriteLine("Gratulálunk megnyerte a főnyereményt!!");
                }
            }
            else
            {
                helyesE = false;
                Console.WriteLine("Sajnáljuk, de rossz választ adott!");
                Console.WriteLine("A helyes válasz ez lett volna: " + k.HelyesValasz);
                switch (szint)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: Console.WriteLine("Az ön nyereménye: 0"); break;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10: Console.WriteLine("Az ön nyereménye: 250 000 Ft"); break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15: Console.WriteLine("Az ön nyereménye: 2 000 000 Ft"); break;
                }

            }
        }

        private void jatekBetoltes()
        {
            Mentes m = new Mentes();
            Jatekos = m.J;
            this.szint = int.Parse(m.K.NehezsegiSzint);
            k = m.K;
            Console.Clear();
            Console.WriteLine(szint + ". kérdés a következő:");
            Console.WriteLine(k);
            segitseg();
            valaszEllnenorzesBetoltesnel();
            Console.ReadKey();

            kerdes();

            // ide kell a játék
            // ide jöhet a jatekInditastól különböző rész
            ranglista();
            File.Delete("mentes.txt");
            Console.ReadKey();
        }

        private void kerdes()
        {
            do
            {
                Console.Clear();
                helyesE = true;
                ks = kerdesek.getVeletlenKerdes(szint);
                Console.WriteLine(szint + ". kérdés a következő:");
                Console.WriteLine(ks);
                segitseg();
                valaszEllnenorzes();
            }
            while (szint <= 15 && helyesE);
        }

        private void sorkerdes()
        {
            sk = this.sorKerdesek.getVeletlenSorKerdes();

            Console.WriteLine(sk);
            Console.Write("Kérem adja meg a helyes sorrendet: ");
            string tipp = Console.ReadLine();
            if (sk.helyesE(tipp))
            {
                kerdes();
            }
            else
                Console.WriteLine("A helyes válasz ez lett volna: " + sk.HelyesSorrend);
        }

        private void jatekInditasa()
        {
            nevBekeres();
            Console.WriteLine("Kedves {0} üdvözlünk a játékban!", Jatekos.Nev);

            sorkerdes();

            this.jatekIdo = Jatekos.getJatekIdo(DateTime.Now);           

            Console.WriteLine("Sajnáljuk a játék végét ért! Ön {0} perc {1} másodpercet játszott!", jatekIdo / 60, jatekIdo % 60);

            ranglista();
            Console.ReadKey();

        }

        private void ranglista()
        {
            try
            {
                int szintLocal = this.szint - 1;
                IList<Ranglista> rangLista;
                if (File.Exists("ranglista.txt"))
                {

                    List<string> sor = new List<string>();
                    StreamReader sr = new StreamReader("ranglista.txt", Encoding.UTF8);
                    while (!sr.EndOfStream)
                    {
                        sor.Add(sr.ReadLine());
                    }
                    sr.Close();
                    //File.Delete("ranglista.txt");
                    if (sor.Count==1)
                    {
                        rangLista = new List<Ranglista>()
                        {
                            new Ranglista(sor[0]),
                            new Ranglista(this.nev,szintLocal,this.jatekIdo)
                        };
                        var result = rangLista.OrderByDescending(s => s.Eredmeny).ThenBy(s => s.JatszottMasodperc);
                        StreamWriter sw = new StreamWriter("ranglista.txt", false, Encoding.UTF8);
                        foreach (var std in result)
                        {
                            sw.WriteLine(std.Nev + ";" + std.Eredmeny + ";" + std.JatszottMasodperc);
                        }
                        sw.Close();
                    }     
                    else if (sor.Count == 2)
                    {
                        rangLista = new List<Ranglista>()
                        {
                            new Ranglista(sor[0]),
                            new Ranglista(sor[1]),
                            new Ranglista(this.nev,szintLocal,this.jatekIdo)
                        };
                        var result = rangLista.OrderByDescending(s => s.Eredmeny).ThenBy(s => s.JatszottMasodperc);
                        StreamWriter sw = new StreamWriter("ranglista.txt", false, Encoding.UTF8);
                        foreach (var std in result)
                        {
                            sw.WriteLine(std.Nev + ";" + std.Eredmeny + ";" + std.JatszottMasodperc);
                        }
                        sw.Close();
                    }
                    else if (sor.Count == 3)
                    {
                        rangLista = new List<Ranglista>()
                        {
                            new Ranglista(sor[0]),
                            new Ranglista(sor[1]),
                            new Ranglista(sor[2]),
                            new Ranglista(this.nev,szintLocal,this.jatekIdo)
                        };
                        var result = rangLista.OrderByDescending(s => s.Eredmeny).ThenBy(s => s.JatszottMasodperc);
                        StreamWriter sw = new StreamWriter("ranglista.txt", false, Encoding.UTF8);
                        foreach (var std in result)
                        {
                            sw.WriteLine(std.Nev + ";" + std.Eredmeny + ";" + std.JatszottMasodperc);
                        }
                        sw.Close();
                    }
                    else
                    {
                        rangLista = new List<Ranglista>()
                        {
                            new Ranglista(sor[sor.Count-1]),
                            new Ranglista(this.nev,szintLocal,this.jatekIdo)
                        };
                        var result = rangLista.OrderByDescending(s => s.Eredmeny).ThenBy(s => s.JatszottMasodperc);
                        StreamWriter sw = new StreamWriter("ranglistaIdeiglenes.txt", false, Encoding.UTF8);
                        foreach (var std in result)
                        {
                            sw.WriteLine(std.Nev + ";" + std.Eredmeny + ";" + std.JatszottMasodperc);
                        }
                        sw.Close();
                        List<string> ideiglenesLista = new List<string>();
                        StreamReader sre = new StreamReader("ranglistaIdeiglenes.txt", Encoding.UTF8);
                        while (!sre.EndOfStream)
                        {
                            ideiglenesLista.Add(sre.ReadLine());
                        }
                        
                        string csere = ideiglenesLista[0];
                        sor[sor.Count - 1] = csere;
                        sre.Close();
                        File.Delete("ranglistaIdeiglenes.txt");
                        StreamWriter swr = new StreamWriter("ranglista.txt", false, Encoding.UTF8);
                        foreach (var item in sor)
                        {
                            swr.WriteLine(item);
                        }
                        swr.Close();
                        File.Delete("ranglista.txt");
                        rangLista = new List<Ranglista>()
                        {
                            new Ranglista(sor[0]),
                            new Ranglista(sor[1]),
                            new Ranglista(sor[2]),
                            new Ranglista(this.nev,szintLocal,this.jatekIdo)
                        };
                        var result2 = rangLista.OrderByDescending(r => r.Eredmeny).ThenBy(r => r.JatszottMasodperc);
                        StreamWriter asd = new StreamWriter("ranglista.txt", false, Encoding.UTF8);
                        foreach (var std in result2)
                        {
                            asd.WriteLine(std.Nev + ";" + std.Eredmeny + ";" + std.JatszottMasodperc);
                        }
                        asd.Close();
                    }
                }
                else
                {
                    StreamWriter sw = new StreamWriter("ranglista.txt", false, Encoding.UTF8);
                    sw.WriteLine("{0};{1};{2}", this.nev, szintLocal, this.jatekIdo);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ranglistaMegjelenites()
        {
            // beolvasás, kilistázás
            Console.Clear();
            List<string> rang = new List<string>();
            StreamReader r = new StreamReader("ranglista.txt", Encoding.UTF8);
            while (!r.EndOfStream)
            {
                string sor = r.ReadLine();
                rang.Add(sor);
            }
            r.Close();
            string s = "TOP 3";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(s);
            Console.ResetColor();
            string[] adatok;
            List<string> nev=new List<string>();
            List<int> eredmeny= new List<int>();
            List<long> ido=new List<long>();
            foreach (var item in rang)
            {
                adatok = item.Split(';');
                nev.Add(adatok[0]);
                eredmeny.Add(int.Parse(adatok[1]));
                ido.Add(long.Parse(adatok[2]));              
            }
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\t{0,-1}.hely\tNév: {1,-10}\tSzint: {2,-1}\tIdő: {3,-3}mp.", i + 1, nev[i], eredmeny[i], ido[i]);
                    Console.ResetColor();
                }
                if (i==1)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n\t{0,-1}.hely\tNév: {1,-10}\tSzint: {2,-1}\tIdő: {3,-3}mp.", i + 1, nev[i], eredmeny[i], ido[i]);
                    Console.ResetColor();
                }
                if (i==2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\t{0,-1}.hely\tNév: {1,-10}\tSzint: {2,-1}\tIdő: {3,-3}mp.", i + 1, nev[i], eredmeny[i], ido[i]);
                    Console.ResetColor();
                }
                
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\n\tNyomjon egy ENTER-t a visszalépéshez!");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void kozonsegSegitseg(Kerdes k)
        {

            int helyesValasz = Program.rnd.Next(30, 71);
            int masodik = Program.rnd.Next(0, 16);
            int harmadik = Program.rnd.Next(0, 16);
            int negyedik = 100 - helyesValasz - masodik - harmadik;

            char c = k.HelyesValasz;
            Console.WriteLine("A közönség a következőket tippelte:");
            switch (c)
            {
                case 'A':
                    Console.WriteLine("A - " + helyesValasz);
                    Console.WriteLine("B - " + masodik);
                    Console.WriteLine("C - " + harmadik);
                    Console.WriteLine("D - " + negyedik);
                    break;
                case 'B':
                    Console.WriteLine("A - " + masodik);
                    Console.WriteLine("B - " + helyesValasz);
                    Console.WriteLine("C - " + harmadik);
                    Console.WriteLine("D - " + negyedik);
                    break;
                case 'C':
                    Console.WriteLine("A - " + masodik);
                    Console.WriteLine("B - " + harmadik);
                    Console.WriteLine("C - " + helyesValasz);
                    Console.WriteLine("D - " + negyedik);
                    break;
                case 'D':
                    Console.WriteLine("A - " + masodik);
                    Console.WriteLine("B - " + harmadik);
                    Console.WriteLine("C - " + negyedik);
                    Console.WriteLine("D - " + helyesValasz);
                    break;
            }
        }

        private void felezoSegitseg(Kerdes k)
        {
            string valaszA = k.ValaszA;
            string valaszB = k.ValaszB;
            string valaszC = k.ValaszC;
            string valaszD = k.ValaszD;

            int szam = Program.rnd.Next(0, 3);

            char c = k.HelyesValasz;
            switch (c)
            {
                case 'A':
                    if (szam == 0)
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tB - " + valaszB);
                    }
                    else if (szam == 1)
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tC - " + valaszC);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    break;
                case 'B':
                    if (szam == 0)
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tB - " + valaszB);
                    }
                    else if (szam == 1)
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tB - " + valaszB);
                        Console.WriteLine("\n\tC - " + valaszC);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tB - " + valaszB);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    break;
                case 'C':
                    if (szam == 0)
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tB - " + valaszB);
                        Console.WriteLine("\n\tC - " + valaszC);
                    }
                    else if (szam == 1)
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tC - " + valaszC);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tC - " + valaszC);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    break;
                case 'D':
                    if (szam == 0)
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    else if (szam == 1)
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tB - " + valaszB);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(k.Kerdes);
                        Console.WriteLine("\n\tC - " + valaszC);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    break;
            }
        }

        private void telefonosSegitseg(Kerdes k)
        {
            char c = k.HelyesValasz;

            double szam=Program.rnd.Next(0,101);

            switch (c)
            {
                case 'A':
                    if (szam>30)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: A");
                    }
                    if(szam<10)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: B");
                    }
                    if (szam > 10 && szam<20)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: C");
                    }
                    if (szam > 20 && szam < 30)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: D");
                    }                                    
                    break;
                case 'B':
                    if (szam > 30)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: B");
                    }
                    if (szam < 10)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: A");
                    }
                    if (szam > 10 && szam < 20)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: C");
                    }
                    if (szam > 20 && szam < 30)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: D");
                    }
                    break;
                case 'C':
                    if (szam > 30)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: C");
                    }
                    if (szam < 10)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: B");
                    }
                    if (szam > 10 && szam < 20)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: A");
                    }
                    if (szam > 20 && szam < 30)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: D");
                    }
                    break;
                case 'D':
                    if (szam > 0.30)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: D");
                    }
                    if (szam < 0.10)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: B");
                    }
                    if (szam > 0.10 && szam < 0.20)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: C");
                    }
                    if (szam > 0.20 && szam < 0.30)
                    {
                        Console.WriteLine("Szerintem a helyes válasz: A");
                    }
                    break;
            }
        }

        private void osszegek()
        {
            Console.Clear();
            Console.WriteLine("Nyeremény összegek:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\t1  - {0,-8} Ft",10000);
            Console.WriteLine("\t2  - {0,-8} Ft", 20000);
            Console.WriteLine("\t3  - {0,-8} Ft", 50000);
            Console.WriteLine("\t4  - {0,-8} Ft",100000);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\t5  - {0,-8} Ft",250000);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t6  - {0,-8} Ft",500000);
            Console.WriteLine("\t7  - {0,-8} Ft",750000);
            Console.WriteLine("\t8  - {0,-8} Ft",1000000);
            Console.WriteLine("\t9  - {0,-8} Ft",1500000);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\t10 - {0,-8} Ft",2000000);
            Console.ResetColor(); 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t11 - {0,-8} Ft",5000000);
            Console.WriteLine("\t12 - {0,-8} Ft",10000000);
            Console.WriteLine("\t13 - {0,-8} Ft",15000000);
            Console.WriteLine("\t14 - {0,-8} Ft",25000000);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\t15 - {0,-8} Ft",50000000);
            Console.ResetColor();            

            Console.ReadKey();
        }

    }
}
