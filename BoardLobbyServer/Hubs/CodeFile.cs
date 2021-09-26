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
            string playername = PlayerData.Instance.Name;
            lobby.AddGame(new Lobby(playername, lobbyName, 3));
            await Clients.All.SendAsync("ReceiveLobby", playername, lobbyName);
            
        }
        public async Task CreatePlayer(string user)
        {
            PlayerData player = PlayerData.Instance;
            player.Name = user;
            await Clients.Caller.SendAsync("CreatePlayer", player);

        }

    }
}