using BoardLobbyServer.Model;
using BoardLobbyServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoardLobbyServer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AdminService _adminService;
        public string weather { get; set; } = "";
        public bool logged { get; set; } = false;
        public string adminName { get; set; } = "";
        public string playerCount { get; set; } = "0";
        public string gameCount { get; set; } = "0";
        public string gamesOnline { get; set; } = "0";
        public string gamesLaunched { get; set; } = "0";
        public string adminAvatar { get; set; } = "";

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
                Admin admin = _adminService.GetByName(adminName);
                adminAvatar = admin.Avatar;
                gamesOnline = LobbyData.Instance.Games.Count.ToString();
                playerCount = Stats.playerList.Count.ToString();
                gameCount = Stats.gameHistory.Count.ToString();

            }
            try
            {
                weather = GetWeather().Result;
            }catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<string> GetWeather()
        {

            using (var client = new HttpClient())
            {
                string data = null;
                Weather weather = null;

                var request = new HttpRequestMessage(HttpMethod.Get, "http://vejr.eu/api.php?location=Roskilde&degree=C");
                var header1 = new MediaTypeWithQualityHeaderValue("application/json");
                var header2 = new ProductInfoHeaderValue("CamelLudo", "1.0");

                request.Headers.UserAgent.Add(header2);
                request.Headers.Accept.Add(header1);


                var resp = await client.SendAsync(request);


                if (resp.IsSuccessStatusCode)
                {

                   data = resp.Content.ReadAsStringAsync().Result;
                   weather = JsonSerializer.Deserialize<Weather>(data);

                }
                
                return weather.CurrentData.GetValueOrDefault("skyText") + " " + weather.CurrentData.GetValueOrDefault("temperature");
            }
        }
    }
    public class Weather
    {
        public string LocationName { get; set; }
        public Dictionary<string, string> CurrentData { get; set; }

    }
}
