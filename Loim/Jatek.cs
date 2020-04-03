using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Timers;
using System.Threading;

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
        private string tipp;
        private bool bevitel;

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
            do
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
                if (this.nev.Length < 3 || this.nev.Length > 10)
                {
                    MessageBox.Show("3 és 10 karakter között legyen a név!");

                }
            }
            while (this.nev.Length < 3 || this.nev.Length > 10);
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
                Console.WriteLine("\t\tK - Közönség segítség");
            }
            if (!Jatekos.FelezoSegitseg)
            {
                Console.WriteLine("\t\tF - Felező segítség");
            }
            if (!Jatekos.TelefonosSegitseg)
            {
                Console.WriteLine("\t\tT - Telefonos segítség");
            }
            Console.ResetColor();
        }

        private void valaszEllnenorzes()
        {
            string lehetsegesValaszok = "ABCD";
            string segitsegek = "KFT";
            string mentes = "MN";
            do
            {
                try
                {
                    ClearLastLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, Console.CursorTop-2);
                    Console.Write("\n\n\tKérem adja meg a helyes választ: ");
                    Console.ResetColor();
                    bevitel = char.TryParse(Console.ReadLine(), out valasz);
                    while (!bevitel)
                    {

                        MessageBox.Show("Hiba, érvénytelen bevitel!");
                        break;
                    }
                    this.valasz = Char.ToUpper(valasz);
                    if (!lehetsegesValaszok.Contains(valasz) && !segitsegek.Contains(valasz) && !mentes.Contains(valasz) && bevitel)
                    {
                        MessageBox.Show("Hiba, érvénytelen karakter!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                if (valasz == 'K' && !Jatekos.KozonsegSegitseg)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\t-Szeretném használni a közönség segítségét, Gundel úr!");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\t-Rendben,{0}! Most kérném a közönséget,\n" +
                        "\thogy vegyék elő nyomógombjaikat és nyomját meg a szerintük helyes választ!",this.nev);
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("\t-Köszönöm!");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(3000);
                    kozonsegSegitseg(ks);
                    Jatekos.kozonsegSegitsegetHasznal();
                }
                else if (valasz == 'K' && Jatekos.KozonsegSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'F' && !Jatekos.FelezoSegitseg)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\t-Szeretném használni a felező segítséget, Gundel úr!");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\t-Rendben,{0}! Rendben a gép máris elvesz kettőt.", this.nev);
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\t-Köszönöm!");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(2000);
                    felezoSegitseg(ks);
                    Jatekos.felezoSegitsegetHasznal();
                }
                else if (valasz == 'F' && Jatekos.FelezoSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'T' && !Jatekos.TelefonosSegitseg)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\t-Szeretném használni a telefonos segítségét, Gundel úr!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write("\t-Rendben,{0}! Kit hívjunk fel? ",this.nev);
                    string kit = Console.ReadLine();
                    System.Threading.Thread.Sleep(10);
                    Console.WriteLine("\tŐ úgy gondolom nagyon jó ebben a témakörben!");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("\t-Már tárcsázzuk is {0}-t. Halló! Jó napot kívánok Gundel-Takács Gábor vagyok\n" +
                        "\ta Legyen Ön is Milliomosból és azért keresem, mert itt ül velem szemben {1}, és egy kérdésben\n" +
                        "\tkérné a segítségét. A 30mp indul MOST!", kit, this.nev);
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\t-Szia! Ez lenne gyorsan a kérdésem:");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("\t-"+ks.Kerdes);
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("\tA válaszok a következők:\n"+ks.getKerdesValaszok());
                    int rand = Program.rnd.Next(0, 3);
                    if (rand==0)
                    {
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Jó kérdés, de örülök, hogy engem hívtál, mert úgy érzem tudom a választ!");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Szuper! Nagyon örülök mi lenne az?");
                    }
                    else if (rand==1)
                    {
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Nem könnyü, de van egy sejtásem!");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Rendben! Mi lenne az?");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Nem vagyok 100%-ig biztos, de");
                    }
                    else if((rand==2 || rand==1) && szint>10)
                    {
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Nagyon nehéz kérdés! Egyenlőre tanácstalan vagyok!");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Mi az amit kizárnál?");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Hát, 2-őt tuti, a maradékon vacilálok! Talán meg lesz ez");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Gyorsan mondd, mert lejár az idő!");
                    }
                    Console.ResetColor();
                    telefonosSegitseg(ks);
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("-Köszönöm a segítséget!");
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
                if (valasz == 'N')
                {
                    Console.WriteLine("\n\t-Gundel úr, szeretnék megállni!");
                    Console.Write("\n\t-Biztos benne?(i/n)");
                    char dontes = char.Parse(Console.ReadLine());
                    if (dontes=='i')
                    {
                        switch (szint - 1)
                        {
                            case 0: Console.WriteLine("\n\tAz ön nyereménye: 0 Ft"); break;
                            case 1: Console.WriteLine("\n\tAz ön nyereménye: 10 000 Ft"); break;
                            case 2: Console.WriteLine("\n\tAz ön nyereménye: 20 000 Ft"); break;
                            case 3: Console.WriteLine("\n\tAz ön nyereménye: 50 000 Ft"); break;
                            case 4: Console.WriteLine("\n\tAz ön nyereménye: 100 000 Ft"); break;
                            case 5: Console.WriteLine("\n\tAz ön nyereménye: 250 000 Ft"); break;
                            case 6: Console.WriteLine("\n\tAz ön nyereménye: 500 000 Ft"); break;
                            case 7: Console.WriteLine("\n\tAz ön nyereménye: 750 000 Ft"); break;
                            case 8: Console.WriteLine("\n\tAz ön nyereménye: 1 000 000 Ft"); break;
                            case 9: Console.WriteLine("\n\tAz ön nyereménye: 1 500 000 Ft"); break;
                            case 10: Console.WriteLine("\n\tAz ön nyereménye: 2 000 000 Ft"); break;
                            case 11: Console.WriteLine("\n\tAz ön nyereménye: 5 000 000 Ft"); break;
                            case 12: Console.WriteLine("\n\tAz ön nyereménye: 10 000 000 Ft"); break;
                            case 13: Console.WriteLine("\n\tAz ön nyereménye: 15 000 000 Ft"); break;
                            case 14: Console.WriteLine("\n\tAz ön nyereménye: 25 000 000 Ft"); break;
                        }
                        break;
                    }
                    else
                    {
                        try
                        {
                            ClearLastLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(0, Console.CursorTop - 2);
                            Console.Write("\n\n\tKérem adja meg a helyes választ: ");
                            Console.ResetColor();
                            bevitel = char.TryParse(Console.ReadLine(), out valasz);
                            while (!bevitel)
                            {

                                MessageBox.Show("Hiba, érvénytelen bevitel!");
                                break;
                            }
                            this.valasz = Char.ToUpper(valasz);
                            if (!lehetsegesValaszok.Contains(valasz) && !segitsegek.Contains(valasz) && !mentes.Contains(valasz) && bevitel)
                            {
                                MessageBox.Show("Hiba, érvénytelen karakter!");
                            }
                        }
                        catch(Exception)
                        { 
                        }
                    }
                }
            }
            while (!lehetsegesValaszok.Contains(valasz));

            if (valasz=='A')
            {
                ClearLastLine();
                Console.Write("\n\tMegjelöljük a következő választ: ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ks.ValaszA);
                Console.ResetColor();
                System.Threading.Thread.Sleep(3000);
            }
            else if (valasz == 'B')
            {
                ClearLastLine();
                Console.Write("\n\tMegjelöljük a következő választ: ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ks.ValaszB);
                Console.ResetColor();
                System.Threading.Thread.Sleep(3000);
            }
            else if (valasz == 'C')
            {
                ClearLastLine();
                Console.Write("\n\tMegjelöljük a következő választ: ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ks.ValaszC);
                Console.ResetColor();
                System.Threading.Thread.Sleep(3000);
            }
            else if (valasz == 'D')
            {
                ClearLastLine();
                Console.Write("\n\tMegjelöljük a következő választ: ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ks.ValaszD);
                Console.ResetColor();
                System.Threading.Thread.Sleep(3000);
            }

            if (ks.helyesE(valasz) && valasz != 'M' && valasz != 'N')
            {
                for (int i = 0; i < 11; i++)
                {
                    ClearLastLine();

                    if (i%2==0)
                    {
                        Console.Write("\t");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Gratulálunk, sikeresen válaszolt!");
                        System.Threading.Thread.Sleep(99);
                        if (szint == 15)
                        {
                            Console.Write("\t");
                            Console.WriteLine("Gratulálunk megnyerte a főnyereményt!!");
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("\t");
                        Console.WriteLine("Gratulálunk, sikeresen válaszolt!");
                        System.Threading.Thread.Sleep(99);
                        if (szint == 15)
                        {
                            Console.Write("\t");
                            Console.WriteLine("Gratulálunk megnyerte a főnyereményt!!");
                        }
                    }
                }
                szint++;
                Console.WriteLine("\n\tNyomjon egy ENTER-t a folytatáshoz!");
                Console.ResetColor();
            }
            else if (!ks.helyesE(valasz) && valasz != 'M' && valasz != 'N')
            {
                helyesE = false;
                for (int i = 0; i < 11; i++)
                {
                    ClearLastLine();

                    if (i % 2 == 0)
                    {
                        Console.Write("\t");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Sajnálom, de rossz a válasz, így el kell búcsúznunk egymástól!");
                        System.Threading.Thread.Sleep(99);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("\t");
                        Console.WriteLine("Sajnálom, de rossz a válasz, így el kell búcsúznunk egymástól!");
                        System.Threading.Thread.Sleep(99);
                    }
                }
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
            else if (valasz == 'N')
            {
                Console.WriteLine("\n\tNyomjon egy ENTER-t a kilépéshez!");
                this.jatekIdo = Jatekos.getJatekIdo(DateTime.Now);
                ranglista();
                Console.ReadKey();
                Jatek j = new Jatek();
            }
            Console.ReadKey();
        }

        public void valaszEllnenorzesBetoltesnel()
        {
            string lehetsegesValaszok = "ABCD";
            string segitsegek = "KFT";
            string mentes = "MN";
            do
            {
                try
                {
                    ClearLastLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    Console.Write("\n\n\tKérem adja meg a helyes választ: ");
                    Console.ResetColor();
                    var bevitel = char.TryParse(Console.ReadLine(), out valasz);
                    while (!bevitel)
                    {

                        MessageBox.Show("Hiba, érvénytelen bevitel!");
                        break;
                    }
                    this.valasz = Char.ToUpper(valasz);
                    if (!lehetsegesValaszok.Contains(valasz) && !segitsegek.Contains(valasz) && !mentes.Contains(valasz) && bevitel)
                    {
                        MessageBox.Show("Hiba, érvénytelen karakter!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (valasz == 'K' && !Jatekos.KozonsegSegitseg)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\t-Szeretném használni a közönség segítségét, Gundel úr!");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\t-Rendben,{0}! Most kérném a közönséget,\n" +
                        "\thogy vegyék elő nyomógombjaikat és nyomját meg a szerintük helyes választ!", this.nev);
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("\t-Köszönöm!");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(3000);
                    kozonsegSegitseg(k);
                    Jatekos.kozonsegSegitsegetHasznal();
                }
                else if (valasz == 'K' && Jatekos.KozonsegSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'F' && !Jatekos.FelezoSegitseg)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\t-Szeretném használni a felező segítséget, Gundel úr!");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\t-Rendben,{0}! Rendben a gép máris elvesz kettőt.", this.nev);
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\t-Köszönöm!");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(2000);
                    felezoSegitseg(k);
                    Jatekos.felezoSegitsegetHasznal();
                }
                else if (valasz == 'F' && Jatekos.FelezoSegitseg)
                {
                    MessageBox.Show("Ezt a segitséget már használta!");
                }
                if (valasz == 'T' && !Jatekos.TelefonosSegitseg)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\t-Szeretném használni a telefonos segítségét, Gundel úr!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write("\t-Rendben,{0}! Kit hívjunk fel? ", this.nev);
                    string kit = Console.ReadLine();
                    System.Threading.Thread.Sleep(10);
                    Console.WriteLine("\tŐ úgy gondolom nagyon jó ebben a témakörben!");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("\t-Már tárcsázzuk is {0}-t. Halló! Jó napot kívánok Gundel-Takács Gábor vagyok\n" +
                        "\ta Legyen Ön is Milliomosból és azért keresem, mert itt ül velem szemben {1}, és egy kérdésben\n" +
                        "\tkérné a segítségét. A 30mp indul MOST!", kit, this.nev);
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\t-Szia! Ez lenne gyorsan a kérdésem:");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("\t-" + k.Kerdes);
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("\tA válaszok a következők:\n" + ks.getKerdesValaszok());
                    int rand = Program.rnd.Next(0, 3);
                    if (rand == 0)
                    {
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Jó kérdés, de örülök, hogy engem hívtál, mert úgy érzem tudom a választ!");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Szuper! Nagyon örülök mi lenne az?");
                    }
                    else if (rand == 1)
                    {
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Nem könnyü, de van egy sejtásem!");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Rendben! Mi lenne az?");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Nem vagyok 100%-ig biztos, de");
                    }
                    else if ((rand == 2 || rand == 1) && szint > 10)
                    {
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Nagyon nehéz kérdés! Egyenlőre tanácstalan vagyok!");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Mi az amit kizárnál?");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Hát, 2-őt tuti, a maradékon vacilálok! Talán meg lesz ez");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\t-Gyorsan mondd, mert lejár az idő!");
                    }
                    Console.ResetColor();
                    telefonosSegitseg(k);
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("-Köszönöm a segítséget!");
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
                if (valasz == 'N')
                {
                    switch (szint - 1)
                    {
                        case 0: Console.WriteLine("\n\tAz ön nyereménye: 0 Ft"); break;
                        case 1: Console.WriteLine("\n\tAz ön nyereménye: 10 000 Ft"); break;
                        case 2: Console.WriteLine("\n\tAz ön nyereménye: 20 000 Ft"); break;
                        case 3: Console.WriteLine("\n\tAz ön nyereménye: 50 000 Ft"); break;
                        case 4: Console.WriteLine("\n\tAz ön nyereménye: 100 000 Ft"); break;
                        case 5: Console.WriteLine("\n\tAz ön nyereménye: 250 000 Ft"); break;
                        case 6: Console.WriteLine("\n\tAz ön nyereménye: 500 000 Ft"); break;
                        case 7: Console.WriteLine("\n\tAz ön nyereménye: 750 000 Ft"); break;
                        case 8: Console.WriteLine("\n\tAz ön nyereménye: 1 000 000 Ft"); break;
                        case 9: Console.WriteLine("\n\tAz ön nyereménye: 1 500 000 Ft"); break;
                        case 10: Console.WriteLine("\n\tAz ön nyereménye: 2 000 000 Ft"); break;
                        case 11: Console.WriteLine("\n\tAz ön nyereménye: 5 000 000 Ft"); break;
                        case 12: Console.WriteLine("\n\tAz ön nyereménye: 10 000 000 Ft"); break;
                        case 13: Console.WriteLine("\n\tAz ön nyereménye: 15 000 000 Ft"); break;
                        case 14: Console.WriteLine("\n\tAz ön nyereménye: 25 000 000 Ft"); break;
                    }
                    break;
                }
            }
            while (!lehetsegesValaszok.Contains(valasz));

            if (valasz == 'A')
            {
                ClearLastLine();
                Console.Write("\n\tMegjelöljük a következő választ: ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(k.ValaszA);
                Console.ResetColor();
                System.Threading.Thread.Sleep(3000);
            }
            else if (valasz == 'B')
            {
                ClearLastLine();
                Console.Write("\n\tMegjelöljük a következő választ: ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(k.ValaszB);
                Console.ResetColor();
                System.Threading.Thread.Sleep(3000);
            }
            else if (valasz == 'C')
            {
                ClearLastLine();
                Console.Write("\n\tMegjelöljük a következő választ: ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(k.ValaszC);
                Console.ResetColor();
                System.Threading.Thread.Sleep(3000);
            }
            else if (valasz == 'D')
            {
                ClearLastLine();
                Console.Write("\n\tMegjelöljük a következő választ: ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(k.ValaszD);
                Console.ResetColor();
                System.Threading.Thread.Sleep(3000);
            }

            if (k.helyesE(valasz) && valasz != 'M' && valasz != 'N')
            {              
                for (int i = 0; i < 11; i++)
                {
                    ClearLastLine();

                    if (i % 2 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\tGratulálunk, sikeresen válaszolt!");
                        System.Threading.Thread.Sleep(99);
                        if (szint == 15)
                        {
                            Console.WriteLine("\tGratulálunk megnyerte a főnyereményt!!");
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("\tGratulálunk, sikeresen válaszolt!");
                        System.Threading.Thread.Sleep(99);
                        if (szint == 15)
                        {
                            Console.WriteLine("\tGratulálunk megnyerte a főnyereményt!!");
                        }
                    }
                }
                Console.WriteLine("\n\tNyomjon egy ENTER-t a folytatáshoz!");
                Console.ResetColor();
                szint++;
            }
            else if (!k.helyesE(valasz) && valasz != 'M' && valasz != 'N')
            {
                helyesE = false;
                for (int i = 0; i < 11; i++)
                {
                    ClearLastLine();

                    if (i % 2 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\tSajnálom, de rossz a válasz, így el kell búcsúznunk egymástól!");
                        System.Threading.Thread.Sleep(99);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("\tSajnálom, de rossz a válasz, így el kell búcsúznunk egymástól!");
                        System.Threading.Thread.Sleep(99);
                    }
                }
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
            else if (valasz == 'N')
            {
                Console.WriteLine("\n\tNyomjon egy ENTER-t a kilépéshez!");
                this.jatekIdo = Jatekos.getJatekIdo(DateTime.Now);
                ranglista();
                Console.ReadKey();
                Jatek j = new Jatek();
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
            Console.WriteLine("Játék betöltése...");
            System.Threading.Thread.Sleep(1000);
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
            megallasMentesMegjelenites();
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

                megallasMentesMegjelenites();


                valaszEllnenorzes();
               
            }
            while (szint <= 15 && helyesE);
        }

        private void sorkerdes()
        {
            sk = this.sorKerdesek.getVeletlenSorKerdes();
            do
            {               
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\tSorkérdés - Témakör: " + sk.getSorKerdesKategoria());
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
                    tipp = Console.ReadLine().ToUpper();
                    while (tipp.Length!=4 || tipp.Length==1)
                    {
                        MessageBox.Show("Hiba, érvénytelen bevitel!");
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } 
            while (tipp.Length!=4 || tipp.Length==1);

            if (sk.helyesE(tipp))
            {
                for (int i = 0; i < 11; i++)
                {
                    ClearLastLine();

                    if (i % 2 == 0)
                    {
                        Console.Write("\t");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Gratulálunk, sikeresen válaszolt!");
                        System.Threading.Thread.Sleep(99);                       
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("\t");
                        Console.WriteLine("Gratulálunk, sikeresen válaszolt!");
                        System.Threading.Thread.Sleep(99);                        
                    }
                }
                Console.Write("\n\tNyomjon egy ENTER-t a folytatáshoz!");
                Console.ResetColor();
                Console.ReadKey();
                kerdes();
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    ClearLastLine();

                    if (i % 2 == 0)
                    {
                        Console.Write("\t");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Sajnáljuk, de nem volt helyes a sorrend!");
                        System.Threading.Thread.Sleep(99);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("\t");
                        Console.WriteLine("Sajnáljuk, de nem volt helyes a sorrend!");
                        System.Threading.Thread.Sleep(99);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tA helyes válasz ez lett volna: " + sk.HelyesSorrend);
                Console.ResetColor();
                Console.WriteLine("\n\tÍgy most nem került bele a játékba..");
                
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

            Console.WriteLine("\n\tSajnáljuk a játék végét ért! Ön {0} perc {1} másodpercet játszott!", jatekIdo / 60, jatekIdo % 60);
            Console.WriteLine("\n\tNyomjon egy ENTER-t a kilépéshez!");
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
                Console.WriteLine();
                string s = "TOP 3";
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(s);
                Console.WriteLine();
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
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n\tA közönség szerint a helyes válasz::");
            switch (c)
            {
                case 'A':
                    Console.WriteLine("\t\tA - " + helyesValasz);
                    Console.WriteLine("\t\tB - " + masodik);
                    Console.WriteLine("\t\tC - " + harmadik);
                    Console.WriteLine("\t\tD - " + negyedik);
                    break;             
                case 'B':              
                    Console.WriteLine("\t\tA - " + masodik);
                    Console.WriteLine("\t\tB - " + helyesValasz);
                    Console.WriteLine("\t\tC - " + harmadik);
                    Console.WriteLine("\t\tD - " + negyedik);
                    break;            
                case 'C':              
                    Console.WriteLine("\t\tA - " + masodik);
                    Console.WriteLine("\t\tB - " + harmadik);
                    Console.WriteLine("\t\tC - " + helyesValasz);
                    Console.WriteLine("\t\tD - " + negyedik);
                    break;             
                case 'D':              
                    Console.WriteLine("\t\tA - " + masodik);
                    Console.WriteLine("\t\tB - " + harmadik);
                    Console.WriteLine("\t\tC - " + negyedik);
                    Console.WriteLine("\t\tD - " + helyesValasz);
                    break;
            }
            Console.ResetColor();
            Console.WriteLine("\n");
        }

        private void felezoSegitseg(Kerdes k)
        {
            string valaszA = k.ValaszA;
            string valaszB = k.ValaszB;
            string valaszC = k.ValaszC;
            string valaszD = k.ValaszD;

            int szam = Program.rnd.Next(0, 3);

            char c = k.HelyesValasz;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n\tA gép elvett kettőt: ");
            switch (c)
            {
                case 'A':
                    if (szam == 0)
                    {
                        
                        
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tB - " + valaszB);
                    }
                    else if (szam == 1)
                    {
                        
                        
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tC - " + valaszC);
                    }
                    else
                    {
                        
                       
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    break;
                case 'B':
                    if (szam == 0)
                    {
                        
                       
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tB - " + valaszB);
                    }
                    else if (szam == 1)
                    {
                        
                       
                        Console.WriteLine("\n\tB - " + valaszB);
                        Console.WriteLine("\n\tC - " + valaszC);
                    }
                    else
                    {
                        
                        
                        Console.WriteLine("\n\tB - " + valaszB);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    break;
                case 'C':
                    if (szam == 0)
                    {
                       
                        
                        Console.WriteLine("\n\tB - " + valaszB);
                        Console.WriteLine("\n\tC - " + valaszC);
                    }
                    else if (szam == 1)
                    {
                        
                        
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tC - " + valaszC);
                    }
                    else
                    {
                        
                     
                        Console.WriteLine("\n\tC - " + valaszC);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    break;
                case 'D':
                    if (szam == 0)
                    {
                        
                        
                        Console.WriteLine("\n\tA - " + valaszA);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    else if (szam == 1)
                    {
                        
                        
                        Console.WriteLine("\n\tB - " + valaszB);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    else
                    {
                        
                        
                        Console.WriteLine("\n\tC - " + valaszC);
                        Console.WriteLine("\n\tD - " + valaszD);
                    }
                    break;
            }
            Console.ResetColor();
            Console.WriteLine("\n");
        }

        private void telefonosSegitseg(Kerdes k)
        {
            char c = k.HelyesValasz;

            double szam=Program.rnd.Next(0,101);
            Console.ForegroundColor = ConsoleColor.DarkGray;

            switch (c)
            {
                case 'A':
                    if (szam>30)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: A");
                    }
                    if(szam<10)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: B");
                    }
                    if (szam > 10 && szam<20)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: C");
                    }
                    if (szam > 20 && szam < 30)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: D");
                    }                                    
                    break;
                case 'B':
                    if (szam > 30)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: B");
                    }
                    if (szam < 10)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: A");
                    }
                    if (szam > 10 && szam < 20)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: C");
                    }
                    if (szam > 20 && szam < 30)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: D");
                    }
                    break;
                case 'C':
                    if (szam > 30)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: C");
                    }
                    if (szam < 10)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: B");
                    }
                    if (szam > 10 && szam < 20)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: A");
                    }
                    if (szam > 20 && szam < 30)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: D");
                    }
                    break;
                case 'D':
                    if (szam > 0.30)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: D");
                    }
                    if (szam < 0.10)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: B");
                    }
                    if (szam > 0.10 && szam < 0.20)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: C");
                    }
                    if (szam > 0.20 && szam < 0.30)
                    {
                        Console.WriteLine("\tSzerintem a helyes válasz: A");
                    }
                    break;
            }
            Console.ResetColor();
            Console.WriteLine("\n");
        }

        private void osszegek()
        {
            Console.Clear();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            string s = "Nyeremény összegek:";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
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
            Console.ForegroundColor = ConsoleColor.Yellow;
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

        private void megallasMentesMegjelenites()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tTovábbi lehetőségek:");
            Console.WriteLine("\t\tM - Mentés");
            Console.WriteLine("\t\tN - Megállás, nyeremény elvitele\n");
            Console.ResetColor();
        }

        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
