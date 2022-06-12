using FutebolApi.Models.UserModel;
using FutebolApi.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutebolApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "admin")]
    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserModel model)
    {
        var result = await _userService.CreateUserAsync(model);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
    {
        var result = await _userService.LoginAsync(model);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}
