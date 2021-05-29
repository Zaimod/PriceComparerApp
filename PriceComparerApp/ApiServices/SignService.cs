using Newtonsoft.Json;
using PriceComparerApp.Models;
using PriceComparerApp.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Net;
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

        public async Task<bool> SignUp(UserForSignUpDto dto)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("http://192.168.0.192/api/authentication", content).GetAwaiter().GetResult();

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> SendVerificationCode(string userName)
        {
            var httpClient = new HttpClient();
            UserNameForVerificationCodeDto verificationCodeDto = new UserNameForVerificationCodeDto { userName = userName };
            var json = JsonConvert.SerializeObject(verificationCodeDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("http://192.168.0.192/api/authentication/sendVerificationCode", content);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeEmailConfirmed(string code, string userName)
        {
            var httpClient = new HttpClient();
            ChangeEmailConfirmedDto changeEmailConfirmedDto = new ChangeEmailConfirmedDto { userName = userName, code = code };
            var json = JsonConvert.SerializeObject(changeEmailConfirmedDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("http://192.168.0.192/api/authentication/changeEmailConfirmed", content);

            return result.IsSuccessStatusCode;
        }
    }
}
