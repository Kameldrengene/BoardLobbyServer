using BoardLobbyServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public class BoardDataFactory
    {
        public BoardData generateBoardData(Board board)
        {
            List<PieceData> pieces = generateList(board);
            return new BoardData(pieces, PieceColor.yellow, board.isWon(), 4);
        }

        private List<PieceData> generateList(Board board)
        {
            List<PieceData> pieces = new List<PieceData>();
            for (int i = 0; i < 4; i++)
            {
                pieces.AddRange(board.getColorPieceData((PieceColor)i));
            }
            return pieces;
        }
    }
    
}
