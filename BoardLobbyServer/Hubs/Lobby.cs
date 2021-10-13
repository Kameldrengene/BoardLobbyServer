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
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public async Task CreateLobby(string leaderName, string gameName)
        {
            GameData game = new GameData();
            game.Leader = new PlayerData(leaderName);
            game.GameName = gameName;
            LobbyData.Instance.AddGame(game);
            await Clients.All.SendAsync("ReceiveGame", game);
            await Clients.Caller.SendAsync("EnterGame", game);
        }

        public async Task JoinLobby(GameData game, PlayerData player)
        {
            game.Participants.Add(player);
            await Clients.Caller.SendAsync("AddedPlayer", game);
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
            await Clients.Caller.SendAsync("ReceiveLobbies", lobbies);
        }

        public async Task getBareLobbies()
        {
            List<GameData> lobby = new List<GameData>(LobbyData.Instance.Games.Values);
            await Clients.Caller.SendAsync("ReceiveLobbies", lobby);
        }

        public async Task addParticipant(string gameId, string playerName)
        {
            GameData result;
            if (LobbyData.Instance.Games.TryGetValue(gameId, out result))
            {
                PlayerData playerData = new PlayerData(playerName);
                result.Participants.Add(playerData);
                List<PlayerData> participants = new List<PlayerData>(result.Participants);
                string jparticipants = JsonSerializer.Serialize(participants);
                await Clients.All.SendAsync("RecieveParticipants", jparticipants);
            }
            
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