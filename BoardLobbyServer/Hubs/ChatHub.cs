using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardLobbyServer.Model;
using Microsoft.AspNetCore.SignalR;

namespace BoardLobbyServer.Hubs
{
    public class ChatHub : Hub
    {
        private List<Admin> admins = new List<Admin>();
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            
            return base.OnDisconnectedAsync(exception);
        }

        public async Task BroadcastStatus(string id,string adminname, string img)
        {
            
            
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", user, message);
            await Clients.Caller.SendAsync("ReceiveOwnMessage", user, message);
        }

    }
}
