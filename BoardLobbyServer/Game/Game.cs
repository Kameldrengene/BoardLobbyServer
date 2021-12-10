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
        private PieceColor playerPointer = 0; //points to the current player in players (0,1,2,3 corresponding to PieceColor in Piece.cs) 

        public void startGame(List<PlayerData> ps)
        {
            this.players = ps;
            board.createBoard();
        }

        public void turn(int choice, int roll)
        {
            if(choice > 0) //actual choice
            {
                board.tryToMove(playerPointer, choice, roll);
            }
            playerPointer = (PieceColor)(((int)playerPointer + 1) % players.Count);
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
