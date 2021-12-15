using BoardLobbyServer.Exceptions;
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
            try
            {
                var admin = _adminService.Get(id);
                return admin;
            }catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Admin adminIn)
        {
            try 
            {
                _adminService.Update(id, adminIn);
                return Accepted(adminIn);
            }catch (InputIsNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _adminService.Remove(id);
                return Accepted(id);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
       public ActionResult<Admin> Create(Admin admin)
        {
            try
            {
                _adminService.Create(admin);
                var token = _jwtAuthenticationManager.Authenticate(admin.Name);

                if (token == null)
                    return Unauthorized();

                return Created("/api/Admins", token);
            }
            catch (InputIsNotValidException ex)
            {       
                return ValidationProblem(ex.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCred)
        {
            try
            {
                Admin admin = _adminService.Verify(userCred.Username, userCred.Password);
            }catch (ResourceNotFoundException ex)
            {          
                return Unauthorized(ex.Message);
            }catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }

            var token = _jwtAuthenticationManager.Authenticate(userCred.Username);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

    }
}
