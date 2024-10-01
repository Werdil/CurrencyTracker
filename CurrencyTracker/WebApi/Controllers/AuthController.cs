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
        try
        {
            await _userService.RegisterAsync(dto.Username, dto.Password, dto.ConfirmPassword);
            return Ok("Registration completed successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var accessToken = await _userService.LoginAsync(dto.Username, dto.Password);
            return Ok(new { AccessToken = accessToken });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}