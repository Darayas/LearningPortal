using LearningPortal.Application.App.User;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Const;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Application.Common.Services.JWT
{
    public class JwtBuilder : IJwtBuilder
    {
        private readonly ILogger _Logger;
        private readonly IUserApplication _UserApplication;

        public JwtBuilder(IUserApplication userApplication, ILogger logger)
        {
            _UserApplication=userApplication;
            _Logger=logger;
        }

        public async Task<OperationResult<string>> GenerateTokenAsync(string UserId)
        {
            try
            {
                #region Validation
                if (UserId==null)
                    throw new ArgumentInvalidException("UserIdIsNull");
                #endregion

                #region GetUserDetails
                OutGetUserDetailsForLogin _UserDetails = null;
                {
                    var qUser = await _UserApplication.GetUserDetailsForLoginAsync(new InpGetUserDetailsForLogin { UserId=UserId });
                    if (qUser.IsSucceeded)
                        _UserDetails=qUser.Data;
                    else
                        throw new ArgumentInvalidException(qUser.Message);
                }
                #endregion

                #region General Claims
                List<Claim> Claims = null;
                {
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,_UserDetails.Id),
                        new Claim(ClaimTypes.Name,_UserDetails.UserName),
                        new Claim(ClaimTypes.Email,_UserDetails.Email),
                        new Claim(ClaimTypes.MobilePhone,_UserDetails.PhoneNumber ?? ""),
                        new Claim("FullName",_UserDetails.FullName ?? ""),
                        new Claim("AccessLevelId",_UserDetails.AccessLevelId),
                        new Claim("AccessLevelTitle",_UserDetails.AccessLevelTitle),
                        new Claim("ImgUrl",_UserDetails.ImgUrl),
                    };
                }
                #endregion

                #region Add Roles To Claim
                {
                    Claims.AddRange(_UserDetails.Roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
                }
                #endregion

                #region Token Descreptior
                SecurityTokenDescriptor TokenDescreptor = null;
                {
                    var _Key = Encoding.ASCII.GetBytes(AuthConst.SecretCode);
                    TokenDescreptor = new SecurityTokenDescriptor
                    {
                        Subject=new ClaimsIdentity(Claims),
                        Issuer= AuthConst.Issuer,
                        Audience=AuthConst.Audience,
                        IssuedAt= DateTime.Now,
                        Expires=DateTime.Now.AddHours(48),
                        SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(_Key), SecurityAlgorithms.HmacSha256Signature)
                    };
                }
                #endregion

                #region Generate Token
                string _GeneratedToken = null;
                {
                    var _SecurityToken = new JwtSecurityTokenHandler().CreateToken(TokenDescreptor);
                    _GeneratedToken="Bearer " + new JwtSecurityTokenHandler().WriteToken(_SecurityToken);
                }
                #endregion

                return new OperationResult<string>().Succeeded(_GeneratedToken.AesEncrypt(AuthConst.SecretKey));
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
    }
}
