using Futebol.Application.Applications.Interfaces;
using Futebol.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Web.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserApplication  _userApplication;

    public UserController(IUserApplication userApplication)
    {
        _userApplication = userApplication;
    }

    [Authorize(Roles = "admin")]
    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserModel model)
    {
        var result = await _userApplication.CreateUserAsync(model, Request.Headers["origin"]);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
    {
        var result = await _userApplication.LoginAsync(model);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string token)
    {
        var result = await _userApplication.VerifyAsync(token);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email)
    {
        var result = await _userApplication.ForgotPassword(email, Request.Headers["origin"]);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordModel model)
    {
        var result = await _userApplication.ResetPassword(model);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}
