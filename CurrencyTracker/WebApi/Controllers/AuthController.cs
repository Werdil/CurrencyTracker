using CurrencyTracker.Domain.Interfaces;
using CurrencyTracker.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyTracker.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        await _userService.RegisterAsync(dto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var accessToken = await _userService.LoginAsync(dto);
        return Ok(new { AccessToken = accessToken });
    }
}