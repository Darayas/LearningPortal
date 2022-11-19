using LearningPortal.Application.App.AccessLevels;
using LearningPortal.Application.App.FilePath;
using LearningPortal.Application.App.Files;
using LearningPortal.Application.App.FileServer;
using LearningPortal.Application.App.NotificationTemplate;
using LearningPortal.Application.Contract.ApplicationDTO.AccessLevel;
using LearningPortal.Application.Contract.ApplicationDTO.FilePath;
using LearningPortal.Application.Contract.ApplicationDTO.Files;
using LearningPortal.Application.Contract.ApplicationDTO.FileServer;
using LearningPortal.Application.Contract.ApplicationDTO.NotificationTemplate;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Domain.Users.UserAgg.Contracts;
using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Common.Utilities.Paging;
using LearningPortal.Framework.Const;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.User
{
    public class UserApplication : IUserApplication
    {
        private readonly ILogger _Logger;
        private readonly IEmailSender _EmailSender;
        private readonly ISmsSender _SmsSender;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserRepository _UserRepository;
        private readonly INotificationTemplateApplication _NotificationTemplateApplication;
        private readonly IAccessLevelsApplication _AccessLevelsApplication;
        private readonly IFileApplication _FileApplication;
        private readonly IFileServerApplication _FileServerApplication;
        private readonly IFilePathApplication _FilePathApplication;
        public UserApplication(IUserRepository userRepository, ILogger logger, IServiceProvider serviceProvider, INotificationTemplateApplication notificationTemplateApplication, IEmailSender emailSender, IAccessLevelsApplication accessLevelsApplication, ILocalizer localizer, ISmsSender smsSender, IFileApplication fileApplication, IFileServerApplication fileServerApplication, IFilePathApplication filePathApplication)
        {
            _UserRepository = userRepository;
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _NotificationTemplateApplication=notificationTemplateApplication;
            _EmailSender=emailSender;
            _AccessLevelsApplication=accessLevelsApplication;
            _Localizer=localizer;
            _SmsSender=smsSender;
            _FileApplication=fileApplication;
            _FileServerApplication=fileServerApplication;
            _FilePathApplication=filePathApplication;
        }

        public async Task<OperationResult<string>> RegisterByEmailAsync(InpRegisterByEmail Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get access level id
                string _AccLevelId;
                {
                    var _UnConfirmedUserIdResult = await _AccessLevelsApplication.GetIdByNameAsync(new InpGetIdByName { Name="UnConfirmedUser" });
                    if (_UnConfirmedUserIdResult.IsSucceeded==false)
                        return new OperationResult<string>().Failed(_UnConfirmedUserIdResult.Message);

                    _AccLevelId=_UnConfirmedUserIdResult.Data;
                }
                #endregion

                #region Add user to db
                {
                    var tUser = new tblUsers
                    {
                        AccessLevelId=_AccLevelId.ToGuid(),
                        Email=Input.Email,
                        EmailConfirmed=false,
                        FullName=Input.FullName,
                        UserName=Input.Email,
                        IsActive=true,
                        RegisterDate=DateTime.Now
                    };

                    var _Result = await _UserRepository.CreateUserAsync(tUser, Input.Password);
                    if (_Result.Succeeded)
                        if (_UserRepository.RequireConfirmedEmail())
                            return new OperationResult<string>().Succeeded(tUser.Id.ToString());
                        else
                            return new OperationResult<string>().Succeeded("UserCreatedSuccessfully", null);
                    else
                        return new OperationResult<string>().Failed(string.Join(", ", _Result.Errors.Select(a => a.Description)));
                }
                #endregion
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        private async Task<OperationResult<tblUsers>> RegisterByPhoneNumberAsync(InpRegisterByPhoneNumber Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get access level id
                string _AccLevelId;
                {
                    var _UnConfirmedUserIdResult = await _AccessLevelsApplication.GetIdByNameAsync(new InpGetIdByName { Name="UnConfirmedUser" });
                    if (_UnConfirmedUserIdResult.IsSucceeded==false)
                        return new OperationResult<tblUsers>().Failed(_UnConfirmedUserIdResult.Message);

                    _AccLevelId=_UnConfirmedUserIdResult.Data;
                }
                #endregion

                #region Add user to db
                {
                    var tUser = new tblUsers
                    {
                        AccessLevelId=_AccLevelId.ToGuid(),
                        Email="",
                        EmailConfirmed=false,
                        FullName="",
                        UserName=Input.PhoneNumber,
                        PhoneNumber=Input.PhoneNumber,
                        IsActive=true,
                        RegisterDate=DateTime.Now
                    };

                    var _Result = await _UserRepository.CreateUserAsync(tUser, "123456");
                    if (_Result.Succeeded)
                        return new OperationResult<tblUsers>().Succeeded("UserCreatedSuccessfully", tUser);
                    else
                        return new OperationResult<tblUsers>().Failed(string.Join(", ", _Result.Errors.Select(a => a.Description)));
                }
                #endregion
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<tblUsers>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<tblUsers>().Failed("Error500");
            }
        }

        public async Task<OperationResult> ConfirmationEmailAccountAsync(InpConfirmationEmailAccount Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get User
                tblUsers tUser = null;
                {
                    tUser = await _UserRepository.Get.Where(a => a.Id == Input.UserId.ToGuid()).SingleOrDefaultAsync();
                    if (tUser is null)
                        return new OperationResult().Failed("UserId NotFound");

                    if (tUser.EmailConfirmed is true)
                        return new OperationResult().Failed("User is Confirmed");
                }
                #endregion

                #region Generate Token
                string _Token = null;
                {
                    _Token= await _UserRepository.GenerateEmailConfirmationTokenAsync(tUser);
                }
                #endregion

                #region Encrypted Data
                string _EncryptedToken = null;
                {
                    _Token= $"{tUser.Id}, {_Token}";
                    _EncryptedToken= _Token.AesEncrypt(SiteSettingConst.AesKey);
                }
                #endregion

                #region Generate Link
                string _GeneratedLink = "";
                {
                    _EncryptedToken= WebUtility.UrlEncode(_EncryptedToken);
                    _GeneratedLink = Input.ConfirmationEmailLink.Replace("[Token]", _EncryptedToken);
                }
                #endregion

                #region Get and generate email template
                string _Template = null;
                {
                    #region Get Template
                    {
                        var _Result = await _NotificationTemplateApplication.GetTemplateAsync(new InpGetTemplate
                        {
                            LangId=Input.LangId,
                            Name="RegisterEmailConfirm"
                        });

                        if (_Result.IsSucceeded is false)
                            return new OperationResult().Failed(_Result.Message);

                        _Template= _Result.Data;
                    }
                    #endregion

                    #region Generate 
                    {
                        _Template= _Template.Replace("[Link]", _GeneratedLink);
                    }
                    #endregion
                }
                #endregion

                #region SendEmail
                {
                    await _EmailSender.SendMailAsync(tUser.Email, _Localizer["ConfirmationEmailAccount"], _Template);
                }
                #endregion

                return new OperationResult().Succeeded("ConfirmationEmailAccountWasSent");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ConfirmAccountByEmailTokenAsync(InpConfirmAccountByEmailToken Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Decrypt Token
                string _UserId;
                string _Token;
                {
                    var _DecryptedToken = Input.Token.AesDecrypt(SiteSettingConst.AesKey);
                    _UserId=_DecryptedToken.Split(", ")[0];
                    _Token=_DecryptedToken.Split(", ")[1];
                }
                #endregion

                #region Get user
                tblUsers tUser;
                {
                    tUser = await _UserRepository.Get.SingleOrDefaultAsync(a => a.Id==_UserId.ToGuid());
                    if (tUser==null)
                    {
                        _Logger.Fatal("کلید رمزنگاری توکن ها لو رفته است. لطفا سریعتر نسب به تغییر آن اقدام نمایید");
                        return new OperationResult().Failed("TokenIsInvalid");
                    }
                }
                #endregion

                #region Confirm user account
                {
                    var _Result = await _UserRepository.ConfirmEmailAsync(tUser, _Token);
                    if (_Result.Succeeded==false)
                        return new OperationResult().Failed(String.Join(',', _Result.Errors.Select(a => a.Description)));
                }
                #endregion

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ChangeEmailAsync(InpChangeEmail Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Decrypt Token
                string _UserId;
                string _Token;
                string _NewEmail;
                {
                    var _DecryptedToken = Input.Token.AesDecrypt(SiteSettingConst.AesKey);
                    _UserId=_DecryptedToken.Split(", ")[0];
                    _NewEmail=_DecryptedToken.Split(", ")[1];
                    _Token=_DecryptedToken.Split(", ")[2];
                }
                #endregion

                #region Get user
                tblUsers tUser;
                {
                    tUser = await _UserRepository.Get.SingleOrDefaultAsync(a => a.Id==_UserId.ToGuid());
                    if (tUser==null)
                    {
                        _Logger.Fatal("کلید رمزنگاری توکن ها لو رفته است. لطفا سریعتر نسب به تغییر آن اقدام نمایید");
                        return new OperationResult().Failed("TokenIsInvalid");
                    }
                }
                #endregion

                #region change email
                {
                    var _Result = await _UserRepository.ChangeEmailAsync(tUser, _NewEmail, _Token);
                    if (_Result.Succeeded==false)
                        return new OperationResult().Failed(String.Join(',', _Result.Errors.Select(a => a.Description)));
                }
                #endregion

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult<string>> LoginByEmailPasswordAsync(InpLoginByEmailPassword Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get UserId
                string _UserId = null;
                {
                    _UserId = await _UserRepository.GetIdByEmailAsync(Input.Email);
                    if (_UserId==null)
                        return new OperationResult<string>().Failed("PasswordIsInvalid");
                }
                #endregion

                #region Validate User Data
                {
                    var _Result = await LoginByUManagerAsync(_UserId, Input.Password);
                    if (!_Result.IsSucceeded)
                        return _Result;
                }
                #endregion

                return new OperationResult<string>().Succeeded(_UserId);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        private async Task<OperationResult<string>> LoginByUManagerAsync(string UserId, string Password)
        {
            try
            {
                var qUser = await _UserRepository.FindById_UManagerAsync(UserId);
                if (qUser==null)
                    return new OperationResult<string>().Failed("PasswordIsInvalid");

                if (!qUser.EmailConfirmed)
                    return new OperationResult<string>().Failed("PleaseConfirmYourEmail");

                if (!qUser.IsActive)
                    return new OperationResult<string>().Failed("YouAccountIsDisabled");

                var _Result = await _UserRepository.PasswordSignInAsync(qUser, Password, true);
                if (_Result.Succeeded)
                    return new OperationResult<string>().Succeeded(qUser.Id.ToString());
                else
                {
                    if (_Result.IsLockedOut)
                        return new OperationResult<string>().Failed("YourAccountIsLockedOut");
                    else if (_Result.IsNotAllowed)
                        return new OperationResult<string>().Failed("PasswordIsInvalid");
                    else
                        return new OperationResult<string>().Failed("PasswordIsInvalid");
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        private async Task<OperationResult> ConfirmPhoneNumberAsync(tblUsers tUser)
        {
            try
            {
                if (tUser == null)
                    return new OperationResult().Failed("tUser cant be null");

                tUser.PhoneNumberConfirmed=true;
                await _UserRepository.UpdateAsync(tUser, default);

                return new OperationResult().Succeeded();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        private async Task<OperationResult<string>> LoginByOTPAsync(string UserId, string OTP)
        {
            try
            {
                #region Validation
                tblUsers tUser;
                {
                    tUser = await _UserRepository.FindByIdAsync(UserId);
                    if (tUser==null)
                        return new OperationResult<string>().Failed("PasswordIsInvalid");

                    //if (!tUser.PhoneNumberConfirmed)
                    //    return new OperationResult<string>().Failed("PleaseConfirmYourPhoneNumber");

                    if (!tUser.IsActive)
                        return new OperationResult<string>().Failed("YouAccountIsDisabled");
                }
                #endregion

                #region Validate OTP Code
                {
                    var _Result = (tUser.SmsHashCode == OTP.ToMd5());
                    if (!_Result)
                        return new OperationResult<string>().Failed("OTPCodeIsInvalid");
                }
                #endregion

                #region Check OTP Expire
                {
                    if (tUser.LastTryToSendSms.HasValue)
                        if (tUser.LastTryToSendSms.Value.AddMinutes(5)<=DateTime.Now)
                            return new OperationResult<string>().Failed("OtpIsExpired.PleaseUseResendBtn");
                }
                #endregion

                #region Confirm PhoneNumber In Case Of Need
                {
                    if (tUser.PhoneNumberConfirmed==false)
                    {
                        var _Result = await ConfirmPhoneNumberAsync(tUser);
                        if (!_Result.IsSucceeded)
                            return new OperationResult<string>().Failed(_Result.Message);
                    }
                }
                #endregion

                return new OperationResult<string>().Succeeded(tUser.Id.ToString());
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        public async Task<OperationResult<OutGetUserDetailsForLogin>> GetUserDetailsForLoginAsync(InpGetUserDetailsForLogin Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await (from a in _UserRepository.GetAsNoTracking
                                   where a.Id==Input.UserId.ToGuid()
                                   select new OutGetUserDetailsForLogin
                                   {
                                       Id=a.Id.ToString(),
                                       Email=a.Email,
                                       FullName=a.FullName,
                                       PhoneNumber=a.PhoneNumber,
                                       UserName=a.UserName,
                                       AccessLevelId=a.AccessLevelId.ToString(),
                                       AccessLevelTitle= a.tblAccessLevels.Name,
                                       ImgUrl = a.tblProfileImg.tblFilePaths.tblFileServer.HttpDomin
                                                                  + a.tblProfileImg.tblFilePaths.tblFileServer.HttpPath
                                                                  + a.tblProfileImg.tblFilePaths.Path
                                                                  + a.tblProfileImg.FileName,
                                       Roles=a.tblUserRoles.Select(b => b.tblRoles.Name).ToArray()
                                   })
                                   .SingleOrDefaultAsync();

                return new OperationResult<OutGetUserDetailsForLogin>().Succeeded(qData);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<OutGetUserDetailsForLogin>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<OutGetUserDetailsForLogin>().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByPhoneNumberStep1Async(InpLoginByPhoneNumberStep1 Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get UserId by PhoneNumber
                tblUsers tUser = null;
                {
                    tUser = await _UserRepository.GetByPhoneNumberAsync(Input.PhoneNumber);
                }
                #endregion

                #region Register User In Case Of Need
                {
                    if (tUser==null)
                    {
                        var _Result = await RegisterByPhoneNumberAsync(Input.Adapt<InpRegisterByPhoneNumber>());
                        if (!_Result.IsSucceeded)
                            return _Result;

                        tUser = _Result.Data;
                    }
                }
                #endregion

                #region Set OTP SMS Password
                string PassOTP;
                {
                    var _Result = await GenerateOTPTokenAsync(tUser);
                    if (!_Result.IsSucceeded)
                        return _Result;

                    PassOTP=_Result.Data;
                }
                #endregion

                #region Send Code
                {
                    var _Result = _SmsSender.SendLoginCode(tUser.PhoneNumber, PassOTP);
                    if (_Result==false)
                        return new OperationResult().Failed("ErrorSmsSender");
                }
                #endregion

                return new OperationResult().Succeeded("SendSmsCodeWasSucceeded");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult<string>> LoginByPhoneNumberStep2Async(InpLoginByPhoneNumberStep2 Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get User
                string _UserId = null;
                {
                    _UserId = await _UserRepository.GetAsNoTracking.Where(a => a.PhoneNumber==Input.PhoneNumber).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
                    if (_UserId==null)
                        return new OperationResult<string>().Failed("OTP is invalid");
                }
                #endregion


                #region Check OTP Code
                {
                    var _Result = await LoginByOTPAsync(_UserId, Input.OTPCode.ToString());
                    if (!_Result.IsSucceeded)
                        return _Result;
                }
                #endregion

                return new OperationResult<string>().Succeeded(_UserId);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        private async Task<OperationResult<string>> GenerateOTPTokenAsync(tblUsers tUser)
        {
            try
            {
                #region Validation
                if (tUser == null)
                    return new OperationResult<string>().Failed("tUser cant be null");
                #endregion

                #region Check Last Try
                {
                    if (tUser.LastTryToSendSms.HasValue)
                        if (tUser.LastTryToSendSms.Value.AddMinutes(2) >= DateTime.Now)
                            return new OperationResult<string>().Failed("PleaseWait2MinToResendSms");
                }
                #endregion

                #region Set OTP code
                string PassOTP;
                {
                    PassOTP = new Random().Next(10101, 99999).ToString();

                    tUser.SmsHashCode = PassOTP.ToMd5();
                    tUser.LastTryToSendSms=DateTime.Now;

                    await _UserRepository.UpdateAsync(tUser, default);
                }
                #endregion

                return new OperationResult<string>().Succeeded(PassOTP);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        public async Task<OperationResult> ResendOTPSmsCodeAsync(InpResendOTPSmsCode Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get UserId by PhoneNumber
                string _UserId;
                {
                    _UserId = await _UserRepository.GetIdByPhoneNumberAsync(Input.PhoneNumber);
                }
                #endregion

                #region Get User
                tblUsers tUser = null;
                {
                    tUser = await _UserRepository.FindById_UManagerAsync(_UserId);
                    if (tUser==null)
                        return new OperationResult().Failed("PhoneNumber is invalid");

                    if (!tUser.PhoneNumberConfirmed)
                        return new OperationResult<string>().Failed("PhoneNumber is invalid");
                }
                #endregion

                #region Set OTP SMS Password
                string PassOTP;
                {
                    var _Result = await GenerateOTPTokenAsync(tUser);
                    if (!_Result.IsSucceeded)
                        return _Result;

                    PassOTP=_Result.Data;
                }
                #endregion

                #region Send Code
                {
                    var _Result = _SmsSender.SendLoginCode(tUser.PhoneNumber, PassOTP);
                    if (_Result==false)
                        return new OperationResult().Failed("ErrorSmsSender");
                }
                #endregion

                return new OperationResult().Succeeded("SendSmsCodeWasSucceeded");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        public async Task<OperationResult> RecoveryPasswordStep1Async(InpRecoveryPasswordStep1 Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region GetUser
                tblUsers tUser;
                {
                    tUser = await _UserRepository.FindByEmail_UManagerAsync(Input.Email);
                    if (tUser == null)
                        return new OperationResult().Failed("Email is invalid");
                }
                #endregion

                #region Generate Token
                string _GeneratedToken;
                {
                    _GeneratedToken = await _UserRepository.GeneratePasswordResetTokenAsync(tUser);
                    _GeneratedToken = $"{tUser.Id},{_GeneratedToken}";

                    _GeneratedToken = _GeneratedToken.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion

                #region Generate link
                string _GeneratedLink;
                {
                    _GeneratedToken = WebUtility.UrlEncode(_GeneratedToken);
                    _GeneratedLink = Input.RecoveryPageUrl.Replace("[Token]", _GeneratedToken);
                }
                #endregion

                #region Get and generate email template
                string _Template = null;
                {
                    #region Get Template
                    {
                        var _Result = await _NotificationTemplateApplication.GetTemplateAsync(new InpGetTemplate
                        {
                            LangId=Input.LangId,
                            Name="ForgotPassword"
                        });

                        if (_Result.IsSucceeded is false)
                            return new OperationResult().Failed(_Result.Message);

                        _Template= _Result.Data;
                    }
                    #endregion

                    #region Generate 
                    {
                        _Template = _Template.Replace("[Link]", _GeneratedLink);
                    }
                    #endregion
                }
                #endregion

                #region SendEmail
                {
                    await _EmailSender.SendMailAsync(tUser.Email, _Localizer["ForgotPassword"], _Template);
                }
                #endregion

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        public async Task<OperationResult> RecoveryPasswordStep2Async(InpRecoveryPasswordStep2 Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Decrypt and parse token
                string _UserId;
                string _Token;
                {
                    string _DecryptedToken = Input.Token.AesDecrypt(AuthConst.SecretKey);
                    _UserId = _DecryptedToken.Split(',')[0];
                    _Token = _DecryptedToken.Split(',')[1];
                }
                #endregion

                #region Get user by id
                tblUsers tUser = null;
                {
                    tUser = await _UserRepository.FindById_UManagerAsync(_UserId);
                    if (tUser == null)
                    {
                        _Logger.Fatal("کلید رمزنگاری توکن ها لو رفته است. لطفا سریعتر نسب به تغییر آن اقدام نمایید");
                        return new OperationResult().Failed(_Localizer["Token is invalid"]);
                    }
                }
                #endregion

                #region Update password
                {
                    var _Result = await _UserRepository.ResetPasswordAsync(tUser, _Token, Input.Password);
                    if (!_Result.Succeeded)
                        return new OperationResult().Failed(String.Join(" , ", _Result.Errors.Select(b => b.Description)));
                }
                #endregion

                return new OperationResult().Succeeded(_Localizer["ResetPasswordWasSucceded"]);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByDisposableLinkStep1Async(InpLoginByDisposableLinkStep1 Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region GetUser
                tblUsers tUser;
                {
                    tUser = await _UserRepository.GetByEmailAsync(Input.Email);
                    if (tUser == null)
                        return new OperationResult().Failed("EmailIsInvalid");
                }
                #endregion

                #region Generate Token and Encrypt
                string _EncryptToken;
                {
                    string _Token = $"{tUser.Id},{tUser.Email}";
                    _EncryptToken = _Token.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion

                #region Generate Link
                string _GeneratedLink;
                {
                    _EncryptToken= WebUtility.UrlEncode(_EncryptToken);
                    _GeneratedLink =Input.EmailLinkTemplate.Replace("[Token]", _EncryptToken);
                }
                #endregion

                #region Get and generate email template
                string _Template = null;
                {
                    #region Get Template
                    {
                        var _Result = await _NotificationTemplateApplication.GetTemplateAsync(new InpGetTemplate
                        {
                            LangId=Input.LangId,
                            Name="DisposableLink"
                        });

                        if (_Result.IsSucceeded is false)
                            return new OperationResult().Failed(_Result.Message);

                        _Template= _Result.Data;
                    }
                    #endregion

                    #region Generate 
                    {
                        _Template = _Template.Replace("[Link]", _GeneratedLink);
                    }
                    #endregion
                }
                #endregion

                #region SendEmail
                {
                    await _EmailSender.SendMailAsync(tUser.Email, _Localizer["LoginByDisposableLink"], _Template);
                }
                #endregion

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult<string>> LoginByDisposableLinkStep2Async(InpLoginByDisposableLinkStep2 Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Decrypt Token
                string UserId;
                string Email;
                {
                    var _DecryptedToken = Input.Token.AesDecrypt(AuthConst.SecretKey);
                    UserId= _DecryptedToken.Split(',')[0];
                    Email= _DecryptedToken.Split(',')[1];
                }
                #endregion

                #region GetUser
                tblUsers tUser;
                {
                    tUser = await _UserRepository.Get
                                            .Where(a => a.Id == UserId.ToGuid())
                                            .Where(a => a.Email == Email)
                                            .SingleOrDefaultAsync();

                    if (tUser == null)
                        return new OperationResult<string>().Failed("Token is invalid");

                    if (tUser.EmailConfirmed==false)
                        return new OperationResult<string>().Failed("email not confirmed");

                }
                #endregion

                return new OperationResult<string>().Succeeded(tUser.Id.ToString());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        public async Task<OperationResult<OutGetUserForEditProfile>> GetUserForEditProfileAsync(InpGetUserForEditProfile Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _UserRepository.Get.Where(a => a.Id == Input.UserId.ToGuid())
                                                     .Select(a => new OutGetUserForEditProfile
                                                     {
                                                         FullName = a.FullName,
                                                         Email = a.Email,
                                                         Bio = a.Bio,
                                                         PhoneNumber = a.PhoneNumber,
                                                         ImgUrl = a.tblProfileImg.tblFilePaths.tblFileServer.HttpDomin
                                                                  + a.tblProfileImg.tblFilePaths.tblFileServer.HttpPath
                                                                  + a.tblProfileImg.tblFilePaths.Path
                                                                  + a.tblProfileImg.FileName
                                                     })
                                                     .SingleOrDefaultAsync();

                return new OperationResult<OutGetUserForEditProfile>().Succeeded(qData);

            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<OutGetUserForEditProfile>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<OutGetUserForEditProfile>().Failed("Error500");
            }
        }

        public async Task<OperationResult<string>> GetIdByPhoneNumberAsync(InpGetIdByPhoneNumber Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _UserRepository.Get
                                                .Where(a => a.PhoneNumber == Input.PhoneNumber)
                                                .Select(a => new { Id = a.Id })
                                                .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult<string>().Failed("PhoneNumber is invalid");

                return new OperationResult<string>().Succeeded(qData.Id.ToString());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }

        public async Task<OperationResult> SaveEditProfileAsync(InpSaveEditProfile Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get user
                tblUsers _User;
                {
                    _User = await _UserRepository.Get
                                                 .Where(a => a.Id==Input.UserId.ToGuid())
                                                 .SingleOrDefaultAsync();

                    if (_User == null)
                        return new OperationResult().Failed("Id is invalid");
                }
                #endregion

                #region Edit User
                {
                    //var _MapConfig = TypeAdapterConfig<InpSaveEditProfile, tblUsers>.NewConfig()
                    //                                                                .Map(dest => dest.Email, src => _User.Email).Config;
                    //_User = Input.Adapt<InpSaveEditProfile, tblUsers>(_MapConfig);

                    _User.Bio = Input.Bio;
                    _User.FullName = Input.FullName;
                }
                #endregion

                #region Edit profile image
                {
                    if (Input.Image != null)
                    {
                        #region Remove old profile image
                        {
                            if (_User.ProfileImgId != null)
                            {
                                var _Result = await _FileApplication.RemoveFileAsync(new InpRemoveFile { FileId = _User.ProfileImgId.ToString() });
                                if (_Result.IsSucceeded == false)
                                    return new OperationResult().Failed("Cant remove old profile image");
                            }
                        }
                        #endregion

                        #region Upload new profile image
                        {
                            #region Get Best Server
                            string _ServerId;
                            {
                                var _Res = await _FileServerApplication.GetBestServerIdAsync(new InpGetBestServerId { });
                                if (_Res.IsSucceeded==false)
                                    return new OperationResult().Failed(_Res.Message);

                                _ServerId = _Res.Data;
                            }
                            #endregion

                            var _Result = await _FileApplication.UploadProfileImageAsync(new InpUploadProfileImage
                            {
                                UploaderUserId = Input.UserId,
                                FileServerId = _ServerId,
                                FormFile = Input.Image,
                                FilePathId = await _FilePathApplication.GetProfileImagePathIdAsync(new InpGetProfileImagePathId { FileServerId = _ServerId }),
                                Title = Input.FullName
                            });

                            if (_Result.IsSucceeded==false)
                                return new OperationResult().Failed(_Result.Message);

                            _User.ProfileImgId = _Result.Data.ToGuid();
                        }
                        #endregion
                    }
                }
                #endregion

                #region Update database
                {
                    await _UserRepository.UpdateAsync(_User, default, true);
                }
                #endregion

                #region ارسال لینک تایید ایمیل درصورت نیاز
                {
                    if (_User.Email != Input.Email)
                    {
                        #region Check Duplicate Email
                        {
                            var qData = await _UserRepository.Get
                                                    .Where(a => a.Email==Input.Email)
                                                    //.Where(a => a.Id != _User.Id)
                                                    .AnyAsync();

                            if (qData == true)
                                return new OperationResult().Failed("Email is Duplicated");
                        }
                        #endregion

                        #region Generate Token
                        string _Token = null;
                        {
                            _Token = await _UserRepository.GenerateChangeEmailTokenAsync(_User, Input.Email);
                        }
                        #endregion

                        #region Encrypted Data
                        string _EncryptedToken = null;
                        {
                            _Token = $"{_User.Id}, {Input.Email}, {_Token}";
                            _EncryptedToken = _Token.AesEncrypt(SiteSettingConst.AesKey);
                        }
                        #endregion

                        #region Generate Link
                        string _GeneratedLink = "";
                        {
                            _EncryptedToken= WebUtility.UrlEncode(_EncryptedToken);
                            _GeneratedLink = Input.ChangeEmailLink.Replace("[Token]", _EncryptedToken);
                        }
                        #endregion

                        #region Get and generate email template
                        string _Template = null;
                        {
                            #region Get Template
                            {
                                var _Result = await _NotificationTemplateApplication.GetTemplateAsync(new InpGetTemplate
                                {
                                    LangId=Input.LangId,
                                    Name="ChangeEmailConfirm"
                                });

                                if (_Result.IsSucceeded is false)
                                    return new OperationResult().Failed(_Result.Message);

                                _Template= _Result.Data;
                            }
                            #endregion

                            #region Generate 
                            {
                                _Template= _Template.Replace("[Link]", _GeneratedLink);
                            }
                            #endregion
                        }
                        #endregion

                        #region SendEmail
                        {
                            await _EmailSender.SendMailAsync(Input.Email, _Localizer["ChangeEmailAccount"], _Template);
                        }
                        #endregion

                        return new OperationResult().Succeeded("OperationWasSucceded,PleaseClickLink");
                    }
                }
                #endregion

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult<OutGetListUsersForManage>> GetListUsersForManageAsync(InpGetListUsersForManage Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _UserRepository.Get
                                        .Select(a => new OutGetListUsersForManageItems
                                        {
                                            Id = a.Id.ToString(),
                                            FullName = a.FullName,
                                            AccessLevelTitle = a.tblAccessLevels.Name,
                                            Date = a.RegisterDate,
                                            IsActive = a.IsActive,
                                            ProfileImgUrl = a.tblProfileImg.tblFilePaths.tblFileServer.HttpDomin
                                                                + a.tblProfileImg.tblFilePaths.tblFileServer.HttpPath
                                                                + a.tblProfileImg.tblFilePaths.Path
                                                                + a.tblProfileImg.FileName
                                        })
                                        .OrderByDescending(a => a.Date);

                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Input.Page, Input.Take);

                return new OperationResult<OutGetListUsersForManage>().Succeeded(new OutGetListUsersForManage
                {
                    Paging = _PagingData,
                    Items = await qData.Skip(_PagingData.Skip).Take(_PagingData.Take).ToListAsync()
                });
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<OutGetListUsersForManage>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<OutGetListUsersForManage>().Failed("Error500");
            }
        }
    }
}
