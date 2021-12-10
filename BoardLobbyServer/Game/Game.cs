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
             boardData = new BoardData(board);
        }

        public void startGame(List<PlayerData> ps)
        {
            this.players = ps;
            board.createBoard();
        }

        public void turn(int choice, int roll)
        {
            if(choice > 0) //actual choice
            {
                board.tryToMove(boardData.currentPlayer, choice, roll);
            }
            boardData.readyNextTurn(board);
        }


        public bool isWon()
        {
            bool won = false;
            for (int i = 0; i < players.Count; i++)
            {
                if (board.piecesDone[i].Count == 4)
                {
                    won = true;
                    break;
                }
            }
            return won;
        }


    }
}
