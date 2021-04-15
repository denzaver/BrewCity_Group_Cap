using BREWCITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Services
{
    interface IGetLocalBreweriesService
    {
        Task<List<JsonBrewery>> GetLocalBreweries(string state);
    }
}
