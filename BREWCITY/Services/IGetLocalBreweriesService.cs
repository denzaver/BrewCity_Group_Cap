using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BREWCITY.Models;

namespace BREWCITY.Services
{
    public interface IGetLocalBreweriesService
    {
        Task<List<JsonBrewery>> GetLocalBreweries(string state);
    }
}
