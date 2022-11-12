using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LearningPortal.Domain.Users.UserAgg.Contracts
{
    public interface IUserRepository : IRepository<tblUsers>
    {
        Task<IdentityResult> AddPasswordAsync(tblUsers user, string password);
        Task<IdentityResult> ChangeEmailAsync(tblUsers user, string newEmail, string token);
        Task<IdentityResult> ConfirmEmailAsync(tblUsers user, string token);
        Task<IdentityResult> CreateUserAsync(tblUsers User, string Password);
        Task<tblUsers> FindByEmail_UManagerAsync(string Email);
        Task<tblUsers> FindByIdAsync(string userId);
        Task<tblUsers> FindById_UManagerAsync(string userId);
        Task<string> GenerateChangeEmailTokenAsync(tblUsers user, string newEmail);
        Task<string> GenerateEmailConfirmationTokenAsync(tblUsers user);
        Task<string> GeneratePasswordResetTokenAsync(tblUsers user);
        Task<tblUsers> GetByEmailAsync(string Email);
        Task<tblUsers> GetByPhoneNumberAsync(string PhoneNumber);
        Task<string> GetIdByEmailAsync(string Email);
        Task<string> GetIdByPhoneNumberAsync(string PhoneNumber);
        Task<bool> HasPasswordAsync(tblUsers user);
        Task<SignInResult> PasswordSignInAsync(tblUsers user, string password, bool lockoutOnFailure);
        Task<IdentityResult> RemovePasswordAsync(tblUsers user);
        bool RequireConfirmedEmail();
        Task<IdentityResult> ResetPasswordAsync(tblUsers user, string Token, string newPassword);
    }
}
