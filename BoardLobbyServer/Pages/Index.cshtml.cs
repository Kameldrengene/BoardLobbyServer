using BoardLobbyServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BoardLobbyServer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AdminService _adminService;
        public string weather { get; set; } = "";
        public bool logged { get; set; } = false;
        public string adminName { get; set; } = "";

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, AdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }

        public IActionResult OnGet()
        {
            if (_adminService.Get().Count == 0)
            {
                return Redirect("/SignUp");
            }
            if (HttpContext.Session.GetString("LoggedIn") != null)
            {
                logged = true;
                adminName = HttpContext.Session.GetString("LoggedIn");

            }
            weather = GetWeather().Result;
            return null;
        }
        public async Task<string> GetWeather()
        {

            using (var client = new HttpClient())
            {
                string vejr = null;

                var request = new HttpRequestMessage(HttpMethod.Get, "http://vejr.eu/api.php?location=Roskilde&degree=C");
                var header1 = new MediaTypeWithQualityHeaderValue("application/json");
                var header2 = new ProductInfoHeaderValue("CamelLudo", "1.0");

                request.Headers.UserAgent.Add(header2);
                request.Headers.Accept.Add(header1);


                var resp = await client.SendAsync(request);


                if (resp.IsSuccessStatusCode)
                {

                    vejr = resp.Content.ReadAsStringAsync().Result;

                }


                //returning the student list to view  
                return vejr;
            }
        }
    }
}
