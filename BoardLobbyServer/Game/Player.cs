using BoardLobbyServer.Game.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game
{
    public abstract class Player
    {
        int UnusedPieces;
        protected Player(string name)
        {
            this.Name = name;    
        }

        public string Name { get; set; }
        public List<Piece> pieces { get; set; }
        public bool HasWon { get; set; }

        public int Un { get; set; }
        public abstract void initPieces(Field field);
       
    } 
}
