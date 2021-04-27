using SL2021.Application.Services.User.Contracts;
using FluentValidation;

namespace SL2021.Application.Services.User.Validators
{
    public class RegisterRequestValidator : AbstractValidator<Register.Request>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email не заполнен!")
                .EmailAddress().WithMessage("Адрес не валидный!");
        }
    }
}
