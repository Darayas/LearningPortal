using LearningPortal.Framework.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Framework.Common.DataAnnotations.Files
{
    public class RequiredFileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage==null)
                ErrorMessage="RequiredFileMsg";

            if (value is null)
                return new ValidationResult(GetMessage(validationContext));

            if (value is not IFormFile)
                throw new ArgumentException("RequiredFile: value must be IFormFile");

            return ValidationResult.Success;
        }

        private string GetMessage(ValidationContext validationContext)
        {
            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();

            ErrorMessage=_Localizer[ErrorMessage];

            if (ErrorMessage.Contains("{0}"))
                ErrorMessage=ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

            return ErrorMessage;
        }
    }
}
