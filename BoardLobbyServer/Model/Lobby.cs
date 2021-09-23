using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public sealed class Lobby
    {
        private static readonly object padlock = new object();
        private List<String> _games;
        private static Lobby instance = null;

        Lobby()
        {
            _games = new List<string>();
            string game1 = "HEj";
            _games.Add(game1);
        }

        public static Lobby Instance
        {
            get {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Lobby();
                    }
                    return instance;
                }
            }
        }

        public List<String> Games
        {
            get { return _games; }
            set { this._games = value; }
        }

        public void AddGame(String game)
        {
            this._games.Add(game);
        }
    }
}
