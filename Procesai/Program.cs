// Procesu laiku skaiciuokle
// Copyright by Paulius Lozys
// 2020-04-18 :^)


using System;
using System.Collections.Generic;
using System.Linq;

namespace Procesai
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> laikai = new List<int>()
            {
                10, 6, 2, 4, 8
                //53,8,68,24
                //24,3,3
                //3,3,24
            };


            RoundRobin(laikai,2);
            Console.WriteLine();
            FCFS(laikai);
            Console.WriteLine();
            SPF(laikai);

        }

        public static void RoundRobin(List<int> laikai, int kvantas, int paleidimoLaikai = 0)
        {
            Console.WriteLine("Round Robin: ");
            bool arNeVisiProcesaiAtlikti(int[] kopija)
            {
                foreach (var item in kopija)
                {
                    if (item > 0)
                        return true;
                }
                return false;
            }

            int[] kopija = new int[laikai.Count];
                laikai.CopyTo(kopija, 0);
            int[] wt =     new int[laikai.Count];
            int[] preitoProcesoPabaiga = new int[laikai.Count];

            double kiek = laikai.Count;
            int totalusVeikimoLaikas = 0;
            bool yraLikeProcesu = true;

            while (yraLikeProcesu)
            {
                for (int i = 0; i < kopija.Length; i++)
                {
                    // Dabartinis P# procesas

                    if (kopija[i] <= 0) // Jei procesas jau atliktas, praleidziama
                        continue;

                    // Proceso pradzia
                    wt[i] += totalusVeikimoLaikas - preitoProcesoPabaiga[i];

                    totalusVeikimoLaikas += kopija[i] < kvantas ? kopija[i] : kvantas;

                    // Issaugoma kada uzsibaige paskutinis P# procesas
                    preitoProcesoPabaiga[i] = totalusVeikimoLaikas;

                    kopija[i] -= kvantas;
                }
                yraLikeProcesu = arNeVisiProcesaiAtlikti(kopija);
            }

            Console.WriteLine($"Wt (avarage) = { (wt.Sum() - paleidimoLaikai) / kiek}");
            Console.WriteLine($"Turnaround / RT (avarage) = { (preitoProcesoPabaiga.Sum() - paleidimoLaikai) / kiek }");
        }

        public static void FCFS(List<int> laikai, int paleidimoLaikai = 0)
        {
            Console.WriteLine("FCFS: ");
            int suma = 0;
            double isViso = 0;
            double suma2 = 0;
            for (int i = 0; i < laikai.Count; i++)
            {
                suma += laikai[i];
                if (i < laikai.Count - 1)  
                    suma2 += suma;

                isViso += suma;
            }
            Console.WriteLine($"Wt (avarage) = { (suma2 - paleidimoLaikai)  / laikai.Count}");
            Console.WriteLine($"Turnaround / RT (avarage) = { (isViso - paleidimoLaikai) / laikai.Count }");
        }

        public static void SPF(List<int> laikai)
        {
            Console.WriteLine("SPF: ");
            var kopija = laikai.OrderBy(x => x).ToList();

            int suma = 0;
            double isViso = 0;
            double suma2 = 0;
            for (int i = 0; i < laikai.Count; i++)
            {
                suma += kopija[i];
                if (i < laikai.Count - 1)
                    suma2 += suma;

                isViso += suma;
            }
            Console.WriteLine($"Wt (avarage) = { suma2 / laikai.Count}");
            Console.WriteLine($"Turnaround / RT (avarage) = { isViso / laikai.Count }");
        }
    }
}
