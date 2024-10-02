using CurrencyTracker.Application.Services;
using CurrencyTracker.Domain.Interfaces;
using CurrencyTracker.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CurrencyTracker.WebApi.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly CurrencyService _currencyService;


    public CurrencyController(CurrencyService currencyService, IUserService userService)
    {
        _currencyService = currencyService;
        _userService = userService;
    }

    [HttpGet("all-rate-info")]
    public async Task<ActionResult<List<CurrencyRateInfoDto>>> GetAllCurrencyRateInfosForUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _userService.GetAllCurrencyRateInfosForUser(userId);
        return Ok(result);
    }

    [HttpPost("{code}/subscribe")]
    public async Task<IActionResult> SubscribeCurrency(string code)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userService.SubscribeCurrency(userId, code);
        return Ok();
    }
    [HttpGet("{code}/history")]
    public async Task<ActionResult<List<ExchangeRateDto>>> GetCurrencyHistory(string code)
    {
        var history = await _currencyService.GetCurrencyRates(code);
        return Ok(history);
    }


    [HttpGet("rate-info/{code}/{days}")]
    public async Task<ActionResult<CurrencyRateInfoDto>> GetCurrencyRateInfo(string code, int days)
    {
        if (string.IsNullOrWhiteSpace(code) || days <= 0)
        {
            return BadRequest("Invalid currency code or days");
        }

        var rateInfo = await _currencyService.GetCurrencyRateInfo(code, days);

        if (rateInfo == null)
        {
            return NotFound($"Currency with code '{code}' not found or data is unavailable");
        }

        return Ok(rateInfo);
    }
}
