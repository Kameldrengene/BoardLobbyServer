using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public class GameData
    {
        private string _id;
        private string _gameName;
        private PlayerData _leader;
        private List<PlayerData> _participants;

        public GameData()
        {
        }

        public GameData(string id, string gameName, PlayerData leader, List<PlayerData> participants)
        {
            _id = id;
            _gameName = gameName;
            _leader = leader;
            _participants = participants;
        }

        public string GameName
        {
            get { return _gameName; }
            set { this._gameName = value; }
        }
        public string Id
        {
            get { return _id; }
            set { this._id = value; }
        }

        public PlayerData Leader
        {
            get { return _leader; }
            set { this._leader = value; }
        }
        public List<PlayerData> Participants
        {
            get { return _participants; }
            set { this._participants = value; }
        }
    }


}
