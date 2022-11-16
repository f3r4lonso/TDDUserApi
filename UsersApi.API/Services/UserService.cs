using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UsersApi.API.Config;
using UsersApi.API.Model;

namespace UsersApi.API.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;

        public UserService
            (HttpClient httpClient,
            IOptions<UsersApiOptions> apiConfig
            )
        {
            this._httpClient = httpClient;
            this._apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetUsers()
        {
            //var userResponse = await _httpClient.GetAsync("http://example.com/users");
            var userResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);

            if (userResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }

            var responseContent = userResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}

