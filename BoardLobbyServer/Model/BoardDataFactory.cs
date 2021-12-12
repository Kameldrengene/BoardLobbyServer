using BoardLobbyServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoardLobbyServer.Model
{
    public class BoardDataFactory
    {
        public BoardData generateBoardData(Board board, PieceColor pc, int lastRoll)
        {
            List<PieceData> pieces = generateList(board);
            return new BoardData(JsonSerializer.Serialize(pieces), pc, board.isWon(), lastRoll);
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
