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
        public string log { get; private set; } = "";
        public string alert { get; private set; } = "";
        private string _adminName;
        public string adminId { get; private set; } = "";
        public string adminName { get; private set; } = "";
        public string adminPassword { get; private set; } = "";
        public bool logged { get; private set; } = false;

        public EditAccountModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        public void OnGet()
        {

            if (HttpContext.Session.GetString("LoggedIn") != null)
            {
                logged = true;
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
                    var id = Request.Form["idAdmin"];
                    Admin admin = new Admin();
                    admin.Id = id;
                    admin.Name = emailAddress;
                    admin.Password = password;
                    _adminService.Update(admin.Id, admin);

                    this.OnGet();
                }
                catch (Exception e)
                {
                    alert = e.Message;

                }
            }
            if (this.Request.Form.Keys.Contains("deletebutton"))
            {
               
                try
                {
                    var id = Request.Form["idAdmin"];

                    _adminService.Remove(id);
                    HttpContext.Session.Remove("LoggedIn");
                    return Redirect("Index");
                }
                catch (Exception e)
                {
                    alert = e.Message;

                }
                this.OnGet();
            }
            return null;

        }
    }
}
