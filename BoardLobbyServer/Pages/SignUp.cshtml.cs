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
        public bool master { get; private set; } = false;
        public bool firstTime { get; private set; } = false;
        private readonly AdminService _adminService;
        public string log { get; set; } = "";

        public SignUpModel(AdminService adminService)
        {
            _adminService = adminService;
        }
        public void OnGet()
        {
            if (_adminService.Get().Count == 0)
            {
                firstTime = true;
            }
            else
            {
                if (HttpContext.Session.GetString("LoggedIn") != null)
                {
                    Admin admin = _adminService.Get(HttpContext.Session.GetString("LoggedIn"));
                    if (admin.AdminType == Admin.Type.Master)
                    {
                        master = true;
                    }
                }
            }

        }
        public IActionResult OnPost(string emailAddress, string password, string imgsrc)
        {
            try
            {
                Admin admin = new Admin();
                admin.Name = emailAddress;
                admin.Password = password;
                admin.Avatar = imgsrc;
                if (_adminService.Get().Count == 0)
                {
                    admin.AdminType = Admin.Type.Master;
                }
                _adminService.Create(admin);
                string id = _adminService.GetByName(admin.Name).Id;
                HttpContext.Session.SetString("LoggedIn", id);
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
