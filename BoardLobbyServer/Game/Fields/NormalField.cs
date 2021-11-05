using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class NormalField : Field
    {
        public NormalField(PieceColor quadrant, int pos) : base(quadrant, pos)
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
            }

            else if (this.pieces.Count == 1) //one piece on field
            {
                if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
                {
                    this.pieces.RemoveAt(0); // Remove piece and send it to start
                                             // TODO: add the removed piece to the correct start
                }
                this.pieces.Add(piece); //adds the piece to field

            }

            else //2 or more pieces
            {
                if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
                {
                    // The moving piece is sent home
                    // TODO: add the current piece to the correct start
                }
                else // The pieces are all the same color
                {
                    this.pieces.Add(piece); // adds the piece to field
                }
            }


        }
    }
}
