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
        private string _adminId = null;
        public string adminId { get; private set; } = "";
        public string adminName { get; private set; } = "";
        public string adminPassword { get; private set; } = "";
        public string adminAvatar { get; private set; }
        public bool logged { get; private set; } = false;
        public bool updated { get; private set; } = false;
        public bool deleted { get; private set; } = false;

        public EditAccountModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        public void OnGet()
        {

            if (HttpContext.Session.GetString("LoggedIn") != null)
            {
                logged = true;
                try
                {
                    _adminId = HttpContext.Session.GetString("LoggedIn");
                    Admin admin = _adminService.Get(_adminId);
                    adminId = admin.Id;
                    adminName = admin.Name;
                    adminPassword = "****************";
                    adminAvatar = admin.Avatar;

                }
                catch (Exception e)
                {
                    log = e.Message;
                }
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
                    var avatar = Request.Form["imgsrc"];
                    var id = Request.Form["idAdmin"];
                    Admin admin = new Admin();
                    admin.Id = id;
                    admin.Name = emailAddress;
                    admin.Password = password;
                    admin.Avatar = avatar;

                    if (_adminService.isMaster(id))
                    {
                        admin.AdminType = Admin.Type.Master;
                    }
                    else
                    {
                        admin.AdminType = Admin.Type.Admin;
                    }

                    _adminService.Update(admin.Id, admin);
                    deleted = false;
                    updated = true;

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
                updated = false;
                deleted = true;
                this.OnGet();
            }
            return null;

        }
    }
}
