using BoardLobbyServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public class BoardData
    {
        private List<PieceData> _pieces;
        private PieceColor _currentPlayer;
        private bool _isWon;
        private int _roll;

        public BoardData(List<PieceData> ps, PieceColor currentPlayer, bool isWon, int roll)
        {
            this._pieces = ps;
            this._currentPlayer = currentPlayer;
            this._isWon = isWon;
            this._roll = roll;
        }
        
        public List<PieceData> Pieces
        {
            get { return _pieces; }
            set { this._pieces = value; }
        }

        public PieceColor CurrentPlayer
        {
            get { return _currentPlayer; }
            set { this._currentPlayer = value; }
        }

        public bool IsWon
        {
            get { return _isWon; }
            set { this._isWon = value; }
        }
        public int Roll
        {
            get { return _roll; }
            set { this._roll = value; }
        }

    }
}
