using LearningPortal.Framework.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace LearningPortal.Framework.Common.DataAnnotations.String
{

    public class EmailAttribute : ValidationAttribute
    {
        public bool IsEmailAddress(string EmailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(EmailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage==null)
            {
                ErrorMessage="EmailValidationMsg";
            }

            if (value is null)
            {
                return ValidationResult.Success;
            }

            if (value is not string)
            {
                throw new Exception("Value must be string.");
            }

            if (IsEmailAddress(value.ToString())==false)
            {
                return new ValidationResult(GetMessage(validationContext));
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        private string GetMessage(ValidationContext validationContext)
        {
            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();

            ErrorMessage=_Localizer[ErrorMessage];

            if (ErrorMessage.Contains("{0}"))
            {
                ErrorMessage=ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);
            }

            return ErrorMessage;
        }
    }
}
