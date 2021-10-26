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
        public void OnGet()
        {
        }
        [BindProperty]
        public string GameId { get; set; }
        public void OnPost()
        {
            // do something with gameId
        }
    }
}
