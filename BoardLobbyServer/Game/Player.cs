using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game
{
    public abstract class Player
    {
        protected Player(string name)
        {
            this.Name = name;    
        }

        public string Name { get; set; }
        public List<Piece> pieces { get; set; }
        public bool HasWon { get; set; }

        public abstract void initPieces();
       
    } 
}
