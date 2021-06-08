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

        public async Task<IEnumerable<CatalogDto>> GetByProductId(int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"http://192.168.0.192/api/catalog/productId/{id}");

            return JsonSerializer.Deserialize<IEnumerable<CatalogDto>>(result, options);
        }

        public async Task<IEnumerable<CatalogDto>> GetBySearch(string stringSearch)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"http://192.168.0.192/api/catalog/search/{stringSearch}");

            return JsonSerializer.Deserialize<IEnumerable<CatalogDto>>(result, options);
        }
    }
}
