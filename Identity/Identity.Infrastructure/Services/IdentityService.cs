using Microsoft.AspNetCore.Identity;

internal class IdentityService(
    UserManager<User> userManager,
    IJwtGenerator jwtGenerator,
    IUserBuilder builder
    ) : IIdentity
{
    private const string InvalidErrorMessage = "Invalid credentials.";

    public async Task<Result<bool>> Register(RegisterUserCommand request)
    {
        var user = builder
                .WithFirstName(request.FirstName)
                .WithMiddleName(request.MiddleName)
                .WithSurname(request.Surname)
                .WithEmail(request.Email)
                .Build();

        var identityResult = await userManager.CreateAsync(
            user,
            request.Password);

        var errors = identityResult.Errors.Select(e => e.Description);

        if (!identityResult.Succeeded)
        {
            return Result<bool>.Failure(errors);
        }

        await userManager.AddToRoleAsync(user, GetRoleName(request.Role));

        return Result<bool>.SuccessWith(true);
    }

    public async Task<Result<UserResponseModel>> Login(UserRequestModel userRequest)
    {
        var user = await userManager.FindByEmailAsync(userRequest.Email);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var passwordValid = await userManager.CheckPasswordAsync(
            user,
            userRequest.Password);

        if (!passwordValid)
        {
            return InvalidErrorMessage;
        }

        var token = await jwtGenerator.GenerateToken(user);

        return new UserResponseModel(token);
    }

    public async Task<Result> ChangePassword(ChangePasswordRequestModel changePasswordRequest)
    {
        var user = await userManager.FindByIdAsync(changePasswordRequest.UserId);

        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var identityResult = await userManager.ChangePasswordAsync(
            user,
            changePasswordRequest.CurrentPassword,
            changePasswordRequest.NewPassword);

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(errors);
    }


    private string GetRoleName(RegisterUserRole userRole)
        => userRole switch
        {
            RegisterUserRole.Administrator => CommonModelConstants.Common.AdministratorRoleName,
            RegisterUserRole.User => CommonModelConstants.Common.UserRoleName,
            RegisterUserRole.Author => CommonModelConstants.Common.AuthorRoleName,
            _ => throw new InvalidOperationException("Invalid user role.")
        };
}