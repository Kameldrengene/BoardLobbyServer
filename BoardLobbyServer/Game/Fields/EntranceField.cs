﻿using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public class EntranceField : Field
    {
        private Field startBankfield;

        public void setBankField(Field bank)
        {
            this.startBankfield = bank;
        }

        public EntranceField(PieceColor quadrant, int pos, Board board) : base(quadrant, pos, board)
        {
            
        }

        public override Field NextField(Piece piece) //Should have piece passed? how will it know to send the correct pieces to finish spaces?
        {
            if (piece.getPieceColor() == this.quadrant) return this.startBankfield;
            return this.nextField;

        }

        public override void OnLand(Piece piece)
        {
            Console.WriteLine("Landed on Entrancefield!");

            //Finding the next star/entrance field

            Field nextStarField = this.nextField;
            while (!(nextStarField is StarField || nextStarField is EntranceField))
            {
                nextStarField = nextStarField.NextField(piece);
            }

            // the check for if the piece should not jump is in handleJump()

            if (this.pieces.Count == 0) //no pieces yet
            {
               
                
                // handle the jump on the next field
                this.handleJump(piece, nextStarField);
                
            }

            else if (this.pieces.Count == 1) //one piece on field
            {
                if (this.pieces[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
                {
                    this.pieces.RemoveAt(0); // Remove piece and send it to start
                                             // TODO: add the removed piece to the correct start
                }
                // handle the jump on the next field
                this.handleJump(piece, nextStarField);

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
                    // handle the jump on the next field
                    this.handleJump(piece, nextStarField);
                }
            }
        }

        private void handleJump(Piece piece, Field nextStarField)
        {
            if (piece.getPieceColor() == this.quadrant) //Should not jump
            {
                this.pieces.Add(piece); // Adds the piece to the current field instead
                piece.field = this;
            }

            else if (nextStarField.getPieces().Count == 0) //no pieces yet
            {
                nextStarField.getPieces().Add(piece); // simply adds the piece to the field
                piece.field = nextStarField;
            }

            else if (nextStarField.getPieces().Count == 1) //one piece on field
            {
                if (nextStarField.getPieces()[0].getPieceColor() != piece.getPieceColor()) // if the fields piece is not the same color
                {
                    Piece deadPiece = nextStarField.getPieces()[0];
                    nextStarField.getPieces().RemoveAt(0); // Remove piece and send it to start
                                                           // TODO: add the removed piece to the correct start
                }
                nextStarField.getPieces().Add(piece); //adds the piece to field
                piece.field = nextStarField;

            }

            else //2 or more pieces
            {
                if (nextStarField.getPieces()[0].getPieceColor() != piece.getPieceColor()) // if the fields pieces are not the same color
                {
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
    }
}
