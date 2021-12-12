using BoardLobbyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Game
{

    //class for keeping track of the game. Controlled by GameHub via playerchoices
    public class Game
    {
        private Board board = new Board();
        private List<PlayerData> players;
        private BoardData boardData;
        public Game()
        {
            BoardDataFactory fac = new BoardDataFactory();
            this.boardData = fac.generateBoardData(board, PieceColor.yellow, 0);
        }

        public Board Board { get => board; set => board = value; }

        public void startGame(List<PlayerData> ps)
        {
            this.players = ps;
            this.board.createBoard();
        }

        public void turn(int choice, int roll)
        {
            if(choice > 0) //actual choice
            {
                board.tryToMove(boardData.CurrentPlayer, choice, roll);
            }
            //boardData.readyNextTurn(board);
        }


        


    }
}
