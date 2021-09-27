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
        private List<GameData> _games;

        LobbyData()
        {
            _games = new List<GameData>();
            PlayerData leader = new PlayerData("Kamel");
            List<PlayerData> participants = new List<PlayerData>();
            PlayerData part1 = new PlayerData("John Doe");
            participants.Add(part1);
            _games.Add(new GameData("game1",leader,participants));
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

        public List<GameData> Games
        {
            get { return _games; }
            set { this._games = value; }
        }

        public void AddGame(GameData game)
        {
            this._games.Add(game);
        }
    }
}
