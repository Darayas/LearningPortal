using LearningPortal.Framework.Const;
using Microsoft.AspNetCore.Http;
using System;

namespace LearningPortal.WebApp.Common.ExMethods
{
    public static class HttpResponseEx
    {
        public static void Logout(this HttpResponse response)
        {
            #region Remove Cookie
            {
                response.Cookies.Delete(".AspNetCore.Identity.Application");
                for (int i = 0; i <= 10; i++)
                {
                    response.Cookies.Delete(AuthConst.CookieName + i);
                    response.Cookies.Delete(".AspNetCore.Identity.ApplicationC" + i);
                }
            }
            #endregion
        }

        public static void CreateAuthCookie(this HttpResponse response, string AuthToken, bool Remember)
        {
            Logout(response);

            #region Add new cookie
            {
                int i = 0;
                while (AuthToken!=null)
                {
                    if (AuthToken.Length > 2000)
                    {
                        string _Section = AuthToken.Substring(0, 2000);

                        response.Cookies.Append(AuthConst.CookieName+i, _Section, Remember ? new CookieOptions() { Expires=DateTime.Now.AddHours(48) } : new CookieOptions());

                        AuthToken= AuthToken.Remove(0, 2000);
                    }
                    else
                    {
                        response.Cookies.Append(AuthConst.CookieName+i, AuthToken, Remember ? new CookieOptions() { Expires=DateTime.Now.AddHours(48) } : new CookieOptions());
                        AuthToken=null;
                    }

                    i++;
                }
            }
            #endregion
        }
    }
}
