using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Services
{
    public interface IGoogleMapService
    {
        public async Task<IActionResult> GetGoogleMap(List<string> jsonBreweryAddresses);
    }
}
