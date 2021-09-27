using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public sealed class PlayerData
    {

        private string _name;

        public PlayerData(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get { return _name; }
            set { this._name = value; }
        }
    }
}
