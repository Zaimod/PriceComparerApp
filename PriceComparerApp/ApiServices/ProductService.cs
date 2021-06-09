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
        /// Gets the products.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync("http://192.168.0.192/api/products");

            return JsonSerializer.Deserialize<IEnumerable<ProductDto>>(result, options);
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"http://192.168.0.192/api/products/byCategoryId/{categoryId}");

            return JsonSerializer.Deserialize<IEnumerable<ProductDto>>(result, options);
        }
    }
}
