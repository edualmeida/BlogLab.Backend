using Common.Application;
using Identity.Application;
using Identity.Application.Commands;
using Microsoft.AspNetCore.Identity;

internal class IdentityService(
    UserManager<User> userManager,
    IJwtGenerator jwtGenerator,
    IUserBuilder builder
    ) : IIdentity
{
    private const string InvalidCredentialsErrorMessage = "Invalid credentials.";

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

    public async Task<Result<LoginResponseModel>> Login(UserRequestModel userRequest)
    {
        var user = await userManager.FindByEmailAsync(userRequest.Email);
        if (user == null)
        {
            return Result<LoginResponseModel>.Failure(InvalidCredentialsErrorMessage);
        }

        var passwordValid = await userManager.CheckPasswordAsync(
            user,
            userRequest.Password);

        if (!passwordValid)
        {
            return Result<LoginResponseModel>.Failure(InvalidCredentialsErrorMessage);
        }

        var isAdministrator = await userManager
            .IsInRoleAsync(user, CommonModelConstants.Common.AdministratorRoleName);
        var roles = new List<string>();
        if (isAdministrator)
        {
            roles.Add(CommonModelConstants.Common.AdministratorRoleName);
        }
        
        var token = await jwtGenerator.GenerateToken(user, roles);

        return new LoginResponseModel(token)
        {
            UserId = user.Id,
            Username = user.UserName!,
            IsAdmin = isAdministrator,
        };
    }

    public async Task<Result> ChangePassword(ChangePasswordRequestModel changePasswordRequest)
    {
        var user = await userManager.FindByIdAsync(changePasswordRequest.UserId);

        if (user == null)
        {
            return Result<LoginResponseModel>.Failure(InvalidCredentialsErrorMessage);
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