using SIgnin_Manager.DTO;
using SIgnin_Manager.Models;

namespace SIgnin_Manager.Interface
{
    public interface IUserInterface
    {


        Task<UserManagerResponse> RegisterUserAsync(RegisterDTO model);

        Task<UserManagerResponse> LoginUserAsync(LoginDTO model);

        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);

        Task<UserManagerResponse> ForgetPasswordAsync(string email);


        Task<ApplicationUser> FindUserByEmail(string email);


        ICollection<ApplicationUser> GetAllUser();
        //Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
    }
}
