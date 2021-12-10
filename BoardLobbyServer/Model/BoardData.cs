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

        public BoardData(Board board)
        {
            _currentPlayer = PieceColor.blue;
            readyNextTurn(board);
        }


        public void readyNextTurn(Board board)
        {
            _currentPlayer = (PieceColor)((((int)_currentPlayer)+1)%4); //Should be %amount of players, but hardcoded to 4 for now
            _isWon = board.isWon();
            generateList(board);
        }

        private void generateList(Board board)
        {
            _pieces = new List<PieceData>();
            for(int i = 0; i < 4; i++)
            {
                _pieces.AddRange(board.getColorPieceData((PieceColor)i));
            }
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
