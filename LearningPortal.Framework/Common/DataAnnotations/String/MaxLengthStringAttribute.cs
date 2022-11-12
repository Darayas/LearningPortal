using LearningPortal.Framework.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Framework.Common.DataAnnotations.String
{
    public class MaxLengthStringAttribute : ValidationAttribute
    {
        private int _MaxLenght { get; set; }
        public MaxLengthStringAttribute(int MaxLenght)
        {
            _MaxLenght=MaxLenght;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage==null)
                ErrorMessage="MaxLengthStringMsg";

            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new Exception("Value must be string.");

            if (value.ToString().Length>_MaxLenght)
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

            if (ErrorMessage.Contains("{1}"))
                ErrorMessage=ErrorMessage.Replace("{1}", _MaxLenght.ToString());

            return ErrorMessage;
        }
    }
}
