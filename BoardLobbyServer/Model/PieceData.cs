using BoardLobbyServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public class PieceData
    {
        private PieceColor _pieceColor;
        private int _pieceID;
        private int _fieldID;
        private bool _isInPlay;
        private bool _isDone;

        public PieceData(PieceColor pc, int pID, int fID, bool inPlay, bool Done)
        {
            this._pieceColor = pc;
            this._pieceID = pID;
            this._fieldID = fID;
            this._isInPlay = inPlay;
            this._isDone = Done;
        }

        public PieceColor PieceColor { get => _pieceColor; set => _pieceColor = value; }
        public int PieceID { get => _pieceID; set => _pieceID = value; }
        public int FieldID { get => _fieldID; set => _fieldID = value; }
        public bool IsInPlay { get => _isInPlay; set => _isInPlay = value; }
        public bool IsDone { get => _isDone; set => _isDone = value; }
    }

}
