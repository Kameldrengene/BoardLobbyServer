using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardLobbyServer.Pages
{
    public class GameListModel : PageModel
    {
        public string Message { get; private set; } = "Pagemodel";
        public bool logged { get; private set; } = false;
        public IActionResult OnGet()
        {
            Message += $" Server time is { DateTime.Now }";

            if (HttpContext.Session.GetString("LoggedIn") != null)
            {
                logged = true;
            }
            return null;

        }
    }
}
