using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BREWCITY.Models;
using BREWCITY;
using BREWCITY.Data;
using BREWCITY.Services;
using System.Net.Http;

namespace BREWCITY.Services
{
    public class GoogleMapService : IGoogleMapService
    {




        public GoogleMapService()
        {

        }
        public async Task<string<mapJpg>> GetGoogleMap(List<string> jsonBreweryAddresses)
        {
            var uri = ConfigureFullUri(jsonBreweryAddresses);
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {

                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
              
            };
            using (var response = client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
            };
            return null;
        }

        public string  ConfigureFullUri(List<string> jsonBreweriesAddresses)
        {
            if(jsonBreweriesAddresses.Count() > 10)
            {
                var numberToRemove = jsonBreweriesAddresses.Count() - 10;
                for(var i = numberToRemove; jsonBreweriesAddresses.Count() > 10; i--)
                {
                    jsonBreweriesAddresses.RemoveAt(10);
                }
            }
            var count = jsonBreweriesAddresses.Count();
            for(var i =0; i < count - 1; i++)
            {
                jsonBreweriesAddresses[i] = ModifyAddress(jsonBreweriesAddresses[i]);
            }
            var uri = ConfigureUri();
            var firstAddress = jsonBreweriesAddresses[0];
            var uriPlusFirstAddress =  AddFirstMarker(uri, firstAddress);
            for(var i = 1; i < count - 1; i++)
            {
                var address = jsonBreweriesAddresses[i];
                uriPlusFirstAddress = AddSubsequentMarkers(uriPlusFirstAddress, address);
            }
            var readyUri = AddRandomLastElement(uriPlusFirstAddress);
            return readyUri;
        }


        public string AddFirstMarker(string configuredUri, string address)
        {
            var markerLocation = ModifyAddress(address);
            var marker = "&format=jpg&zoom=10&size=600x600&maptype=roadmap&&markers=size:tiny%7" + markerLocation;
            configuredUri += marker;
            return configuredUri;
        }
        public string AddSubsequentMarkers(string configuredUri, string address)
        {
            var markerLocation = ModifyAddress(address);
            var marker = "|" + markerLocation;
            configuredUri += marker;
            return configuredUri;
        }
        public string ConfigureUri()
        {
            var uri = "https://maps.googleapis.com/maps/api/staticmap?";
            return uri;
        }
           

         public string ModifyAddress(string address)
         {
          
            var commaSpacesRemoved = address.Replace(", ", ",");
            var spacesRemoved = commaSpacesRemoved.Replace(" ", "+");
            return spacesRemoved;

         }
        public string AddRandomLastElement(string configuredUri)
        {
            var random = "&key=AIzaSyDNqHjDXYRLJWKv3oHJQvCaz9rriqy3PUw";
            configuredUri += random;
            return configuredUri;
        }

    {  
}
