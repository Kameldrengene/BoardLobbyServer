using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public class BoardServerDatabaseSettings : IBoardServerDatabaseSettings
    {
        public string AdminsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string PlayersCollectionName { get; set; }
    }

    public interface IBoardServerDatabaseSettings
    {
        string AdminsCollectionName { get; set; }
        string PlayersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
