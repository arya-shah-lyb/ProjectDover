using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.Extensions.Configuration;

namespace ProjectDover
{
    public class GameSession
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{ get; set; }
        public RoomManager RoomManager { get; set; }
        public Inventory Inventory{ get; set; }
        public List<string> KeyEvents{ get; set; }
        public Player Player { get; set; }
        

        // Load Save game
        public GameSession(RoomManager roomManager, Inventory inventory, List<string> keyEvents, Player player)
        {
                RoomManager = roomManager;
                Inventory = inventory;
                KeyEvents = keyEvents;
                Player = player;
        }

        // Create New Game
        public GameSession(string playerName)
        {
                RoomManager = new RoomManager(GameType.NEW_GAME);
                Inventory = new Inventory("Player Inventory");
                KeyEvents = new List<string>();
                Player = new Player(playerName);
        }

        public string Summary(){
            StringBuilder summary = new  StringBuilder();

            summary.Append("Here your summary:");
 
            if(KeyEvents.Count > 0){
                summary.AppendFormat(Environment.NewLine);
                foreach(var keyevent in KeyEvents){
                    summary.AppendFormat("{0} ", keyevent);
                }
            }

            summary.AppendFormat("{0}{1}", Environment.NewLine, RoomManager.MapCoverage());

            return summary.ToString();

        }

        public string SaveGame(string connStr){

            IMongoCollection<GameSession> _gameSessions;
            var client = new MongoClient("mongodb://localhost:27017");
            
            var database = client.GetDatabase("Blind2021Db");

            _gameSessions = database.GetCollection<GameSession>("GameSessions");
            _gameSessions.InsertOne(this);

            return "Game saved.";
        }

        public string LoadGameSession(string playerName, IConfigurationRoot gameConfig){
            GameLoader.LoadGameSession(playerName, gameConfig);
            return "Game loaded.";
        }
    }
}