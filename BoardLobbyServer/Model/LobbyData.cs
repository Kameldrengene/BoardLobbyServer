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
        private Dictionary<string,GameData> _gameData;
        private Dictionary<string,BoardLobbyServer.Game.Game> _games;

        LobbyData()
        {
            _gameData = new Dictionary<string,GameData>();
            PlayerData leader = new PlayerData("Kamel");
            List<PlayerData> participants = new List<PlayerData>();
            PlayerData part1 = new PlayerData("John Doe");
            participants.Add(part1);
            _gameData.Add("s234dfgh34",new GameData("s234dfgh34","game1",leader,participants));
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

        public Dictionary<string,GameData> GameData
        {
            get { return _gameData; }
            set { this._gameData = value; }
        }
        public Dictionary<string, BoardLobbyServer.Game.Game> Games
        {
            get { return _games; }
            set { this._games = value; }
        }

        public void AddGame(GameData gameData)
        {
            Guid guid = Guid.NewGuid();
            string gameid = guid.ToString();
            gameData.Id = gameid;
            this._gameData.Add(gameid,gameData);
            /*BoardData  = new BoardData(new Game.Board());
            this._games.Add(gameid, _games);*/
        }
    }
}
