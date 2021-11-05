using BoardLobbyServer.Game.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Players
{
    //todo make class singleton
    public class GreenPlayer : Player
    {
        public GreenPlayer(string name) : base(name)
        {
            
        }

        //todo correct positions
        public override void initPieces(Field field)
        {
            this.pieces = new List<Piece>();
            Piece piece1 = new Piece(Color.GREEN, this, field);
            Piece piece2 = new Piece(Color.GREEN, this, field);
            Piece piece3 = new Piece(Color.GREEN, this, field);
            Piece piece4 = new Piece(Color.GREEN, this, field);
            pieces.Add(piece1);
            pieces.Add(piece2);
            pieces.Add(piece3);
            pieces.Add(piece4);
        }
    }
}
