using Newtonsoft.Json;
using PriceComparerApp.Models;
using PriceComparerApp.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparerApp.ApiServices
{
    public class SignService
    {
        public async Task<TokenResponse> SignIn(string login, string password)
        {
            var httpClient = new HttpClient();
            UserForSignInDto userForSignInDto = new UserForSignInDto { UserName = login, Password = password };
            var json = JsonConvert.SerializeObject(userForSignInDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://192.168.0.192/api/authentication/login", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TokenResponse>(jsonResult);

            return result;
        }
    }
}
