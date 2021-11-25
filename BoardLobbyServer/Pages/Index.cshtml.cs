using BoardLobbyServer.Model;
using BoardLobbyServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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
        public Weather _weather { get; set; } = null;
        public bool logged { get; set; } = false;
        public string adminId { get; set; } = "";
        public Admin admin { get; set; } = null;
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
                adminId = HttpContext.Session.GetString("LoggedIn");
                admin = _adminService.Get(adminId);
                adminAvatar = admin.Avatar;
                gamesOnline = LobbyData.Instance.Games.Count.ToString();
                playerCount = Stats.playerList.Count.ToString();
                gameCount = Stats.gameHistory.Count.ToString();

            }
            try
            {
                _weather = GetWeather().Result;
            }catch (Exception ex)
            {
                _weather = new Weather();
                _weather._temperature = ex.Message;
            }
            return null;
        }
        public async Task<Weather> GetWeather()
        {

            using (var client = new HttpClient())
            {
              
                Weather weather = new Weather();
               
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=55.73107&lon=12.39702");
                var header1 = new MediaTypeWithQualityHeaderValue("application/json");
                var header2 = new ProductInfoHeaderValue("CamelLudo", "1.0");

                request.Headers.UserAgent.Add(header2);
                request.Headers.Accept.Add(header1);


                var resp = await client.SendAsync(request);


                if (resp.IsSuccessStatusCode)
                {

                    string data = resp.Content.ReadAsStringAsync().Result;
                   
                    dynamic result = JObject.Parse(data);
                    weather._temperature = result["properties"]["timeseries"][0]["data"]["instant"]["details"]["air_temperature"]; 
                    weather._skytext = result["properties"]["timeseries"][0]["data"]["next_1_hours"]["summary"]["symbol_code"];


                }

                return weather;
            }
        }
    }
    public class Weather
    {
        public string _temperature { get; set; }
        public string _skytext { get; set; }

    }
}
