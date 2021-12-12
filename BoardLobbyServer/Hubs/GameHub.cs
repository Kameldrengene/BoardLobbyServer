using BoardLobbyServer.Game;
using BoardLobbyServer.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Hubs
{
    public class GameHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task GetGame(string id)
        {
            GameData game;
            if(LobbyData.Instance.GameData.TryGetValue(id, out game))
            {
                await Clients.Group(id).SendAsync("GetGame",game);
            }
        }

        public async Task LaunchGame(string id)
        {
            GameData game;
            if (LobbyData.Instance.GameData.TryGetValue(id, out game)) 
            {
                await Clients.Group(id).SendAsync("LaunchGame", game);
            }
        }

        public async Task UpdateGame(string id,string r, string lm, string pID) 
        {
            //Should recieve game id, roll and legal moves, then send board state to other players on update
            GameData game;
            if (LobbyData.Instance.GameData.TryGetValue(id, out game))
            {
                //This is the turn of a game!

                //game.Game is BoardData
                int roll = int.Parse(r);
                int legalMoves = int.Parse(lm);
                int pieceID = int.Parse(pID);
                PieceColor pieceColor = game.Game.CurrentPlayer;

                Board tmpBoard = new Board(game.Game);

                if (legalMoves > 0)
                    tmpBoard.tryToMove(pieceColor, pieceID, roll);

                pieceColor = (PieceColor)(((int)pieceColor + 1) % 4);

                BoardDataFactory fac = new BoardDataFactory(); //Updating to next players turn

                game.Game = fac.generateBoardData(tmpBoard,pieceColor,roll);

                await Clients.Group(id).SendAsync("UpdateGame", game); //Update game for everyone
            }
        }


        // Tænker på at groupName bliver game id. Medlemmer tilføjes når de joiner en game room.
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            // Kald til medlemmer i gruppen (den metode der skal køre på klienten)
           // await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        // Fjern medlemmer fra gruppen når spillet er slut.
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

           // await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
