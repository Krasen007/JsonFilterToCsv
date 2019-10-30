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

        readonly List<string> playerSuperstarList = new List<string>();
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
                CalculateMaxYearsPlayed(maxPlayedYears);
            }
            else
            {
                System.Console.WriteLine("Please provide maximum number of years the player has been in NBA.");
            }

            if (args.Length >= 3)
            {
                CalculateMinRequiredRating(minPlayerRating);
            }
            else
            {
                System.Console.WriteLine("Please provide minimum required rating the player has to quailify.");
            }

            if (args.Length >= 4)
            {
                savePath = args[3];
                ExportCSV(savePath);
            }
            else
            {
                System.Console.WriteLine("Please provide path for the csv file to be saved.");
            }
        }

        private void ExportCSV(string savePath)
        {
            using StreamWriter outputFile = new StreamWriter(savePath);
            foreach (var item in playerSuperstarList)
            {
                outputFile.Write(item + ",");
            }
        }

        private void CalculateMinRequiredRating(double minPlayerRating)
        {
            foreach (var player in playerList)
            {
                var ratingTolerance = player.Rating - minPlayerRating;
                if (player.Rating >= ratingTolerance)
                {
                    Console.WriteLine(player.Rating);
                    playerSuperstarList.Add(player.Name);
                }
            }
        }

        private void CalculateMaxYearsPlayed(int maxPlayedYears)
        {
            foreach (var player in playerList)
            {
                var yearTolerance = player.PlayerSince - maxPlayedYears;
                if (player.PlayerSince >= yearTolerance)
                {
                    Console.WriteLine(player.PlayerSince);
                    playerSuperstarList.Add(player.Name);
                }
            }
        }

        private void LoadJson(string pathToJsonFile)
        {
            Console.WriteLine("You loaded json file:");
            Console.WriteLine(pathToJsonFile + "\n");

            JsonSerializer serializer = new JsonSerializer();
            using StreamReader sr = new StreamReader(pathToJsonFile);
            using JsonTextReader reader = new JsonTextReader(sr);
            {
                var jsonFile = serializer.Deserialize<Player[]>(reader);

                foreach (var player in jsonFile)
                {
                    playerList.Add(player);
                }
                System.Console.WriteLine("Json Loaded succesfuly...");
            }
        }
    }
}