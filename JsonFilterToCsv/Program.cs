namespace JsonFilterToCsv
{
    using JsonFilterToCsv.Model;
    using Newtonsoft.Json;
    using System;
    using System.IO;

    public static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("You loaded json file:");
            string pathToJsonFile;

            if (args.Length != 0)
            {
                pathToJsonFile = args[0];
                Console.WriteLine(pathToJsonFile + "\n");

                JsonSerializer serializer = new JsonSerializer();
                using StreamReader sr = new StreamReader(pathToJsonFile);
                using JsonTextReader reader = new JsonTextReader(sr);
                {
                    var jsonFile = serializer.Deserialize<Player[]>(reader);

                    foreach (var item in jsonFile)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
            }

            // Use for serialization.
            ////string testJson;
            ////if (args.Length == 0)
            ////{
            ////    Console.WriteLine("No arguments given...");
            ////    var playerList = new List<Player>();

            ////    var testPlayer = new Player
            ////    {
            ////        Name = "Krasen Ivanov",
            ////        PlayerSince = 2019,
            ////        Position = ".Net Developer",
            ////        Rating = 67.9
            ////    };
            ////    var testPlayer2 = new Player
            ////    {
            ////        Name = "Oen Danov",
            ////        PlayerSince = 1019,
            ////        Position = "Developer",
            ////        Rating = 17.9
            ////    };

            ////    playerList.Add(testPlayer);
            ////    playerList.Add(testPlayer2);

            ////    testJson = JsonConvert.SerializeObject(playerList);

            ////    using (StreamWriter outputFile = new StreamWriter("results.txt"))
            ////    {
            ////        outputFile.WriteLine(testJson);
            ////    }
            ////}
            ////else
            ////{
            ////    Player givenPlayer = JsonConvert.DeserializeObject<Player>(args[0]);
            ////    Console.WriteLine(givenPlayer.Name);
            ////}
        }
    }
}