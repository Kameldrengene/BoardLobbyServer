using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardLobbyServer.Pages
{
    public class GameModel : PageModel
    {
        public string Message { get; private set; } = "PageModel in C#";
        public string GameId { get; set; } = "0";
        public void OnGet()
        {
 
        }
        public void OnPost()
        {

            if (Request.Form.ContainsKey("gameid"))
                GameId = Request.Form["gameid"];
        }
    }
}
