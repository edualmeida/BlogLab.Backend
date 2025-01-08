public interface IIdentity
{
    Task<Result<bool>> Register(RegisterUserCommand userRequest);

    Task<Result<UserResponseModel>> Login(UserRequestModel userRequest);

    Task<Result> ChangePassword(ChangePasswordRequestModel changePasswordRequest);
}