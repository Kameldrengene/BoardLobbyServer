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
            if(LobbyData.Instance.Games.TryGetValue(id, out game))
            {
                await Clients.Caller.SendAsync("GetGame",game);
            }
        }

        public async Task LaunchGame(string id)
        {
            GameData game;
            if (LobbyData.Instance.Games.TryGetValue(id, out game))
            {
                await Clients.Group(id).SendAsync("LaunchGame", game);
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
