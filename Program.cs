using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    class Adat
    {
        public string Kod { get; set; }
        public int Perc { get; set; }
        public int Esemeny { get; set; }
        public string Ido { get; set; }

        public Adat(string kod, int perc, int esemeny, string ido)
        {
            this.Kod = kod;
            this.Perc = perc;
            this.Esemeny = esemeny;
            this.Ido = ido;
        }
    }
    static void Main()
    {
        // 1. feladat
        List<Adat> adatok = new List<Adat>();

        foreach (var sor in File.ReadAllLines("bedat.txt"))
        {
            var reszek = sor.Split(' ');
            string kod = reszek[0];
            string ido = reszek[1];
            int esemeny = int.Parse(reszek[2]);

            var idoResz = ido.Split(':');
            int perc = int.Parse(idoResz[0]) * 60 + int.Parse(idoResz[1]);

            adatok.Add(new Adat(kod, perc, esemeny, ido));
        }

        

        // 2. feladat
        string elso = "";
        string utolso = "";

        foreach (var a in adatok)
        {
            if (a.Esemeny == 1 && elso == "")
                elso = a.Ido;

            if (a.Esemeny == 2)
                utolso = a.Ido;
        }

       
        Console.WriteLine($"Az elso belepo: {elso}");
        Console.WriteLine($"Az utolso kilepo: {utolso}");

        // 3. feladat
        using (StreamWriter sw = new StreamWriter("kesok.txt"))
        {
            foreach (var a in adatok)
            {
                if (a.Esemeny == 1 && a.Perc > 7 * 60 + 50 && a.Perc <= 8 * 60 + 15)
                {
                    sw.WriteLine($"{a.Ido} {a.Kod}");
                }
            }
        }


        // 4. feladat
        int szam = 0;

        foreach (var a in adatok)
        {
            if (a.Esemeny == 3)
            {
                szam++;
            }
                
        }

        Console.WriteLine("4. feladat:");
        Console.WriteLine($"A menzan ebedelt tanulok szama: {szam}");

        // 5. feladat
        
        int szam1 = 0;
        foreach (var a in adatok)
        {
            if (a.Esemeny == 4) {  szam1++; }
                
        }

        
        Console.WriteLine($"Konyvtarban kolcsonzok szama: {szam1}");

        if (szam1 > szam) { Console.WriteLine("Tobben voltak, mint a menzan."); }

        else {
            Console.WriteLine("Nem voltak tobben, mint a menzan.");
        }

        // 6. feladat
        List<string> kint = new List<string>();
        List<string> vissza = new List<string>();

        foreach (var a in adatok)
        {
            if (a.Esemeny == 2 && a.Perc >= 10 * 60 + 45 && a.Perc <= 10 * 60 + 50)
                kint.Add(a.Kod);

            if (a.Esemeny == 1 && a.Perc > 10 * 60 + 50 && kint.Contains(a.Kod))
                vissza.Add(a.Kod);
        }

        
        foreach (var k in vissza)
            Console.Write(k + " ");
        Console.WriteLine();

        // 7. feladat
        Console.Write("7. feladat: Adja meg a tanulo azonositojat: ");
        string azon = Console.ReadLine();

        int? belep = null;
        int? kilep = null;

        foreach (var a in adatok)
        {
            if (a.Kod == azon)
            {
                if (a.Esemeny == 1 && belep == null)
                    belep = a.Perc;

                if (a.Esemeny == 2)
                    kilep = a.Perc;
            }
        }

        if (belep == null)
        {
            Console.WriteLine("Ilyen azonositoju tanulo aznap nem volt az iskolaban.");
        }
        else
        {
            int ido = kilep.Value - belep.Value;
            int ora = ido / 60;
            int perc = ido % 60;

            Console.WriteLine($"A bent toltott ido: {ora} ora {perc} perc.");
        }
    }
}

