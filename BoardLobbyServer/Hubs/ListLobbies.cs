using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using BoardLobbyServer.Model;

namespace BoardLobbyServer.Hubs
{
    public class ListLobbiesHub : Hub
    {
        public async Task getLobbies()
        {
            LobbyData lobby = LobbyData.Instance;
            string lobbies = JsonSerializer.Serialize(lobby.Games);
            await Clients.All.SendAsync("ReceiveLobbies", lobbies);
        }
    }
}
