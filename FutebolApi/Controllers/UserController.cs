using FutebolApi.Models.User;
using FutebolApi.Services.Interfaces;
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
        var result = await _userService.CreateUserAsync(model, Request.Headers["origin"]);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
    {
        var result = await _userService.LoginAsync(model);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string token)
    {
        var result = await _userService.VerifyAsync(token);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email)
    {
        var result = await _userService.ForgotPassword(email, Request.Headers["origin"]);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordModel model)
    {
        var result = await _userService.ResetPassword(model);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}
