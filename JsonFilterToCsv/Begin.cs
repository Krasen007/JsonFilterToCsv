using JsonFilterToCsv.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonFilterToCsv
{
    public class Begin
    {
        private readonly List<Player> playerList = new List<Player>();
        private readonly List<Player> playerSuperstarList = new List<Player>();
        private readonly int maxPlayedYears;
        private readonly double minPlayerRating;

        public Begin(string[] args)
        {
            if (args.Length >= 1 && args.Length < 5)
            {
                string pathToJsonFile = args[0];
                if (this.IsJsonLoaded(pathToJsonFile))
                {
                    if (args.Length >= 2)
                    {
                        this.maxPlayedYears = Convert.ToInt32(args[1]);
                    }
                    else
                    {
                        Console.WriteLine("Please provide the maximum number of years the player has been in NBA:");
                        this.maxPlayedYears = Convert.ToInt32(Console.ReadLine());
                    }

                    if (args.Length >= 3)
                    {
                        this.minPlayerRating = Convert.ToInt32(args[2]);
                    }
                    else
                    {
                        Console.WriteLine("Please provide the minimum required rating the player needs to qualify:");
                        this.minPlayerRating = Convert.ToInt32(Console.ReadLine());
                    }

                    string savePath;
                    if (args.Length >= 4)
                    {
                        this.CalculatePlayerNeededYearAndRating(this.maxPlayedYears, this.minPlayerRating);
                        savePath = args[3];
                        this.ExportCSV(savePath);
                    }
                    else
                    {
                        Console.WriteLine("Please provide the path for the csv file to be saved.");
                        savePath = Console.ReadLine();
                        this.CalculatePlayerNeededYearAndRating(this.maxPlayedYears, this.minPlayerRating);
                        this.ExportCSV(savePath);
                    }
                }
            } else
            {
                if (args.Length >= 5)
                {
                    Console.WriteLine("Too many parameters!");
                }
            }
        }

        private void CalculatePlayerNeededYearAndRating(int maxPlayedYears, double minPlayerRating)
        {
            var currentYear = DateTime.Today.Year;
            foreach (var player in this.playerList)
            {
                var yearTolerance = player.PlayerSince + maxPlayedYears;
                if ((currentYear <= yearTolerance) && (player.Rating >= minPlayerRating))
                {
                    Console.WriteLine($"Playing since {player.PlayerSince}, {player.Name} meets the required rating of {minPlayerRating} with a score of {player.Rating} achieved in under {maxPlayedYears} years, and qualifies as NBA Superstar in the making.");
                    this.playerSuperstarList.Add(player);
                }
            }
        }

        private void ExportCSV(string savePath)
        {
            if (this.playerSuperstarList.Count == 0)
            {
                Console.WriteLine("No SuperStar found among your players.");
            }
            else
            {
                using StreamWriter outputFile = new StreamWriter(savePath + "\\SuperStars.csv");
                foreach (var player in this.playerSuperstarList)
                {
                    outputFile.WriteLine(player.Name + "," + String.Format("{0:0.0}", player.Rating));
                }
                outputFile.Dispose();
                Console.WriteLine("\nThese are your SuperStars!\nSuccesfuly saved to " + savePath + "\\SuperStars.csv");
            }
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        private bool IsJsonLoaded(string pathToJsonFile)
        {
            Console.WriteLine("You selected json:");
            Console.WriteLine(pathToJsonFile + "\n");

            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using StreamReader sr = new StreamReader(pathToJsonFile);
                using JsonTextReader reader = new JsonTextReader(sr);
                {
                    var jsonFile = serializer.Deserialize<Player[]>(reader);

                    foreach (var player in jsonFile)
                    {
                        this.playerList.Add(player);
                    }
                }
                Console.WriteLine("Json Loaded succesfuly...\n");
                return true;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return false;
            }
        }
    }
}