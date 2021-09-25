using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BoardLobbyServer.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using BoardLobbyServer.DTO;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task CreateLobby(string user, string lobbyName)
        {
            LobbyData lobby = LobbyData.Instance;
            lobby.AddGame(new Lobby(user, lobbyName, 3));
            await Clients.All.SendAsync("ReceiveLobby", user, lobbyName);
            
        }
       
    }
}