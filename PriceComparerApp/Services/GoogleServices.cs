using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PriceComparerApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparerApp.Services
{
    public class GoogleServices
    {
        public static readonly string ClientId = "938558567530-jrgv8ncqscr0jf1lfd7ddc6p57bdc9ra.apps.googleusercontent.com";
        public static readonly string ClientSecret = "VJNmvrbK00yLoSBiRrpAgisx";
        public static readonly string RedirectUri = "https://www.youtube.com/channel/UCwiVJ3xwJUvgioIsRnMeCDA";

        public async Task<string> GetAccessTokenAsync(string code)
        {
            var requestUrl =
                "https://www.googleapis.com/oauth2/v4/token"
                + "?code=" + code
                + "&client_id=" + ClientId
                + "&client_secret=" + ClientSecret
                + "&redirect_uri=" + RedirectUri
                + "&grant_type=authorization_code";

            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(requestUrl, null);

            var json = await response.Content.ReadAsStringAsync();

            var accessToken = JsonConvert.DeserializeObject<JObject>(json).Value<string>("access_token");

            return accessToken;
        }

        public async Task<GoogleProfile> GetGoogleUserProfileAsync(string accessToken)
        {

            var requestUrl = "https://www.googleapis.com/plus/v1/people/me"
                             + "?access_token=" + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var googleProfile = JsonConvert.DeserializeObject<GoogleProfile>(userJson);

            return googleProfile;
        }
    }
}
