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
    }
}
