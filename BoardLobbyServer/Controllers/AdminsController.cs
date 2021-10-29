using BoardLobbyServer.Model;
using BoardLobbyServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardLobbyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController: ControllerBase
    {
        private readonly AdminService _adminService;
        public AdminsController(AdminService adminservice)
        {
            _adminService = adminservice;
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

        [HttpPost]
       public ActionResult<Admin> Create(Admin admin)
        {
            _adminService.Create(admin);

            return CreatedAtRoute("GetAdmin", new { id = admin.Id.ToString() }, admin);
        }

    }
}
