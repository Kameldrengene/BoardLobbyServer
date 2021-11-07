using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardLobbyServer.Pages
{
    public class ChatModel : PageModel
    {
        public string adminName { get; private set; } = "";
        public bool logged { get; private set; } = false;
        public void OnGet()
        {
            if (HttpContext.Session.GetString("LoggedIn") != null)
            {
                logged = true;
                adminName = HttpContext.Session.GetString("LoggedIn");
            }


        }
    }
}
