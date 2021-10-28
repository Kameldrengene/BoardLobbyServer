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
    public class SignInModel : PageModel
    {
        private readonly AdminService _adminService;
        public string email { get; set; } = "";
        public SignInModel(AdminService adminService)
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
                Admin admin = _adminService.GetByName(emailAddress);             
                if (password.Equals(admin.Password))
                {
                   
                    HttpContext.Session.SetString("LoggedIn", "yes");
                    return Redirect("Index");
                }
            }catch(Exception e)
            {
                email = "User do not exits";
                
            }
            return null;
        }

        public IActionResult OnPostSignup()
        {
            return Redirect("SignUp");
        }
    }
}
