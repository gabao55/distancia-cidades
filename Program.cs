using System;
using System.IO;

namespace DistanciaCidades
{
    public class Program {
        static void Main(string[] args)
        {
            Console.WriteLine("Quantas cidades quer utilizar para análise do percurso?");
            bool validateInput;
            int matrixLength;

            do
            {
                validateInput = int.TryParse(Console.ReadLine(), out matrixLength);
            } while (!validateInput);

            int[,] distancesArray = new int[matrixLength, matrixLength];
            int currentDistance;

            for (int i = 0; i < matrixLength; i++)
            {
                for (int j = i; j < matrixLength; j++)
                {
                    if (i == j) {
                        distancesArray[i, j] = 0;
                    }
                    else 
                    {   
                        do
                        {
                            Console.WriteLine($"Informe a distância entre as cidades {i+1} e {j+1}:");
                            validateInput = int.TryParse(Console.ReadLine(), out currentDistance);
                        } while (!validateInput);

                        distancesArray[i, j] = currentDistance;
                        distancesArray[j, i] = currentDistance;
                    }
                }
            }
            
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