using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Framework.Common.DataAnnotations.String
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (ErrorMessage == null)
                ErrorMessage = "PhoneNumberValidationMsg";

            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new Exception("Value must be string.");

            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();

            if (!value.ToString().IsMatch(_Localizer["PhoneNumberPattern"]))
            {
                return new ValidationResult(GetMessage(validationContext));
            }

            return ValidationResult.Success;
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
