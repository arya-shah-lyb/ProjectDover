using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDover
{
    class GameLoader
    {
        public static GameSession LoadGameSession(string playerName)
        {
            IMongoCollection<GameSession> _gameSessions;
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("Blind2021Db");

            _gameSessions = database.GetCollection<GameSession>("GameSessions");
            var game = _gameSessions.Find(g => g.Player.Name == playerName).SingleOrDefault<GameSession>();

            GameSession session = new GameSession(game.RoomManager, game.Inventory, game.KeyEvents, game.Player);
            session.Id = game.Id;

            return session;
        }
    }
}
