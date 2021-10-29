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
    public class SignUpModel : PageModel
    {
        private readonly AdminService _adminService;
        public string log { get; set; } = "";

        public SignUpModel(AdminService adminService)
        {
            _adminService = adminService;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(string emailAddress, string password)
        {
            try
            {
                Admin admin = new Admin();
                admin.Name = emailAddress;
                admin.Password = password;
                _adminService.Create(admin);
                HttpContext.Session.SetString("LoggedIn", admin.Name);
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
