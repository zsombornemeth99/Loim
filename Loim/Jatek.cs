using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Media;

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
        private char valasz;

        internal Jatekos Jatekos { get => jatekos; set => jatekos = value; }
       
        public Jatek()
        {
            this.sorKerdesek = new SorKerdesek("sorkerdes.txt");
            this.kerdesek = new Kerdesek("kerdes.txt");

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
                    case 6: MessageBox.Show("Köszönjük, hogy részt vett a játékban");
                            Environment.Exit(0); break;
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
                Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n\n\tKérem adja meg a nevét: ");
            Console.ResetColor();
            this.nev = Console.ReadLine();
            Console.Clear();
            Jatekos = new Jatekos(nev);
            szint = 1;
            Console.Clear();
        }

        private void segitseg()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tElérhető segítségek:");
            if (!Jatekos.KozonsegSegitseg)
            {
                Console.WriteLine("\t\tK - Közönség segítsége");
            }
            if (!Jatekos.FelezoSegitseg)
            {
                Console.WriteLine("\t\tF - Számítógép segítsége");
            }
            if (!Jatekos.TelefonosSegitseg)
            {
                Console.WriteLine("\t\tT - Telefon segítsége");
            }
            Console.ResetColor();
        }

        private void valaszEllnenorzes()
        {
            char valasz;
            string lehetsegesValaszok = "ABCD";
            string segitsegek = "KFT";
            string mentes = "M";
            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n\tKérem adja meg a helyes választ: ");
                Console.ResetColor();
                valasz = char.Parse(Console.ReadLine());
                valasz = Char.ToUpper(valasz);

                if (valasz == 'K' && !Jatekos.KozonsegSegitseg)
                {
                    kozonsegSegitseg(ks);
                    Jatekos.kozonsegSegitsegetHasznal();
                }
                else if (valasz == 'K' && Jatekos.KozonsegSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'F' && !Jatekos.FelezoSegitseg)
                {
                    felezoSegitseg(ks);
                    Jatekos.felezoSegitsegetHasznal();
                }
                else if (valasz == 'F' && Jatekos.FelezoSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'T' && !Jatekos.TelefonosSegitseg)
                {
                    telefonosSegitseg(ks);
                    Jatekos.telefonosSegitsegetHasznal();
                }
                else if (valasz == 'T' && Jatekos.TelefonosSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'M')
                {
                    new Mentes(Jatekos, ks);
                    Console.WriteLine("\tJáték mentése...");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\tA játék mentésre került");
                    break;
                }
                if (!lehetsegesValaszok.Contains(valasz) && !segitsegek.Contains(valasz) && !mentes.Contains(valasz))
                {
                    MessageBox.Show("Érvénytelen karaktert adott meg!");
                    
                }
            }
            while (!lehetsegesValaszok.Contains(valasz));

            if (ks.helyesE(valasz) && valasz != 'M')
            {
                szint++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tGratulálunk, sikeresen válaszolt!");
                if (szint == 15)
                {
                    Console.WriteLine("\tGratulálunk megnyerte a főnyereményt!!");
                }
                Console.ResetColor();
            }
            else if (!ks.helyesE(valasz) && valasz != 'M')
            {
                helyesE = false;
                Console.ForegroundColor = ConsoleColor.Red;
                MessageBox.Show("\tSajnáljuk, de rossz választ adott!");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tA helyes válasz ez lett volna: " + ks.HelyesValasz);
                switch (szint)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: Console.WriteLine("\n\tAz ön nyereménye: 0"); break;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10: Console.WriteLine("\n\tAz ön nyereménye: 250 000 Ft"); break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15: Console.WriteLine("\n\tAz ön nyereménye: 2 000 000 Ft"); break;
                }
                Console.ResetColor();
            }
            else if (valasz == 'M')
            {
                Console.WriteLine("\n\tNyomjon egy ENTER-t a kilépéshez!");
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n\tKérem adja meg a helyes választ: ");
                Console.ResetColor();
                valasz = char.Parse(Console.ReadLine());
                valasz = Char.ToUpper(valasz);

                if (valasz == 'K' && !Jatekos.KozonsegSegitseg)
                {
                    kozonsegSegitseg(k);
                    Jatekos.kozonsegSegitsegetHasznal();
                }
                else if (valasz == 'K' && Jatekos.KozonsegSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'F' && !Jatekos.FelezoSegitseg)
                {
                    felezoSegitseg(k);
                    Jatekos.felezoSegitsegetHasznal();
                }
                else if (valasz == 'F' && Jatekos.FelezoSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'T' && !Jatekos.TelefonosSegitseg)
                {
                    telefonosSegitseg(k);
                    Jatekos.telefonosSegitsegetHasznal();
                }
                else if (valasz == 'T' && Jatekos.TelefonosSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'M')
                {
                    new Mentes(Jatekos, k);
                    Console.WriteLine("\tJáték mentése...");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\tA játék mentésre került");
                    break;
                }
                if (!lehetsegesValaszok.Contains(valasz) && !segitsegek.Contains(valasz) && !mentes.Contains(valasz))
                {
                    MessageBox.Show("Érvénytelen karaktert adott meg!");
                }
            }
            while (!lehetsegesValaszok.Contains(valasz));

            if (k.helyesE(valasz) && valasz != 'M')
            {
                szint++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tGratulálunk, sikeresen válaszolt!");
                if (szint == 15)
                {
                    Console.WriteLine("\tGratulálunk megnyerte a főnyereményt!!");
                }
                Console.ResetColor();
            }
            else if (!k.helyesE(valasz) && valasz != 'M')
            {
                helyesE = false;
                Console.ForegroundColor = ConsoleColor.Red;
                MessageBox.Show("\tSajnáljuk, de rossz választ adott!");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tA helyes válasz ez lett volna: " + k.HelyesValasz);
                switch (szint)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: Console.WriteLine("\n\tAz ön nyereménye: 0"); break;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10: Console.WriteLine("\n\tAz ön nyereménye: 250 000 Ft"); break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15: Console.WriteLine("\n\tAz ön nyereménye: 2 000 000 Ft"); break;
                }
                Console.ResetColor();
            }
            else if (valasz == 'M')
            {
                Console.WriteLine("\n\tNyomjon egy ENTER-t a kilépéshez!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.ReadKey();
        }

        private void jatekBetoltes()
        {
            Mentes m = new Mentes();
            jatekos = m.J;
            this.szint = int.Parse(m.K.NehezsegiSzint);
            this.nev = m.J.Nev;
            
            k = m.K;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t{0}. kérdés - Témakör: {1}", this.szint, k.getKerdesKategoria());
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - k.getKerdes().Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(k.getKerdes());
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n");
            Console.WriteLine(k.getKerdesValaszok());
            Console.ResetColor();
            Console.WriteLine();

            segitseg();
            valaszEllnenorzesBetoltesnel();
            if(helyesE)
                kerdes();

            DateTime jatekKezdete = m.J.JatekKezdete;
            DateTime jatekVege = DateTime.Now;
            this.jatekIdo = (long)jatekVege.Subtract(jatekKezdete).TotalSeconds;

            Console.WriteLine("Sajnáljuk a játék végét ért! Ön {0} perc {1} másodpercet játszott!", jatekIdo / 60, jatekIdo % 60);

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
               
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\t{0}. kérdés - Témakör: {1}",this.szint, ks.getKerdesKategoria());
                Console.WriteLine();
                Console.SetCursorPosition((Console.WindowWidth - ks.getKerdes().Length) / 2, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(ks.getKerdes());
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n");
                Console.WriteLine(ks.getKerdesValaszok());
                Console.ResetColor();
                Console.WriteLine();

                segitseg();
      
                valaszEllnenorzes();
               
            }
            while (szint <= 15 && helyesE);
        }

        private void sorkerdes()
        {
            sk = this.sorKerdesek.getVeletlenSorKerdes();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\tSorkérdés - Témakör: "+sk.getSorKerdesKategoria());
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - sk.getSorKerdes().Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(sk.getSorKerdes());
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n");
            Console.WriteLine(sk.getSorKerdesValaszok());
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\tKérem adja meg a helyes sorrendet: ");
            Console.ResetColor();
            string tipp = Console.ReadLine();
            if (sk.helyesE(tipp))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine();
                Console.WriteLine("\tGratulálunk, helyes a válasz!");
                Console.Write("\n\tNyomjon egy ENTER-t a folytatáshoz!");
                Console.ResetColor();
                Console.ReadKey();
                kerdes();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;

                MessageBox.Show("Rossz válasz!");
                Console.WriteLine();
                Console.WriteLine("\tA helyes válasz ez lett volna: " + sk.HelyesSorrend);

                Console.ResetColor();
                Console.ReadLine();
            }
        }

        private void jatekInditasa()
        {
            nevBekeres();
            udvozloUzenet();
            Console.Clear();
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
                string nev = this.nev;
                long jatekIdo = this.jatekIdo;
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
                            new Ranglista(nev,szintLocal,jatekIdo)
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
                            new Ranglista(nev,szintLocal,jatekIdo)
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
                            new Ranglista(nev,szintLocal,jatekIdo)
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
                            new Ranglista(nev,szintLocal,jatekIdo)
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
                            new Ranglista(nev,szintLocal,jatekIdo)
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
                    sw.WriteLine("{0};{1};{2}", nev, szintLocal, jatekIdo);
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
            try
            {
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
                List<string> nev = new List<string>();
                List<int> eredmeny = new List<int>();
                List<long> ido = new List<long>();
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
                    try
                    {
                        if (i == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\n\t{0,-1}.hely\tNév: {1,-10}\tSzint: {2,-1}\tIdő: {3,-3}mp.", i + 1, nev[i], eredmeny[i], ido[i]);
                            Console.ResetColor();
                        }
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        if (i == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n\t{0,-1}.hely\tNév: {1,-10}\tSzint: {2,-1}\tIdő: {3,-3}mp.", i + 1, nev[i], eredmeny[i], ido[i]);
                            Console.ResetColor();
                        }
                    }
                    catch (Exception)
                    {

                    }
                    
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\n\tNyomjon egy ENTER-t a visszalépéshez!");
                Console.ResetColor();
                Console.ReadKey();
            }
            catch (FileNotFoundException)
            {
                string s = "TOP 3";
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(s);
                Console.ResetColor();
                MessageBox.Show("Még nem jött létre ranglista!");
            }
            
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

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\n\tNyomjon egy ENTER-t a visszalépéshez!");
            Console.ResetColor();

            Console.ReadKey();
        }

        private void udvozloUzenet()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string s = "Üdvözöm kedves "+Jatekos.Nev+" a LEGYEN ÖN IS MILLIOMOS játékban!";
            Console.WriteLine("\n\n\n");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 5, Console.CursorTop);
            Console.WriteLine(s);
            Console.ResetColor();
            string ss = "A játék következőképp fog zajlani:";
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 10, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(ss);
            Console.WriteLine("\t\tA játékot egy sorkéréssel fogja kezdeni, majd ha helyesen sorrendbe" +
                " rakjla a válaszokat\n" +
                "\t\tbekerül a játékba.\n\t\tEzek után 15 kérdés áll ön elött, melyek megválaszolásáért " +
                "egyre nagyobb nyereményre thete szert.\n" +
                "\t\tHa minden kérdést helyesen megválaszol 50 millió játékpénzt nyert.\n" +
                "\t\tAz 5. és a 10. kérdések megválaszolását követően biztos nyereménnyel fog távozni.");
            Console.WriteLine();
            string sss = "Ha a szabályokat elolvasta, akkor egy ENTER-el elindíthatja a játékot!";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 10, Console.CursorTop);
            Console.Write(sss);
            Console.ResetColor();
            Console.ReadKey();
        }

    }
}
