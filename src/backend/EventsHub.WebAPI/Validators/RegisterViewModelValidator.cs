using EventsHub.WebAPI.ViewModels;
using FluentValidation;

namespace EventsHub.WebAPI.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(s => s.Email).NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("A valid email is required");
        }
    }
}
