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
    public class PlayerSignUpModel : PageModel
    {

        private readonly PlayerService _playerservice;
        public string log { get; set; } = "";

        public PlayerSignUpModel(PlayerService playerService)
        {
            _playerservice = playerService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string emailAddress, string password)
        {
            try
            {
                PlayerData player = new PlayerData();
                player.Name = emailAddress;
                player.password = password;
                _playerservice.Create(player);
                HttpContext.Session.SetString("LoggedIn", player.Name);
                return Redirect("Index");
            }
            catch (Exception e)
            {

                log = e.Message;
            }
            return null;
        }
    }
}
