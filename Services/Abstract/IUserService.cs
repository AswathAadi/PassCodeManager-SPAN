using PassCodeManager.DTO.RequestObjects;

namespace PassCodeManager.Services.Abstract
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserObject registerUser);
        Task<bool> ValidateUser(string UserName, string password);
    }
}
