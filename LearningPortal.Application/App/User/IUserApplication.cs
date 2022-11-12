using LearningPortal.Application.Contract.ApplicationDTO.Result;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.User
{
    public interface IUserApplication
    {
        Task<OperationResult> ChangeEmailAsync(InpChangeEmail Input);
        Task<OperationResult> ConfirmAccountByEmailTokenAsync(InpConfirmAccountByEmailToken Input);
        Task<OperationResult> ConfirmationEmailAccountAsync(InpConfirmationEmailAccount Input);
        Task<OperationResult<string>> GetIdByPhoneNumberAsync(InpGetIdByPhoneNumber Input);
        Task<OperationResult<OutGetUserDetailsForLogin>> GetUserDetailsForLoginAsync(InpGetUserDetailsForLogin Input);
        Task<OperationResult<OutGetUserForEditProfile>> GetUserForEditProfileAsync(InpGetUserForEditProfile Input);
        Task<OperationResult> LoginByDisposableLinkStep1Async(InpLoginByDisposableLinkStep1 Input);
        Task<OperationResult<string>> LoginByDisposableLinkStep2Async(InpLoginByDisposableLinkStep2 Input);
        Task<OperationResult<string>> LoginByEmailPasswordAsync(InpLoginByEmailPassword Input);
        Task<OperationResult> LoginByPhoneNumberStep1Async(InpLoginByPhoneNumberStep1 Input);
        Task<OperationResult<string>> LoginByPhoneNumberStep2Async(InpLoginByPhoneNumberStep2 Input);
        Task<OperationResult> RecoveryPasswordStep1Async(InpRecoveryPasswordStep1 Input);
        Task<OperationResult> RecoveryPasswordStep2Async(InpRecoveryPasswordStep2 Input);
        Task<OperationResult<string>> RegisterByEmailAsync(InpRegisterByEmail Input);
        Task<OperationResult> ResendOTPSmsCodeAsync(InpResendOTPSmsCode Input);
        Task<OperationResult> SaveEditProfileAsync(InpSaveEditProfile Input);
    }
}