using SL2021.Application.Services.User.Contracts;
using FluentValidation;

namespace SL2021.Application.Services.User.Validators
{
    public class UpdateRequestValidator : AbstractValidator<Update.Request>
    {
        public UpdateRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName не заполнен!")
                .Matches("[a-zA-Z0-9_]*")
                .MinimumLength(5)
                .MaximumLength(50);
            RuleFor(x => x.FirstName)
                .Matches("[A-Z][a-z]*").WithMessage("FirstName не валидный!")
                .MinimumLength(1)
                .MaximumLength(50);
            RuleFor(x => x.LastName)
                .Matches("[A-Z][a-z]*").WithMessage("LastName не валидный!")
                .MinimumLength(1)
                .MaximumLength(50);
            RuleFor(x => x.MiddleName)
                .Matches("[A-Z][a-z]*").WithMessage("MiddleName не валидный!")
                .MinimumLength(1)
                .MaximumLength(50);
        }
    }
}
