using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Framework.Common.DataAnnotations.String
{
    public class GUIDAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                if (ErrorMessage==null)
                    ErrorMessage="GUIDMsg";

                if (value is null)
                    return ValidationResult.Success;

                if (value is not string)
                {
                    ErrorMessage="'{0}', Value must be string.";
                    return new ValidationResult(GetMessage(validationContext));
                }

                var _guid = Guid.Parse((string)value);

                return ValidationResult.Success;
            }
            catch (ArgumentInvalidException ex)
            {
                return new ValidationResult(ex.Message);
            }
            catch (Exception)
            {
                return new ValidationResult(GetMessage(validationContext));
            }
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
