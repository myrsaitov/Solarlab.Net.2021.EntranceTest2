using WidePictBoard.Application.Services.User.Contracts;
using FluentValidation;

namespace WidePictBoard.Application.Services.User.Validators
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
