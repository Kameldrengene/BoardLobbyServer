using BoardLobbyServer.Game.Fields;
using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game
{
    public enum PieceColor
    {
        blue = 0,
        yellow = 1,
        green = 2,
        red = 3
    }

    public abstract class Piece
    {
        protected PieceColor pieceColor;
        public Field field { get; set; }
        protected Player owner;
        public int pieceID;

        protected Piece(int id)
        {
            this.pieceID = id;
        }

        public PieceColor getPieceColor()
        {
            return pieceColor;
        }
    }

    public class RedPiece : Piece
    {
        public RedPiece(int id): base(id)
        {
            this.pieceColor = PieceColor.red;
        }
    }


    public class BluePiece : Piece
    {
        public BluePiece(int id) : base(id)
        {
            this.pieceColor = PieceColor.blue;
        }
    }

    public class YellowPiece : Piece
    {
        public YellowPiece(int id) : base(id)
        {
            this.pieceColor = PieceColor.yellow;
        }
    }

    public class GreenPiece : Piece
    {
        public GreenPiece(int id) : base(id)
        {
            this.pieceColor = PieceColor.green;
        }
    }
}
