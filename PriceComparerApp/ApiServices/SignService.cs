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
        /// <summary>
        /// Signs the in.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Signs up.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public async Task<bool> SignUp(UserForSignUpDto dto)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("http://192.168.0.192/api/authentication", content).GetAwaiter().GetResult();

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends the verification code.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public async Task<bool> SendVerificationCode(string userName)
        {
            var httpClient = new HttpClient();
            UserNameForVerificationCodeDto verificationCodeDto = new UserNameForVerificationCodeDto { userName = userName };
            var json = JsonConvert.SerializeObject(verificationCodeDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("http://192.168.0.192/api/authentication/sendVerificationCode", content);

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Changes the email confirmed.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
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
