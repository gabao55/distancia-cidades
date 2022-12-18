using System;
using System.IO;

namespace DistanciaCidades
{
    public class Program {
        static void Main(string[] args)
        {
            int[,] distancesArray = {
                { 0, 15, 30, 5, 12 },
                { 15, 0, 10, 17, 28 },
                { 30, 10, 0, 3, 11 },
                { 5, 17, 3, 0, 80 },
                { 12, 28, 11, 80, 0 },
            };
            
            Console.WriteLine("Insira o caminho a ser percorrido:");
            string? pathsText = Console.ReadLine();

            while (pathsText == null || pathsText == "") {
                Console.WriteLine("Caminho inserido inválido, por favor insira um caminho válido (exemplo: 1,2,3,2,5,1,4):");
                pathsText = Console.ReadLine();
            }

            int[] paths = pathsText.Split(",").Select(n => Convert.ToInt32(n)).ToArray();

            int acc = 0;

            for (int i = 1; i < paths.Length; i++)
            {
                int currentLocationIndex = paths[i-1]-1;
                int nextLocationIndex = paths[i]-1;
                acc += distancesArray[currentLocationIndex, nextLocationIndex];
            }

            Console.WriteLine($"A distância percorrida foi de {acc.ToString()} km");
        }
    }
}