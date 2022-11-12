using LearningPortal.Domain.Users.UserAgg.Contracts;
using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LearningPortal.Infrastructure.EFCore.Repositories.User
{
    public class UserRepository : BaseRepository<tblUsers>, IUserRepository
    {
        private readonly SignInManager<tblUsers> _SignInManager;
        private readonly UserManager<tblUsers> _UserManager;
        public UserRepository(MainContext context, UserManager<tblUsers> userManager, SignInManager<tblUsers> signInManager) : base(context)
        {
            _UserManager=userManager;
            _SignInManager=signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(tblUsers User, string Password)
        {
            return await _UserManager.CreateAsync(User, Password);
        }

        public virtual async Task<string> GenerateEmailConfirmationTokenAsync(tblUsers user)
        {
            return await _UserManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public virtual async Task<IdentityResult> ConfirmEmailAsync(tblUsers user, string token)
        {
            return await _UserManager.ConfirmEmailAsync(user, token);
        }

        public async Task<tblUsers> FindById_UManagerAsync(string userId)
        {
            return await _UserManager.FindByIdAsync(userId);
        }

        public async Task<tblUsers> FindByEmail_UManagerAsync(string Email)
        {
            return await _UserManager.FindByEmailAsync(Email);
        }

        public async Task<tblUsers> FindByIdAsync(string userId)
        {
            return await Get.Where(a => a.Id==userId.ToGuid()).SingleOrDefaultAsync();
        }

        public async Task<SignInResult> PasswordSignInAsync(tblUsers user, string password, bool lockoutOnFailure)
        {
            return await _SignInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        public async Task<string> GetIdByEmailAsync(string Email)
        {
            return await GetAsNoTracking.Where(a => a.Email==Email).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
        }

        public async Task<tblUsers> GetByEmailAsync(string Email)
        {
            return await GetAsNoTracking.Where(a => a.Email==Email).SingleOrDefaultAsync();
        }

        public async Task<string> GetIdByPhoneNumberAsync(string PhoneNumber)
        {
            return await GetAsNoTracking.Where(a => a.PhoneNumber==PhoneNumber).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
        }

        public async Task<tblUsers> GetByPhoneNumberAsync(string PhoneNumber)
        {
            return await GetAsNoTracking.Where(a => a.PhoneNumber==PhoneNumber).SingleOrDefaultAsync();
        }

        public async Task<IdentityResult> RemovePasswordAsync(tblUsers user)
        {
            return await _UserManager.RemovePasswordAsync(user);
        }

        public async Task<IdentityResult> AddPasswordAsync(tblUsers user, string password)
        {
            return await _UserManager.AddPasswordAsync(user, password);
        }

        public async Task<bool> HasPasswordAsync(tblUsers user)
        {
            return await _UserManager.HasPasswordAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(tblUsers user)
        {
            return await _UserManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(tblUsers user, string Token, string newPassword)
        {
            return await _UserManager.ResetPasswordAsync(user, Token, newPassword);
        }

        public bool RequireConfirmedEmail()
        {
            return _UserManager.Options.SignIn.RequireConfirmedEmail;
        }

        public async Task<string> GenerateChangeEmailTokenAsync(tblUsers user, string newEmail)
        {
            return await _UserManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async Task<IdentityResult> ChangeEmailAsync(tblUsers user, string newEmail, string token)
        {
            return await _UserManager.ChangeEmailAsync(user, newEmail, token);
        }
    }
}
