using SL2021.Application.Services.User.Contracts;
using FluentValidation;

namespace SL2021.Application.Services.User.Validators
{
    public class RegisterRequestValidator : AbstractValidator<Register.Request>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName не заполнен!")
                .Matches("[a-zA-Z0-9_]*")
                .MinimumLength(5)
                .MaximumLength(50);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email не заполнен!")
                .EmailAddress().WithMessage("Адрес не валидный!");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password не заполнен!")
                .Matches("/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[+!@#$%^&*]).{6,20}/g")
                .MinimumLength(6)
                .MaximumLength(20);
            RuleFor(x => x.FirstName)
                .Matches("[A-Z][a-z]*").WithMessage("FirstName не валидный!")
                .MinimumLength(1)
                .MaximumLength(50);
            RuleFor(x => x.LastName)
                .Matches("[A-Z]+([ '-][a-zA-Z]+)*").WithMessage("LastName не валидный!")
                .MinimumLength(1)
                .MaximumLength(50);
            RuleFor(x => x.MiddleName)
                .Matches("[A-Z][a-z]*").WithMessage("MiddleName не валидный!")
                .MinimumLength(1)
                .MaximumLength(50);
        }
    }
}
