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
            Stats.playerList.Add(Context.ConnectionId);
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Stats.playerList.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task CreateLobby(string leaderName, string gameName)
        {
            GameData game = new GameData();
            game.Leader = new PlayerData(leaderName);
            game.GameName = gameName;
            LobbyData.Instance.AddGame(game);
            Stats.gameHistory.Add(gameName);
            await Clients.All.SendAsync("ReceiveGame", game);
            await Clients.Caller.SendAsync("EnterGame", game);
        }

        public async Task JoinLobby(GameData game, PlayerData player)
        {
            game.Participants.Add(player);

            //temporary logic of changing status
            if (game.Participants.Count == 4) game.Status = "started"; 
            await Clients.All.SendAsync("AddedPlayer", game);
        }
        public async Task CreatePlayer(string playerName)
        {
            PlayerData player = new PlayerData(playerName);
            await Clients.Caller.SendAsync("CreatePlayer", player);

        }
        public async Task getLobbies()
        {
            List<GameData> lobby = new List<GameData>(LobbyData.Instance.GameData.Values);
            string lobbies = JsonSerializer.Serialize(lobby);
            await Clients.Caller.SendAsync("ReceiveLobbies", lobbies);
        }

        public async Task getBareLobbies()
        {
            List<GameData> lobby = new List<GameData>(LobbyData.Instance.GameData.Values);
            await Clients.Caller.SendAsync("ReceiveLobbies", lobby);
        }

       

        public async Task addParticipant(string gameId, string playerName)
        {
            GameData result;
            if (LobbyData.Instance.GameData.TryGetValue(gameId, out result))
            {
                PlayerData playerData = new PlayerData(playerName);
                result.Participants.Add(playerData);
                List<PlayerData> participants = new List<PlayerData>(result.Participants);
               
                if (result.Participants.Count == 4) result.Status = "started";
                await Clients.All.SendAsync("RecieveParticipants", participants);
                await Clients.All.SendAsync("RecieveUpdatedGame", result);
            }
            
        }

        public async Task RemoveParticipant(string gameId, string playerName)
        {
            GameData result;
            if (LobbyData.Instance.GameData.TryGetValue(gameId, out result))
            {
                
                for (int i = 0; i < result.Participants.Count; i++)
                {
                    if (result.Participants[i].Name.Contains(playerName))
                    {
                        result.Participants.RemoveAt(i);
                    }
                       
                }
                List<PlayerData> participants = new List<PlayerData>(result.Participants);

                if (result.Participants.Count == 4) result.Status = "started";
                await Clients.All.SendAsync("RecieveParticipants", participants);
                await Clients.All.SendAsync("RecieveUpdatedGame", result);
            }

        }

        public async Task MonitorGame(string gameId)
        {
            GameData result;
            if(LobbyData.Instance.GameData.TryGetValue(gameId, out result))
            {
                await Clients.Caller.SendAsync("MonitorGame", result);
            }
            
        }
        public async Task deleteGame(string gameId)
        {
           GameData tempData;
            if (LobbyData.Instance.GameData.TryGetValue(gameId, out tempData))
            {
                LobbyData.Instance.Games.Remove(gameId);
                await Clients.All.SendAsync("RemoveGame", tempData);
            }
        }

    }
    public static class Stats
    {
        public static HashSet<string> playerList = new HashSet<string>();
        public static HashSet<string> gameHistory = new HashSet<string>();
        public static HashSet<string> gamesLunched = new HashSet<string>();
    }
}