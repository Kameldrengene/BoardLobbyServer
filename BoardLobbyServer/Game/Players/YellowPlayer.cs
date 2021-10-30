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
            initPieces();
        }

        //todo correct positions
        public override void initPieces()
        {
            this.pieces = new List<Piece>();
            Piece piece1 = new Piece(Color.YELLOW, this, new Home(Color.RED, new Position(1, 1)));
            Piece piece2 = new Piece(Color.YELLOW, this, new Home(Color.RED, new Position(1, 2)));
            Piece piece3 = new Piece(Color.YELLOW, this, new Home(Color.RED, new Position(2, 1)));
            Piece piece4 = new Piece(Color.YELLOW, this, new Home(Color.RED, new Position(2, 2)));
            pieces.Add(piece1);
            pieces.Add(piece2);
            pieces.Add(piece3);
            pieces.Add(piece4);
        }
    }
}
