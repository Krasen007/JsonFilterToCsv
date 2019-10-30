using System;
using System.Collections.Generic;
using System.IO;
using JsonFilterToCsv.Model;
using Newtonsoft.Json;

namespace JsonFilterToCsv
{
    public class Begin
    {
        readonly string pathToJsonFile = string.Empty;
        readonly List<Player> playerList = new List<Player>();

        readonly List<Player> playerSuperstarList = new List<Player>();
        readonly int maxPlayedYears = 0;
        readonly double minPlayerRating = 0.0;
        readonly string savePath = string.Empty;
        public Begin(string[] args)
        {
            if (args.Length >= 1)
            {
                pathToJsonFile = args[0];
                LoadJson(pathToJsonFile);
            }

            if (args.Length >= 2)
            {
                maxPlayedYears = Convert.ToInt32(args[1]);
                //CalculateMaxYearsPlayed(maxPlayedYears);
            }
            else
            {
                Console.WriteLine("Please provide maximum number of years the player has been in NBA.");
            }

            if (args.Length >= 3)
            {
                minPlayerRating = Convert.ToInt32(args[2]);
                //CalculateMinRequiredRating(minPlayerRating);
            }
            else
            {
                Console.WriteLine("Please provide minimum required rating the player has to quailify.");
            }

            if (args.Length >= 4)
            {
                CalculatePlayerNeededYearAndRating(maxPlayedYears, minPlayerRating);
                savePath = args[3];
                ExportCSV(savePath);
            }
            else
            {
                Console.WriteLine("Please provide path for the csv file to be saved.");
            }
        }

        private void CalculatePlayerNeededYearAndRating(int maxPlayedYears, double minPlayerRating)
        {
            var currentYear = DateTime.Today.Year;
            foreach (var player in playerList)
            {
                var yearTolerance = player.PlayerSince + maxPlayedYears;
                if ((currentYear <= yearTolerance) && (player.Rating >= minPlayerRating))
                {
                    Console.WriteLine($"Player {player.Name} quialifies with {player.Rating} rating of {minPlayerRating} rating needed.");
                    Console.WriteLine($"Player {player.Name} qualifies with {player.PlayerSince} years of max {yearTolerance - player.PlayerSince} years needed.");
                    playerSuperstarList.Add(player);
                }
            }
        }

        private void ExportCSV(string savePath)
        {
            using StreamWriter outputFile = new StreamWriter(savePath);
            foreach (var player in playerSuperstarList)
            {
                outputFile.WriteLine(player.Name + "," + player.Rating);
            }
        }

        private void CalculateMinRequiredRating(double minPlayerRating)
        {
            foreach (var player in playerList)
            {
                if (player.Rating >= minPlayerRating)
                {
                    Console.WriteLine($"Player {player.Name} quialifies with {player.Rating} rating of {minPlayerRating} rating needed.");
                }
            }
        }

        private void CalculateMaxYearsPlayed(int maxPlayedYears)
        {
            var currentYear = DateTime.Today.Year;
            foreach (var player in playerList)
            {
                var yearTolerance = player.PlayerSince + maxPlayedYears;
                if (currentYear <= yearTolerance)
                {
                    //playerSuperstarList.Add(player.Name);
                    Console.WriteLine($"Player {player.Name} qualifies with {player.PlayerSince} years of max {yearTolerance - player.PlayerSince} years needed.");
                }
            }
        }

        private void LoadJson(string pathToJsonFile)
        {
            Console.WriteLine("You loaded json file:");
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
                        playerList.Add(player);
                    }
                    Console.WriteLine("Json Loaded succesfuly...");
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}