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
        public string weather { get; set; } = "";

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
           weather = await GetWeather();
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
