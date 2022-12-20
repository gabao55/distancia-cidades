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
            
            int[,] distancesArray = new int[5, 5];

            using (var reader = new StreamReader(distancesFileAddress)) 
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using (var csv = new CsvReader(reader, config)) 
                {
                    int i = 0;
                    csv.Read();
                    var record = csv.GetRecord<DistanceInfo>();
                    distancesArray[i, 0] = record.First;
                    distancesArray[i, 1] = record.Second;
                    distancesArray[i, 2] = record.Third;
                    distancesArray[i, 3] = record.Fourth;
                    distancesArray[i, 4] = record.Fifth;
                    i++;
                    while (csv.Read())
                    {
                        record = csv.GetRecord<DistanceInfo>();
                        distancesArray[i, 0] = record.First;
                        distancesArray[i, 1] = record.Second;
                        distancesArray[i, 2] = record.Third;
                        distancesArray[i, 3] = record.Fourth;
                        distancesArray[i, 4] = record.Fifth;
                        i++;
                    }
                }
            }

            string pathsFileName = "caminhos.txt";

            string pathsFileAddress = Path.Combine(desktopPath, pathsFileName);

            string pathsText = "1, 2, 3, 2, 5, 1, 4";
            File.WriteAllText(pathsFileAddress, pathsText);

            int[] paths = new int[7];

            using (var reader = new StreamReader(pathsFileAddress))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ", ",
                    HasHeaderRecord = false,
                };
                using (var csv = new CsvReader(reader, config)) 
                {
                    int i = 0;
                    csv.Read();
                    var record = csv.GetRecord<PathInfo>();
                    paths[0] = record.First;
                    paths[1] = record.Second;
                    paths[2] = record.Third;
                    paths[3] = record.Fourth;
                    paths[4] = record.Fifth;
                    paths[5] = record.Sixth;
                    paths[6] = record.Seventh;
                }
            }

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

    public class DistanceInfo
    {
        [Index(0)]
        public int First { get; set; }
        [Index(1)]
        public int Second { get; set; }
        [Index(2)]
        public int Third { get; set; }
        [Index(3)]
        public int Fourth { get; set; }
        [Index(4)]
        public int Fifth { get; set; }
    }

    public class PathInfo
    {
        [Index(0)]
        public int First { get; set; }
        [Index(1)]
        public int Second { get; set; }
        [Index(2)]
        public int Third { get; set; }
        [Index(3)]
        public int Fourth { get; set; }
        [Index(4)]
        public int Fifth { get; set; }
        [Index(5)]
        public int Sixth { get; set; }
        [Index(6)]
        public int Seventh { get; set; }
    }
}