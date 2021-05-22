using PriceComparerApp.Models;
using PriceComparerApp.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PriceComparerApp.ApiServices
{
    public class CatalogService
    {
        const string Url = "http://192.168.0.192/api/catalog";

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<CatalogDto>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);

            return JsonSerializer.Deserialize<IEnumerable<CatalogDto>>(result, options);
        }


    }
}
