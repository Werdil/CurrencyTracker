using AutoMapper;
using CurrencyTracker.Domain.Entities;
using CurrencyTracker.Domain.Interfaces;
using CurrencyTracker.Validations;
using CurrencyTracker.WebApi.Dtos;
using FluentValidation;
using System.Security.Claims;

namespace CurrencyTracker.Application.Services;
public class UserService : IUserService
{
    private readonly TokenService _tokenService;
    private readonly CurrencyService _currencyService;

    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrencyHelper _currencyHelper;



    public UserService(IUserRepository userRepository, TokenService tokenService, ICurrencyHelper currencyHelper, CurrencyService currencyService, ICurrencyRepository currencyRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _currencyHelper = currencyHelper;
        _currencyService = currencyService;
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var validator = new RegisterDtoValidator();
        var result = await validator.ValidateAsync(dto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existingUser != null)
        {
            throw new Exception("User with this name already exists");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User(dto.Username, passwordHash);

        await _userRepository.AddAsync(user);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var validator = new LoginDtoValidator();
        var result = await validator.ValidateAsync(dto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null || !user.VerifyPassword(dto.Password))
        {
            throw new UnauthorizedAccessException("Incorrect username or password");
        }

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

        var accessToken = _tokenService.GenerateAccessToken(claims);
        return accessToken;
    }

    public async Task SubscribeCurrency(string userid, string code)
    {
        var currency = await _currencyRepository.GetByCodeAsync(code);
        if (currency == null)
        {
            currency = await _currencyHelper.DownloadAndSaveCurrency(code);
        }
        var guid = Guid.Parse(userid);
        var user = await _userRepository.GetByUserIdAsync(guid);
        if (user != null && currency != null)
        {
            user.UserCurrencies.Add(currency);
        }
        await _userRepository.UpdateAsync(user);
    }

    public async Task UnsubscribeCurrency(string userid, string code)
    {
        var userIdGuid = Guid.Parse(userid);
        var currency = await _currencyRepository.GetByCodeAsync(code);
        if (currency != null)
        {
            await _userRepository.DeleteUserCurrencyAsync(userIdGuid, currency.Id);

            var isAnyUserSubscribed = await _userRepository.IsAnyUserSubscribedToCurrencyAsync(currency.Id);
            if (!isAnyUserSubscribed)
            {
                await _currencyRepository.RemoveByCodeAsync(code);
            }
        }
    }

    public async Task<List<CurrencyRateInfoDto>> GetAllCurrencyRateInfosForUser(string userid)
    {
        var guid = Guid.Parse(userid);

        var user = await _userRepository.GetByUserIdWithCurrenciesAsync(guid, u => u.ExchangeRates
                .OrderByDescending(er => er.Date)
                .Take(14));
        if (user != null)
        {
            var dtos = _mapper.Map<List<CurrencyRateInfoDto>>(user.UserCurrencies);
            return dtos;
        }
        return new List<CurrencyRateInfoDto>();
    }
}