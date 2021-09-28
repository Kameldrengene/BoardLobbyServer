using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BoardLobbyServer.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace SignalRChat.Hubs
{
    public class LobbyHub : Hub
    {
        public async Task CreateLobby(string leaderName, string gameName)
        {
            GameData game = new GameData();
            game.Leader = new PlayerData(leaderName);
            game.GameName = gameName;
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
            List<GameData> lobby = new List<GameData>(LobbyData.Instance.Games.Values);
            string lobbies = JsonSerializer.Serialize(lobby);
            await Clients.All.SendAsync("ReceiveLobbies", lobbies);
        }
        public async Task MonitorGame(string gameId)
        {
            GameData result;
            if(LobbyData.Instance.Games.TryGetValue(gameId, out result))
            {
                await Clients.Caller.SendAsync("MonitorGame", result);
            }
            
            

        }

    }
}