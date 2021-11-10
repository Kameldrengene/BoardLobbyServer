using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class FinishField : Field
    {
        public FinishField(PieceColor quadrant, int pos, Board board) : base(quadrant, pos, board)
        {
            
        }

        public override Field NextField(Piece piece)
        {
            if (this.nextField != null)
                return this.nextField;
            else
                return null;

        }

        public override void OnLand(Piece piece)
        {
            Console.WriteLine("Landed on FinishField!");

            //TODO remove piece from play and update the board


        }
    }
}
