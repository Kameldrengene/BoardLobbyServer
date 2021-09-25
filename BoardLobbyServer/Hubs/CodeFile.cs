using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BoardLobbyServer.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task CreateLobby(string user, string lobbyName)
        {
            Lobby lobby = Lobby.Instance;
            lobby.AddGame(lobbyName);
            await Clients.All.SendAsync("ReceiveLobby", user, lobbyName);

        }
        // Når man joiner serveren så må tidligere tilsluttede bruger skal ikke have listen igen
        public async Task getLobbies()
        {
            Lobby lobby = Lobby.Instance;
            string lobbies = JsonSerializer.Serialize(lobby.Games);
            await Clients.All.SendAsync("ReceiveLobbies", lobbies);

        }


    }
}