using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class StarField : Field
    {
        public StarField(PieceColor quadrant, int pos, Board board) : base(quadrant, pos, board)
        {
            
        }

        public override Field NextField(Piece piece)
        {
            if (this.nextField != null)
                return this.nextField;
            else
                throw new NotImplementedException();
        }

        public override void OnLand(Piece piece)
        {
            Console.WriteLine("Landed on Starfield!");

            //Finding the next star/entrance field

            Field nextStarField = this.nextField;
            while (!(nextStarField is StarField || nextStarField is EntranceField))
            {
                nextStarField = nextStarField.NextField(piece);
            }

            if (this.pieces.Count == 0) //no pieces yet
            {
                // handle the jump on the next field
                this.handleJump(piece, nextStarField);
            }

            else if (this.pieces.Count == 1) //one piece on field
            {
                if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
                {
                    board.sendPieceHome(this.pieces[0]);
                }
                // handle the jump on the next field
                this.handleJump(piece, nextStarField);

            }

            else //2 or more pieces
            {
                if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
                {
                    // The moving piece is sent home
                    board.sendPieceHome(piece);
                }
                else // The pieces are all the same color
                {
                    // handle the jump on the next field
                    this.handleJump(piece, nextStarField);
                }
            }
        }

        private void handleJump(Piece piece, Field nextStarField)
        {
            if (nextStarField.getPieces().Count == 0) //no pieces yet
            {
                nextStarField.getPieces().Add(piece); // simply adds the piece to the field
                piece.field = nextStarField;
            }

            else if (nextStarField.getPieces().Count == 1) //one piece on field
            {
                if (nextStarField.getPieces()[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
                {
                    board.sendPieceHome(nextStarField.getPieces()[0]);// Remove piece and send it to start
                }
                nextStarField.getPieces().Add(piece); //adds the piece to field
                piece.field = nextStarField;

            }

            else //2 or more pieces
            {
                if (nextStarField.getPieces()[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
                {
                    board.sendPieceHome(piece);
                    // The moving piece is sent home
                    // TODO: add the current piece to the correct start
                }
                else // The pieces are all the same color
                {
                    nextStarField.getPieces().Add(piece); // adds the piece to field
                    piece.field = nextStarField;
                }
            }
        }

        public override void OnMoveOut(Piece piece)
        {
            this.pieces.Remove(piece);
        }
    }
}
