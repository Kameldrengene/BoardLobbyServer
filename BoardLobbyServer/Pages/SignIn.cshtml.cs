using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardLobbyServer.Model;
using BoardLobbyServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BoardLobbyServer.Pages
{
    public class SignInModel : PageModel
    {
        private readonly AdminService _adminService;
        public string log { get; set; } = "";
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
                Admin admin = _adminService.Verify(emailAddress,password);             
                if (admin!=null)
                {
                    var adminValues = new {
                        Id = admin.Id,
                        Name = admin.Name,
                        AdminType = admin.AdminType,
                        Avatar = admin.Avatar
                    };

                    string adminJson = JsonConvert.SerializeObject(adminValues);
                    Console.WriteLine(adminJson);
                    HttpContext.Session.SetString("LoggedIn", adminJson);
                    return Redirect("Index");
                }
            }catch(Exception e)
            {
                log = e.Message;
                
            }
            return null;
        }

        public IActionResult OnPostSignup()
        {
            return Redirect("SignUp");
        }

        public IActionResult OnPostPlayerSignup()
        {
            return Redirect("PlayerSignUp");
        }
    }
}
