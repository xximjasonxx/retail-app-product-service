
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductApi.Common;
using ProductApi.Models;
using RestSharp;

namespace ProductApi.Extensions
{
    public static class StringExtensions
    {
        public static async Task<User> ValidateToken(this string token)
        {
            var loginApiUrl = Environment.GetEnvironmentVariable("LOGIN_API_URL");
            var client = new RestClient(loginApiUrl);
            var request = new RestRequest("validate", Method.GET);
            request.AddHeader("Authorization", token);

            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(response.Content);
                return null;
            }

            return JsonConvert.DeserializeObject<User>(response.Content);
        }
    }
}