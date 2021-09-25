using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.DTO
{
    public class Lobby
    {
        private string _playername;
        private string _lobbyname;
        private int _nrOfPlayers;

        public Lobby(string playername,string lobbyname, int nrOfPlayers)
        {
            _playername = playername;
            _lobbyname = lobbyname;
            _nrOfPlayers = nrOfPlayers;
        }

        public string Playername
        {
            get { return _playername; }
            set { this._playername = value; }
        }

        public string Lobbyname
        {
            get { return _lobbyname; }
            set { this._lobbyname = value; }
        }

        public int NrOfPlayers
        {
            get { return _nrOfPlayers; }
            set { this._nrOfPlayers = value; }
        }
    }

    
}
