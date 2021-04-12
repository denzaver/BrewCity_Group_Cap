using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BREWCITY.Models
{
    public class JsonBrewery
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "obdb_id")]
        public string ObdbId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "brewery_type")]
        public string BreweryType { get; set; }
        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }
        [JsonProperty(PropertyName = "address_2")]
        public object AddressTwo { get; set; }
        [JsonProperty(PropertyName = "address_3")]
        public object AddressThree { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
        [JsonProperty(PropertyName = "county_province")]
        public object County { get; set; }
        [JsonProperty(PropertyName = "postal_code")]
        public string ZipCode { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "longitude")]
        public object Longitude { get; set; }
        [JsonProperty(PropertyName = "latitude")]
        public object Latitude { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "website_url")]
        public string WebsiteUrl { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
    }
}