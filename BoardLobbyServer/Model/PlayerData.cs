using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public sealed class PlayerData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        private string _name;
        private string password;

        public PlayerData(string name)
        {
            this._name = name;
        }
        public PlayerData() { }

        public string Name
        {
            get { return _name; }
            set { this._name = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
