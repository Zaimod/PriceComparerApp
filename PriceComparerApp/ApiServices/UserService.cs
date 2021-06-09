using Google.Apis.Auth.OAuth2.Responses;
using Newtonsoft.Json;
using PriceComparerApp.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PriceComparerApp.ApiServices
{
    public class UserService
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        public async Task<UserDto> GetUser(string login)
        {
            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync($"http://192.168.0.192/api/authentication/username/{login}");

            return System.Text.Json.JsonSerializer.Deserialize<UserDto>(result, options);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(UserDto dto)
        {
            var httpClient = new HttpClient();
            
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://192.168.0.192/api/authentication/userUpdate", content);

            return response.IsSuccessStatusCode;
        }
    }
}
