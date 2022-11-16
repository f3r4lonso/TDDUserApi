using Microsoft.AspNetCore.Mvc;
using UsersApi.API.Model;
using UsersApi.API.Services;

namespace UsersApi.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> logger;
    private readonly IUserService userService;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        this.logger = logger;
        this.userService = userService;
    }

    [HttpGet(Name = "GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetUsers();

        if (users.Any())
        {
            logger.LogInformation("OK");
            return Ok(users);
        }
        else
        {
            logger.LogInformation("Not Found");
            return NotFound(users);
        }
    }
}
