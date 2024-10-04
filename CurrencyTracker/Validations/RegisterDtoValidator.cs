using CurrencyTracker.WebApi.Dtos;
using FluentValidation;

namespace CurrencyTracker.Validations;
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .Length(5, 20).WithMessage("Username must be between 5 and 20 characters")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Username can only contain letters and numbers");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}