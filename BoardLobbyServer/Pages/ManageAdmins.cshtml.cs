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
    public class ManageAdminsModel : PageModel
    {
        private readonly AdminService _adminService;
        public List<Admin> admins { get; private set; } = null;
        public string alert { get; private set; } = "";
        public bool logged { get; private set; } = false;
        public bool master { get; private set; } = false;
        public string log { get; private set; } = "";
        public ManageAdminsModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoggedIn") != null)
            {
                logged = true;
                admins = _adminService.Get();
                int ismaster = (from admin in admins
                                where admin.Name.Contains(HttpContext.Session.GetString("LoggedIn"))
                                && admin.AdminType == Admin.Type.Master
                                select admin.AdminType).Count();
                if (ismaster == 1)
                {
                    master = true;
                }
            }

            return null;
        }
        public IActionResult OnPost()
        {

            if (this.Request.Form.Keys.Contains("updatebutton"))
            {
                alert = "update";
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
                    return Redirect("ManageAdmins");
                }
                catch (Exception e)
                {
                    log = e.Message;

                }
            }
            if (this.Request.Form.Keys.Contains("deletebutton"))
            {
                alert = "delete";

                try
                {
                    var id = Request.Form["idAdmin"];

                    _adminService.Remove(id);
                }
                catch (Exception e)
                {
                    log = e.Message;
                }
                return Redirect("ManageAdmins");
            }
            return null;

        }
    }
}
