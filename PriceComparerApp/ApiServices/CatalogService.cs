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

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <returns></returns>
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        /// <summary>
        /// Gets the by product identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<CatalogDto>> GetByProductId(int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"http://192.168.0.192/api/catalog/productId/{id}");

            return JsonSerializer.Deserialize<IEnumerable<CatalogDto>>(result, options);
        }

        /// <summary>
        /// Gets the by search.
        /// </summary>
        /// <param name="stringSearch">The string search.</param>
        /// <returns></returns>
        public async Task<IEnumerable<CatalogDto>> GetBySearch(string stringSearch)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"http://192.168.0.192/api/catalog/search/{stringSearch}");

            return JsonSerializer.Deserialize<IEnumerable<CatalogDto>>(result, options);
        }
    }
}
