using BREWCITY.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace BREWCITY.Services
{
    public interface BreweryService
    {

        public async Task<IActionResult> GetBreweryList(string state)
        {
            string uri = "https://brianiswu-open-brewery-db-v1.p.rapidapi.com/breweries/search?query=" + state;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {

                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
                 {
                         { "x-rapidapi-key", "1dfbf71e41msh870113fa2bffc98p14393fjsnd146183a749a" },
                         { "x-rapidapi-host", "brianiswu-open-brewery-db-v1.p.rapidapi.com" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    return (IActionResult)JsonConvert.DeserializeObject<JsonBrewery>(jsonResult);


                }
            };
            return null;

        }

    }
}