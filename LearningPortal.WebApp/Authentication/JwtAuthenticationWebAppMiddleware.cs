using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Const;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Authentication
{
    public class JwtAuthenticationWebAppMiddleware
    {
        private readonly RequestDelegate _Next;
        public JwtAuthenticationWebAppMiddleware(RequestDelegate Next)
        {
            _Next=Next;
        }

        public async Task InvokeAsync(HttpContext Context)
        {
            string _EncryptedToken = null;
            for (int i = 0; i < 10; i++)
                if (Context.Request.Cookies[AuthConst.CookieName+i]!=null)
                    _EncryptedToken += Context.Request.Cookies[AuthConst.CookieName+i].ToString();

            if (_EncryptedToken!=null)
            {
                string _DecryptedToken = _EncryptedToken.AesDecrypt(AuthConst.SecretKey);

                Context.Request.Headers.Add("Authorization", _DecryptedToken);
            }

            await _Next(Context);
        }
    }
}
