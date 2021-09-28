using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BoardLobbyServer.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SignalRChat.Hubs
{
    public class LobbyHub : Hub
    {
        public async Task CreateLobby(string leaderName, string gameName)
        {
            GameData game = new GameData();
            game.Leader = new PlayerData(leaderName);
            game.GameName = gameName;
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            LobbyData.Instance.AddGame(game);
            await Clients.All.SendAsync("ReceiveGame", game);
            await Clients.Caller.SendAsync("EnterGame", game);
        }
        public async Task CreatePlayer(string playerName)
        {
            PlayerData player = new PlayerData(playerName);
            await Clients.Caller.SendAsync("CreatePlayer", player);

        }
        public async Task getLobbies()
        {
            LobbyData lobby = LobbyData.Instance;
            string lobbies = JsonSerializer.Serialize(lobby.Games);
            await Clients.All.SendAsync("ReceiveLobbies", lobbies);
        }

    }
}