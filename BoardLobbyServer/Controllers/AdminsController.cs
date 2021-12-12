using BoardLobbyServer.Model;
using BoardLobbyServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController: ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public AdminsController(AdminService adminservice, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _adminService = adminservice;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet]
        public ActionResult<List<Admin>> Get() =>
            _adminService.Get();

        [HttpGet("{id:length(24)}", Name = "GetAdmin")]
        public ActionResult<Admin> Get(string id)
        {
            var admin = _adminService.Get(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Admin adminIn)
        {
            var admin = _adminService.Get(id);

            if (admin == null)
            {
                return NotFound();
            }

            _adminService.Update(id, adminIn);

            return Accepted(admin);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var admin = _adminService.Get(id);

            if (admin == null)
            {
                return NotFound();
            }

            _adminService.Remove(admin.Id);

            return Accepted(admin);
        }

        [AllowAnonymous]
        [HttpPost]
       public ActionResult<Admin> Create(Admin admin)
        {
            _adminService.Create(admin);

            var token = _jwtAuthenticationManager.Authenticate(admin.Name);

            if (token == null)
                return Unauthorized();

            return Created("/api/Admins", token);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCred)
        {
            Admin admin = _adminService.Verify(userCred.Username,userCred.Password);

            if (admin == null)
            {
                return Unauthorized("Bad credentials: " + userCred.Username + ".");
            }

            var token = _jwtAuthenticationManager.Authenticate(userCred.Username);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

    }
}
