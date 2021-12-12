using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game.Fields
{
    public abstract class Field
    {
        
        public Field nextField { get; set; }
        protected List<Piece> pieces = new List<Piece>();
        protected PieceColor quadrant;  // Must be same as pieces color system
        protected int pos;
        protected Board board;
        public Field(PieceColor q, int p, Board b)
        {
            this.quadrant = q;
            this.pos = p;
            this.board = b;
        }

        public int getID()
        {
            return pos;
        }

        public List<Piece> getPieces() { return this.pieces; }

        public PieceColor getQuadrant() { return this.quadrant; }
        public abstract void OnMoveOut(Piece piece);
        public abstract void OnLand(Piece piece);
        public abstract Field NextField(Piece piece);


    }
}
