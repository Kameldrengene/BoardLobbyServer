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
    public class EditAccountModel : PageModel
    {
        private readonly AdminService _adminService;
        public string log { get; set; } = "";
        public string alert { get; set; } = "";
        private string _adminName;
        public string adminId { get; set; } = "";
        public string adminName { get; set; } = "";
        public string adminPassword { get; set; } = "";

        public EditAccountModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        public void OnGet()
        {

            if (HttpContext.Session.GetString("LoggedIn") == null)
            {
                alert = "Please log in.";
            }
            try
            {
                _adminName = HttpContext.Session.GetString("LoggedIn");
                Admin admin = _adminService.GetByName(_adminName);
                adminId = admin.Id;
                adminName = admin.Name;
                adminPassword = "****************";
                    
            }catch(Exception e)
            {
                log = e.Message;
            }
        }
        public IActionResult OnPost()
        {
            
            if(this.Request.Form.Keys.Contains("updatebutton"))
            {
                try
                {
                    var emailAddress = Request.Form["emailaddress"];
                    var password = Request.Form["password"];
                    var id = Request.Form["adminId"];
                    Admin admin = new Admin();
                    admin.Id = adminId;
                    admin.Name = adminName;
                    admin.Password = adminPassword;
                    _adminService.Update(adminId, admin);
                }
                catch (Exception e)
                {
                    alert = e.Message;

                }
            }
            if (this.Request.Form.Keys.Contains("deletebutton"))
            {
                alert = "Delete";
                try
                {
               
                    _adminService.Remove(adminId);
                }
                catch (Exception e)
                {
                    alert = e.Message;

                }
            }

                return null;
        }
    }
}
