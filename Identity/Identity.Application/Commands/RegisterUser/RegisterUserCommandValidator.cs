using FluentValidation;
    
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.Email)
            .MinimumLength(UserModelConstants.Identity.MinEmailLength)
            .MaximumLength(UserModelConstants.Identity.MaxEmailLength)
            .EmailAddress()
            .NotEmpty();

        RuleFor(u => u.Password)
            .MinimumLength(UserModelConstants.Identity.MinPasswordLength)
            .MaximumLength(UserModelConstants.Identity.MaxPasswordLength)
            .NotEmpty();

        RuleFor(u => u.ConfirmPassword)
            .Equal(u => u.Password)
            .WithMessage("The password and confirmation password do not match.")
            .NotEmpty();
    }
}