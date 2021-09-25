using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardLobbyServer.DTO;

namespace BoardLobbyServer.Model
{
    public sealed class LobbyData
    {
        private static readonly object padlock = new object();
        private List<Lobby> _games;
        private static LobbyData instance = null;

        LobbyData()
        {
            _games = new List<Lobby>();
            _games.Add(new Lobby("volkan","hygge",2));
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

        public List<Lobby> Games
        {
            get { return _games; }
            set { this._games = value; }
        }

        public void AddGame(Lobby game)
        {
            this._games.Add(game);
        }
    }
}
