using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class SafeHomeField : Field
    {
        public SafeHomeField(PieceColor quadrant, int pos, Board board) : base(quadrant, pos, board)
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
            Console.WriteLine("Landed on SafeHomeField!");

            if (this.pieces.Count == 0) //no pieces yet
            {
                this.pieces.Add(piece); // simply adds the piece to the field
                piece.field = this;
            }

            else //1 or more pieces
            {
                if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece(s) are not the same color
                {
                    // Checking if the moving piece is of the starting color. if so, it will send any amount of pieces in the field home.
                    // This is because the moving piece is just exiting home. pieces will never land on their SafeHomeField on any other occation than exiting home.
                    if (piece.getPieceColor() == this.quadrant)
                    {
                        // TODO: The stading piece(s) are sent home
                        this.pieces = new List<Piece>(); //set new list to clear out any pieces on the field
                        this.pieces.Add(piece);
                        piece.field = this;
                    }
                    else
                    {
                        // The moving piece is sent home
                        // TODO: add the current piece to the correct start
                    }
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
