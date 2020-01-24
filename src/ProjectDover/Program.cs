using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ProjectDover
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static private void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            GameType gameType = Introduction();
            string playerName = PlayerCreation();

            var parser = new CommandParser();
            GameSession GameSession; 

            if (gameType == GameType.LOADED_GAME)
            {
                GameSession = GameLoader.LoadGameSession(playerName, configuration);
                Console.WriteLine(GameSession.Summary() + Environment.NewLine);
                Console.WriteLine(GameSession.RoomManager.CurrentRoomDescription);
            }
            else {
                GameSession = new GameSession(playerName);
            }

            while (true)
            {
                // spit room desc
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(GameSession.RoomManager.CurrentRoomName);
                Console.ResetColor();
                if (!GameSession.RoomManager.CurrentRoomHasSeenDescription)
                {
                    Console.WriteLine(GameSession.RoomManager.CurrentRoomDescription);
                }

                Console.WriteLine(GameSession.RoomManager.CurrentRoomExits());
                Console.Write("> ");
                var inputString = Console.ReadLine();

                var command = parser.ProcessCommandText(inputString);
                switch (command)
                {
                    case Command.COMMAND_QUIT:
                        {
                            DoQuit();
                        }
                        break;
                    case Command.COMMAND_NORTH:
                    case Command.COMMAND_SOUTH:
                    case Command.COMMAND_EAST:
                    case Command.COMMAND_WEST:
                    {
                        GameSession.RoomManager.Go(command);
                    }
                        break;
                    case Command.COMMAND_LOOK:
                        {
                            GameSession.RoomManager.Do(command);
                        }
                        break;
                    case Command.COMMAND_INVENTORY:
                        {
                            GameSession.Inventory.ListItems();
                        }
                        break;
                    case Command.COMMAND_TAKE:
                        {
                            DoTake(GameSession, inputString);
                        }
                        break;
                    case Command.COMMAND_SUMMARY:
                        {
                            Console.WriteLine(GameSession.Summary());
                        }
                        break;
                    case Command.COMMAND_SAVE:
                    {
                        var connStr = configuration["Blind2021DatabaseSettings:ConnectionString"];
                        Console.WriteLine(GameSession.SaveGame(connStr));
                    }
                    break;
                    case Command.COMMAND_HANDLED: break;
                    default:
                        {
                            Console.WriteLine();
                            Console.WriteLine("I'm not sure what to do");
                        }
                        break;
                }
            }
        }

        private static void DoTake(GameSession GameSession, string inputString)
        {
            Inventory roomInventory = GameSession.RoomManager.CurrentRoomInventory();
            string itemName = ExtractItemName(inputString);

            if (roomInventory.Contains(itemName))
            {
                Item currentItem = roomInventory.RemoveItem(itemName);
                HandleKeyEvent(GameSession, currentItem);
                GameSession.Inventory.AddItem(currentItem);
            }
        }

        private static void HandleKeyEvent(GameSession GameSession, Item currentItem)
        {
            if (currentItem.Triggers.ContainsKey("take"))
            {
                string keyEvent = GameSession.RoomManager.ProcessTrigger(currentItem, "take");
                if (!String.IsNullOrEmpty(keyEvent))
                {
                    GameSession.KeyEvents.Add(keyEvent);
                }
            }
        }

        private static string ExtractItemName(string inputString)
        {
            return inputString.Split(' ')[1];
        }

        private static void DoQuit()
        {
            Console.Clear();
            Console.WriteLine("Thanks for playing!");
            Environment.Exit(0);
        }

        static private GameType Introduction(){

            Console.Clear();
            Console.WriteLine("-=- Welcome to Blind2021 -=-");

            var asciiArtPath = configuration.GetValue<string>("medias:asciiArtRelativePath");
            var path = Directory.GetCurrentDirectory() + asciiArtPath;

            Console.Write(File.ReadAllText(path)); 
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("This is a text based adventure game...");

            Console.WriteLine("Do you want to:");
            Console.WriteLine("1 - Start a New Game.");

            var client = new MongoClient("mongodb://localhost:27017");
            if(client != null)
                Console.WriteLine("2 - Load a Saved Game.");
            
            Console.Write("> ");
            var inputString = Console.ReadLine();

            var selectedGameType = GameTypeParser.ProcessGameTypeText(inputString);
            Console.WriteLine(Environment.NewLine);

            return selectedGameType;
        }


        public static string PlayerCreation(){

            bool validName = false;
            string inputString = string.Empty;

            while(!validName)
            {
                Console.WriteLine("How should I call you?");
                Console.Write("> ");
                inputString = Console.ReadLine();

                if(inputString.Length >= 5){
                    validName = true;
                }
            }

            Console.WriteLine(String.Format("Happy to have you with us {0}, now let's get started.", inputString));
            Console.WriteLine(Environment.NewLine);

            return inputString;
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
        }

    }
}
