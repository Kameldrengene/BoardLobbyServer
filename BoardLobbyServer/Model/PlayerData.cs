using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public sealed class PlayerData
    {
        private static readonly object playerlock = new object();
        private static PlayerData instance = null;
        private string _name;

        PlayerData()
        {
        }
        public static PlayerData Instance
        {
            get
            {
                lock (playerlock)
                {
                    if(instance == null)
                    {
                        instance = new PlayerData();
                    }
                    return instance;
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set { this._name = value; }
        }
    }
}
