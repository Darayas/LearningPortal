using LearningPortal.Application.Contract.PresentationDTO.ViewModels;
using System.Linq;
using System;
using System.Security.Claims;

namespace LearningPortal.WebApp.Common.ExMethods
{
    public static class IdentityEx
    {
        public static vmGetUserDetails GetUserDetails(this ClaimsPrincipal user)
        {
            var UserData = new vmGetUserDetails()
            {
                UserId = user.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault() ?? "",
                UserName = user.Claims.Where(a => a.Type == ClaimTypes.Name).Select(a => a.Value).SingleOrDefault() ?? "",
                Email = user.Claims.Where(a => a.Type == ClaimTypes.Email).Select(a => a.Value).SingleOrDefault() ?? "",
                MobileNumber = user.Claims.Where(a => a.Type == ClaimTypes.MobilePhone).Select(a => a.Value).SingleOrDefault() ?? "",
                FullName = user.Claims.Where(a => a.Type == "FullName").Select(a => a.Value).SingleOrDefault() ?? "",
                AccessLevelId = user.Claims.Where(a => a.Type == "AccessLevelId").Select(a => a.Value).SingleOrDefault() ?? "",
                AccessLevelTitle = user.Claims.Where(a => a.Type == "AccessLevelTitle").Select(a => a.Value).SingleOrDefault() ?? "",
                ImgUrl = user.Claims.Where(a => a.Type == "ImgUrl").Select(a => a.Value).SingleOrDefault() ?? "",
            };

            return UserData;
        }
    }
}
