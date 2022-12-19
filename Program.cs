using System;
using System.IO;

namespace DistanciaCidades
{
    public class Program {
        static void Main(string[] args)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string distancesFileName = "matriz.txt";

            string distancesFileAddress = Path.Combine(desktopPath, distancesFileName);

            string matrixText = "00,15,30,05,12\n15,00,10,17,28\n30,10,00,03,11\n05,17,03,00,80\n12,28,11,80,00";
            File.WriteAllText(distancesFileAddress, matrixText);
            
            string[] distancesLines = File.ReadAllLines(distancesFileAddress);

            int[,] distancesArray = new int[distancesLines.Length, distancesLines.Length];

            for (int i = 0; i < distancesLines.Length; i++)
            { 
                int[] distances = distancesLines[i].Split(",").Select(n => Convert.ToInt32(n)).ToArray();
                distances = distances.ToArray();

                for (int j = 0; j < distances.Length; j++)
                {
                    distancesArray[i, j] = distances[j];
                }
            }

            string pathsFileName = "caminhos.txt";

            string pathsFileAddress = Path.Combine(desktopPath, pathsFileName);

            string pathsText = "1, 2, 3, 2, 5, 1, 4";
            File.WriteAllText(pathsFileAddress, pathsText);
            
            string[] pathsLine = File.ReadAllLines(pathsFileAddress);

            int[] paths = pathsLine[0].Split(", ").Select(n => Convert.ToInt32(n)).ToArray();

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