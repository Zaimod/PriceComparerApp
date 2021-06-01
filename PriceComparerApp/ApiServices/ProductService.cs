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
    public class ProductService
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

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync("http://192.168.0.192/api/products");

            return JsonSerializer.Deserialize<IEnumerable<ProductDto>>(result, options);
        }
    }
}
