using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class NormalField : Field
    {
        public NormalField(PieceColor quadrant, int pos, Board board) : base(quadrant, pos, board)
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
            Console.WriteLine("Landed on NormalField!");

            if (this.pieces.Count == 0) //no pieces yet
            {
                this.pieces.Add(piece); // simply adds the piece to the field
                piece.field = this;
            }

            else if (this.pieces.Count == 1) //one piece on field
            {
                if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
                {
                    Piece deadPiece = this.pieces[0];
                    this.board.sendPieceHome(deadPiece); // Remove piece and send it to start

                }
                this.pieces.Add(piece); //adds the piece to field
                piece.field = this;

            }

            else //2 or more pieces
            {
                if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
                {
                    this.board.sendPieceHome(piece);
                    // The moving piece is sent home
                }
                else // The pieces are all the same color
                {
                    this.pieces.Add(piece); // adds the piece to field
                    piece.field = this;
                }
            }


        }
    }
}
