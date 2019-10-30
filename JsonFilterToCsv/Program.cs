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
            // Use for serialization.            
            if (args.Length == 0)
            {
                IfNoArgsGenerateExapmleJson();
            }
            else
            {
                new Begin(args);
            }
        }

        private static void IfNoArgsGenerateExapmleJson()
        {
            string testJson;
            List<Player> playerList = new List<Player>();

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