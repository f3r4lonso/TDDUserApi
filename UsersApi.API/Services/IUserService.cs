using Microsoft.AspNetCore.Mvc;
using UsersApi.API.Model;

namespace UsersApi.API.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
    }
}
