using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardLobbyServer.Game;
using BoardLobbyServer.Model;
using BoardLobbyServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardLobbyServer.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public PlayerController(PlayerService playerService, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _playerService = playerService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet]
        public ActionResult<List<PlayerData>> Get() =>_playerService.Get();

        [HttpGet("{id:length(24)}", Name = "GetPlayer")]
        public ActionResult<PlayerData> Get(string id)
        {
            var player = _playerService.Get(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<PlayerData> Create(PlayerData player)
        {
            _playerService.Create(player);
            var token = _jwtAuthenticationManager.Authenticate(player.Name);
            if (token == null) return Unauthorized();
            return Created("/api/player", token);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            PlayerData player = _playerService.Verify(userCredentials.Username, userCredentials.Password);
            if(player == null) return Unauthorized("wrong credentials: " + userCredentials.Username);
            var token = _jwtAuthenticationManager.Authenticate(userCredentials.Username);
            if (token == null) return Unauthorized();
            return Ok(token);
        }
    }
}
