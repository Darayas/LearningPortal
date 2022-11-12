using Kavenegar;
using Kavenegar.Models;
using LearningPortal.Framework.Contracts;
using System;
using System.Linq;

namespace LearningPortal.Framework.Services.Sms
{
    public class KaveNegarSmsSender : ISmsSender
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly string ApiKey;
        private readonly string Sender;
        public KaveNegarSmsSender(ILogger logger, ILocalizer localizer)
        {
            _Logger=logger;

            ApiKey="6F57535A6F726F6E69766C37687137654730705A42772B744A43346351673233535A62654971672B6B67633D";
            Sender="";
            _Localizer=localizer;
        }
        public bool Send(string PhoneNumber, string Message)
        {
            throw new NotImplementedException();
        }

        private bool SendOTP(string PhoneNumber, string TemplateName, string[] Tokens)
        {
            try
            {
                if (PhoneNumber==null)
                    throw new ArgumentNullException("PhoneNumber cant be null");

                if (TemplateName==null)
                    throw new ArgumentNullException("TemplateName cant be null");

                if (Tokens==null)
                    throw new ArgumentNullException("Tokens cant be null");

                if (Tokens.Count()<1)
                    throw new ArgumentNullException("Token items must be greater than Zero");

                KavenegarApi Api = new KavenegarApi(ApiKey);
                SendResult _Result = null;

                if (Tokens.Count() == 1)
                {
                    _Result = Api.VerifyLookup(PhoneNumber, Tokens[0], TemplateName);
                }
                else if (Tokens.Count() == 2)
                {
                    _Result = Api.VerifyLookup(PhoneNumber, Tokens[0], Tokens[1], Tokens[2], TemplateName);
                }
                else if (Tokens.Count() == 3)
                {
                    _Result = Api.VerifyLookup(PhoneNumber, Tokens[0], Tokens[1], Tokens[2], TemplateName);
                }

                bool IsSent = CheckStatus(_Result);

                return IsSent;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        private bool CheckStatus(SendResult Result)
        {
            try
            {
                if (Result.Status == 5)
                {
                    return true;
                }
                else
                {
                    if (Result.Status == 401)
                    {
                        throw new Exception("حساب کاربری غیرفعال شده است");
                    }
                    else if (Result.Status == 402)
                    {
                        throw new Exception("عملیات ناموفق بود");
                    }
                    else if (Result.Status == 403)
                    {
                        throw new Exception("کد شناسائی API-Key معتبر نمی‌باشد");
                    }
                    else if (Result.Status == 406)
                    {
                        throw new Exception("پارامترهای اجباری خالی ارسال شده اند");
                    }
                    else if (Result.Status == 407)
                    {
                        throw new Exception("دسترسی به اطلاعات مورد نظر برای شما امکان پذیر نیست. برای استفاده از متدهای Select، SelectOutbox و LatestOutBox و یا ارسال با خط بین المللی نیاز به تنظیم IP در بخش تنظیمات امنیتی می باشد");
                    }
                    else if (Result.Status == 409)
                    {
                        throw new Exception("سرور قادر به پاسخگوئی نیست بعدا تلاش کنید");
                    }
                    else if (Result.Status == 411)
                    {
                        throw new Exception($"دریافت کننده نامعتبر است, [{Result.Receptor}]");
                    }
                    else if (Result.Status == 412)
                    {
                        throw new Exception($"ارسال کننده نامعتبر است, [{Sender}]");
                    }
                    else if (Result.Status == 414)
                    {
                        throw new Exception("حجم درخواست بیشتر از حد مجاز است ،ارسال پیامک :هر فراخوانی حداکثر 200 رکوردو کنترل وضعیت :هر فراخوانی 500 رکورد");
                    }
                    else if (Result.Status == 415)
                    {
                        throw new Exception("اندیس شروع بزرگ تر از کل تعداد شماره های مورد نظر است");
                    }
                    else if (Result.Status == 416)
                    {
                        throw new Exception("IP سرویس مبدا با تنظیمات مطابقت ندارد");
                    }
                    else if (Result.Status == 417)
                    {
                        throw new Exception("تاریخ ارسال اشتباه است و فرمت آن صحیح نمی باشد.");
                    }
                    else if (Result.Status == 418)
                    {
                        throw new Exception("اعتبار شما کافی نمی‌باشد");
                    }
                    else if (Result.Status == 420)
                    {
                        throw new Exception("استفاده از لینک در متن پیام برای شما محدود شده است");
                    }
                    else if (Result.Status == 422)
                    {
                        throw new Exception("داده ها به دلیل وجود کاراکتر نامناسب قابل پردازش نیست");
                    }
                    else if (Result.Status == 424)
                    {
                        throw new Exception("الگوی مورد نظر پیدا نشد");
                    }
                    else if (Result.Status == 426)
                    {
                        throw new Exception("استفاده از این متد نیازمند سرویس پیشرفته می‌باشد");
                    }
                    else if (Result.Status == 427)
                    {
                        throw new Exception("استفاده از این خط نیازمند ایجاد سطح دسترسی می باشد");
                    }
                    else if (Result.Status == 428)
                    {
                        throw new Exception("ارسال کد از طریق تماس تلفنی امکان پذیر نیست");
                    }
                    else if (Result.Status == 429)
                    {
                        throw new Exception("IP محدود شده است");
                    }
                    else if (Result.Status == 432)
                    {
                        throw new Exception("پارامتر کد در متن پیام پیدا نشد");
                    }
                    else if (Result.Status == 451)
                    {
                        throw new Exception("فراخوانی بیش از حد در بازه زمانی مشخص IP محدود شده");
                    }
                    else if (Result.Status == 501)
                    {
                        throw new Exception("فقط امکان ارسال پیام تست به شماره صاحب حساب کاربری وجود دارد");
                    }
                    else
                    {
                        throw new Exception("Unknown error");
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public bool SendLoginCode(string PhoneNumber, string Code)
        {
            return SendOTP(PhoneNumber, _Localizer["SendLoginCode"], new string[] { Code });
        }
    }
}
