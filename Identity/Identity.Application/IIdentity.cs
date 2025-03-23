public interface IIdentity
{
    Task<Result<bool>> Register(RegisterUserCommand userRequest);

    Task<Result<LoginResponseModel>> Login(UserRequestModel userRequest);

    Task<Result> ChangePassword(ChangePasswordRequestModel changePasswordRequest);
}