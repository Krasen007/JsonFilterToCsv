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
            string pathToJsonFile;
            var playerList = new List<Player>();
            int maxPlayedYears = 0;
            double minPlayerRating = 0.0;

            if (args.Length >= 1)
            {
                pathToJsonFile = args[0];
                Console.WriteLine("You loaded json file:");
                Console.WriteLine(pathToJsonFile + "\n");

                JsonSerializer serializer = new JsonSerializer();
                using StreamReader sr = new StreamReader(pathToJsonFile);
                using JsonTextReader reader = new JsonTextReader(sr);
                {
                    var jsonFile = serializer.Deserialize<Player[]>(reader);

                    foreach (var player in jsonFile)
                    {
                        Console.WriteLine(player.Name);
                        playerList.Add(player);
                    }
                }

                if (args.Length >= 2)
                {
                    foreach (var player in playerList)
                    {
                        // if (maxPlayedYears >= 1)
                        // {

                        // }
                        System.Console.WriteLine(player.PlayerSince);
                    }

                }
                else
                {
                    System.Console.WriteLine("Please provide maximum number of years the player has been in NBA.");
                }

                if (args.Length >= 3)
                {
                    foreach (var player in playerList)
                    {
                        System.Console.WriteLine(player.Rating);
                    }
                }
                else
                {
                    System.Console.WriteLine("Please provide minimum required rating the player has to quailify.");
                }

                if (args.Length >= 4)
                {
                    using StreamWriter outputFile = new StreamWriter("results.csv");
                    foreach (var item in playerList)
                    {
                        outputFile.WriteLine(item.Position);
                    }                    
                }
                else
                {
                    System.Console.WriteLine("Please provide path for the csv file to be saved.");
                }
            }

            // Use for serialization.            
            if (args.Length == 0)
            {
                string testJson;
                Console.WriteLine("No arguments given.\nGenerating example json file...");

                var testPlayer = new Player
                {
                    Name = "Krasen Ivanov",
                    PlayerSince = 2019,
                    Position = ".Net Developer",
                    Rating = 67.9
                };
                var testPlayer2 = new Player
                {
                    Name = "Test Person",
                    PlayerSince = 1019,
                    Position = "Test Developer",
                    Rating = 17.9
                };

                playerList.Add(testPlayer);
                playerList.Add(testPlayer2);

                testJson = JsonConvert.SerializeObject(playerList);

                using StreamWriter outputFile = new StreamWriter("example.json");
                outputFile.WriteLine(testJson);
            }
        }
    }
}