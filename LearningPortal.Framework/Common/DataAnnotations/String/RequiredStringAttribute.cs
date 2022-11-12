using LearningPortal.Framework.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Framework.Common.DataAnnotations.String
{
    public class RequiredStringAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage==null)
                ErrorMessage="RequiredStringMsg";

            if (value is null)
                return new ValidationResult(GetMessage(validationContext));
            else
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
