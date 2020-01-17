using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDover
{
    class GameLoader
    {
        public static GameSession LoadGameSession(string playerName, IConfigurationRoot config)
        {
            IMongoCollection<GameSession> _gameSessions;
            var client = new MongoClient(config["Blind2021DatabaseSettings:ConnectionString"]);

            var database = client.GetDatabase(config["Blind2021DatabaseSettings:DatabaseName"]);

            _gameSessions = database.GetCollection<GameSession>(config["Blind2021DatabaseSettings:RoomsCollectionName"]);
            var game = _gameSessions.Find(g => g.Player.Name == playerName).SingleOrDefault<GameSession>();

            GameSession session = new GameSession(game.RoomManager, game.Inventory, game.KeyEvents, game.Player);
            session.Id = game.Id;

            return session;
        }
    }
}
