using System;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

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
            
            int[,] distancesMatrix;

            using (var reader = new StreamReader(distancesFileAddress)) 
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using (var csv = new CsvParser(reader, config)) 
                {
                    int i = 0;

                    csv.Read();
                    var record = csv.Record;
                    distancesMatrix = new int[record.Length, record.Length];
                    
                    MatrixAndArrayPopulator.PopulateMatrixRow(distancesMatrix, i, record);

                    while (csv.Read())
                    {
                        i++;
                        record = csv.Record;
                        MatrixAndArrayPopulator.PopulateMatrixRow(distancesMatrix, i, record);
                    }
                }
            }

            string pathsFileName = "caminhos.txt";

            string pathsFileAddress = Path.Combine(desktopPath, pathsFileName);

            string pathsText = "1, 2, 3, 2, 5, 1, 4";
            File.WriteAllText(pathsFileAddress, pathsText);

            int[] paths;

            using (var reader = new StreamReader(pathsFileAddress))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = false,
                };
                using (var csv = new CsvParser(reader, config)) 
                {
                    csv.Read();
                    var record = csv.Record;
                    paths = new int[record.Length];
                    MatrixAndArrayPopulator.PopulateArray(paths, record);
                }
            }

            int acc = 0;

            for (int i = 1; i < paths.Length; i++)
            {
                int currentLocationIndex = paths[i-1]-1;
                int nextLocationIndex = paths[i]-1;
                acc += distancesMatrix[currentLocationIndex, nextLocationIndex];
            }

            Console.WriteLine($"A distância percorrida foi de {acc.ToString()} km");
        }
    }

    public static class MatrixAndArrayPopulator
    {
        public static void PopulateMatrixRow(int[,] matrix, int row, string[] rowData)
        {
            for (int i = 0; i < rowData.Length; i++)
            {
                bool validation = int.TryParse(rowData[i], out int castedData);
                if (validation)
                {
                    matrix[row, i] = castedData;
                }
            }
        }

        public static void PopulateArray(int[] array, string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                bool validation = int.TryParse(data[i], out int castedData);
                if (validation)
                {
                    array[i] = castedData;
                }
            }
        }
    }
}