using BoardLobbyServer.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Services
{
    public class PlayerService
    {
        private readonly IMongoCollection<PlayerData> _player;

        public PlayerService(IBoardServerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _player = database.GetCollection<PlayerData>(settings.PlayersCollectionName);
            Console.WriteLine("connection estabished");
        }

        public List<PlayerData> Get() =>
            _player.Find(player => true).ToList();
        public PlayerData Get(string id) =>
            _player.Find<PlayerData>(player => player.Id == id).FirstOrDefault();
        public PlayerData Create(PlayerData player)
        {
            player.Password = BCrypt.Net.BCrypt.HashPassword(player.Password);
            _player.InsertOne(player);
            return player;
        }

        public PlayerData Verify(string name, string password)
        {
            PlayerData player= _player.Find<PlayerData>(player => player.Name == name).FirstOrDefault();

            if (player == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, player.Password))
                return player;
            else
                return null;
        }


    }
}
