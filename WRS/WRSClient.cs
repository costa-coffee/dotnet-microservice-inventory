﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Inventory;

namespace WRS
{
    public class WRSClient
    {
        public WRSClient(
            string host,
            string username,
            string password)
        {
            this.Username = username;
            this.Password = password;

            this.Client = new HttpClient
            {
                BaseAddress = new Uri(host)
            };
        }

        public string Username { get; }
        public string Password { get; }

        private HttpClient Client { get; }

        public async Task<TokenResponse> Authenticate()
        {
            var body = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            };

            
            var credentials = Encoding.ASCII.GetBytes($"{Username}:{Password}");

            var request = new HttpRequestMessage(HttpMethod.Post, "token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            request.Content = new FormUrlEncodedContent(body);

            HttpResponseMessage response = await Client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };

            TokenResponse token = JsonSerializer.Deserialize<TokenResponse>(content, options);

            return token;
        }

    }
}
