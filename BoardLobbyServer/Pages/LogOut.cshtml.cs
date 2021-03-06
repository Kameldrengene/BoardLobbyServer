using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardLobbyServer.Pages
{
    public class LogOutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("LoggedIn");
            return Redirect("Index");
        }
    }
}
