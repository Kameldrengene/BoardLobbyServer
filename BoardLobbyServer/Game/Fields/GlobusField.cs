using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class GlobusField : Field
    {
        
        public GlobusField(PieceColor quadrant, int pos, Board board) : base(quadrant, pos, board)
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
            Console.WriteLine("Landed on GlobusField!");


            if (this.pieces.Count == 0) //no pieces yet
            {
                this.pieces.Add(piece); // simply adds the piece to the field
                piece.field = this;
            }

            else //1 or more pieces
            {
                if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece(s) are not the same color
                {
                    board.sendPieceHome(piece);
                }
                else // The pieces are all the same color
                {
                    this.pieces.Add(piece); // adds the piece to field
                    piece.field = this;
                }
            }
        }

        public override void OnMoveOut(Piece piece)
        {
            this.pieces.Remove(piece);
        }
    }
}
