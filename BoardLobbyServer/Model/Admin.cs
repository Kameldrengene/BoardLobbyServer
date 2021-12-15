using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    [Serializable]
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        private string _name { get; set; }

        private string _password { get; set; }

        public enum Type
        {
            Admin,
            Master
        }
        private Type _admintype { get; set; }
        private string _avatar { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public Type AdminType
        {
            get { return _admintype; }
            set { _admintype = value; }
        }
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }

    }
}
