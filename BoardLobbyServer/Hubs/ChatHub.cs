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
        private static Dictionary<string,OnlineAdmin> admins = new Dictionary<string, OnlineAdmin>();
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            admins.Remove(Context.ConnectionId);
            await Clients.Others.SendAsync("Disconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        //public async Task RemoveDisconnected(string id)
        //{
        //    await Task.Run(() =>
        //    {
        //        admins.Remove(id);
        //    });
        //}

        public async Task BroadcastStatus(string id,string adminname, string img)
        {
            OnlineAdmin online = new OnlineAdmin{ _id = id, _adminname = adminname, _img = img };
            admins.Add(id,online);
            Console.WriteLine(admins.Count);
            await Clients.Caller.SendAsync("ReceiveOnlineAdmins", admins.ToArray());
            await Clients.Others.SendAsync("UpdateOnlineAdmins", online);
        }
        public async Task SendMessage(string user, string message, string img)
        {
            await Clients.Others.SendAsync("ReceiveMessage", user, message,img);
            await Clients.Caller.SendAsync("ReceiveOwnMessage", user, message,img);
        }

    }
    public class OnlineAdmin
    {
        public string _id { get; set; }
        public string _adminname { get; set;}
        public string _img { get; set;}
    }
}
