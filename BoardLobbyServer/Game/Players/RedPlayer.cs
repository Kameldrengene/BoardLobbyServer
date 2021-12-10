using BoardLobbyServer.Game.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Players
{
    //todo make class singleton
    public class RedPlayer : Player
    {
        public RedPlayer(string name) : base(name)
        {
            
        }

        //todo correct positions
        public override void initPieces(Field field)
        {
            this.pieces = new List<Piece>();
            Piece piece1 = new RedPiece(1); 
            Piece piece2 = new RedPiece(2);
            Piece piece3 = new RedPiece(3);
            Piece piece4 = new RedPiece(4);
            pieces.Add(piece1);
            pieces.Add(piece2);
            pieces.Add(piece3);
            pieces.Add(piece4);
        }
    }
}
