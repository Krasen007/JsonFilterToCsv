namespace JsonFilterToCsv
{
    using JsonFilterToCsv.Model;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class Program
    {
        private static void Main(string[] args)
        {
            // This is used when the app is run on a PC using ',' (comma) as a decimal separator.
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            string localVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!.ToString();
            Console.Title = "Super Star Finder " + localVer;

            if (args.Length == 0)
            {
                IfNoArgsGenerateSampleJson();
            }
            else
            {
                _ = new Begin(args);
            }
        }

        private static void IfNoArgsGenerateSampleJson()
        {
            string testJson;
            List<Player> playerList = new List<Player>();

            Console.WriteLine("Not enough arguments provided.\n" +
                "\nPlease indicate separated with a space:\n" +
                "-the path to the .json file,\n" +
                "-the maximum number of years the player has played to qualify,\n" +
                "-the player's minimum rating, and\n" +
                "-the path for SuperStar.csv export.\n" +
                "\nGenerating sample.json file in the app folder...");

            var playerKrasen = new Player
            {
                Name = "Krasen Ivanov",
                PlayerSince = 2019,
                Position = ".Net Developer",
                Rating = 67.9
            };
            var playerTest = new Player
            {
                Name = "Test Person",
                PlayerSince = 2015,
                Position = "Test Developer",
                Rating = 47.9
            };

            playerList.Add(playerKrasen);
            playerList.Add(playerTest);

            testJson = JsonConvert.SerializeObject(playerList);
            using StreamWriter outputFile = new StreamWriter("sample.json");
            outputFile.WriteLine(testJson);
            outputFile.Dispose();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}