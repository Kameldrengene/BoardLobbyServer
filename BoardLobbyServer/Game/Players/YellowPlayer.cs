using BoardLobbyServer.Game.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Players
{
    //todo make class singleton
    public class YellowPlayer : Player
    {
        public YellowPlayer(string name) : base(name)
        {
           
        }

        //todo correct positions
        public override void initPieces(Field field)
        {
            this.pieces = new List<Piece>();
            Piece piece1 = new YellowPiece(1);
            Piece piece2 = new YellowPiece(2);
            Piece piece3 = new YellowPiece(3); 
            Piece piece4 = new YellowPiece(4);
            pieces.Add(piece1);
            pieces.Add(piece2);
            pieces.Add(piece3);
            pieces.Add(piece4);
        }
    }
}
