using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public sealed class LobbyData
    {
        private static readonly object padlock = new object();
        private static LobbyData instance = null;
        private Dictionary<string,GameData> _games;
        private Dictionary<string,BoardData> _boards;

        LobbyData()
        {
            _games = new Dictionary<string,GameData>();
            PlayerData leader = new PlayerData("Kamel");
            List<PlayerData> participants = new List<PlayerData>();
            PlayerData part1 = new PlayerData("John Doe");
            participants.Add(part1);
            _games.Add("s234dfgh34",new GameData("s234dfgh34","game1",leader,participants));
        }

        public static LobbyData Instance
        {
            get {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LobbyData();
                    }
                    return instance;
                }
            }
        }

        public Dictionary<string,GameData> Games
        {
            get { return _games; }
            set { this._games = value; }
        }
        public Dictionary<string,BoardData> Boards
        {
            get { return _boards; }
            set { this._boards = value; }
        }

        public void AddGame(GameData game)
        {
            Guid guid = Guid.NewGuid();
            string gameid = guid.ToString();
            game.Id = gameid;
            this._games.Add(gameid,game);
        }
    }
}
