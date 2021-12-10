using BoardLobbyServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public class BoardData
    {
        private List<PieceData> pieces;
        private PieceColor currentPlayer;

        public BoardData(Board board)
        {
            currentPlayer = PieceColor.blue;
            readyNextTurn(board);
        }


        public void readyNextTurn(Board board)
        {
            currentPlayer = (PieceColor)((((int)currentPlayer)+1)%4); //Should be %amount of players, but hardcoded to 4 for now
            generateList(board);
        }

        private void generateList(Board board)
        {
            pieces = new List<PieceData>();
            for(int i = 0; i < 4; i++)
            {
                pieces.AddRange(board.getColorPieceData((PieceColor)i));
            }
        }
    }


}
