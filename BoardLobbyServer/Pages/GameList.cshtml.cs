using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardLobbyServer.Pages
{
    public class GameListModel : PageModel
    {
        public string Message { get; private set; } = "Pagemodel";
        public void OnGet()
        {
            Message += $" Server time is { DateTime.Now }";
        }
    }
}
