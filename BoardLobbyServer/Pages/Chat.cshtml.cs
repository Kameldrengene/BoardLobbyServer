using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardLobbyServer.Model;
using BoardLobbyServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardLobbyServer.Pages
{
    public class ChatModel : PageModel
    {
        private readonly AdminService _adminService;
        public ChatModel(AdminService adminService)
        {
            _adminService = adminService;
        }
        public string adminName { get; private set; } = "";
        public bool logged { get; private set; } = false;
        public string adminAvatar { get; set; } = "";
        public void OnGet()
        {
            if (HttpContext.Session.GetString("LoggedIn") != null)
            {
                logged = true;
                adminName = HttpContext.Session.GetString("LoggedIn");

                Admin admin = _adminService.GetByName(adminName);
                adminAvatar = admin.Avatar;

            }


        }
    }
}
