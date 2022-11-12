using LearningPortal.Framework.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Framework.Common.DataAnnotations.Integer
{
    public class RangeNumAttribute : ValidationAttribute
    {
        private readonly decimal _Min;
        private readonly decimal _Max;

        public RangeNumAttribute(decimal Minimum, decimal Maximum)
        {
            _Min=Minimum;
            _Max=Maximum;
        }

        public RangeNumAttribute(int Minimum, int Maximum)
        {
            _Min=Minimum;
            _Max=Maximum;
        }

        public RangeNumAttribute(long Minimum, long Maximum)
        {
            _Min=Minimum;
            _Max=Maximum;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage==null)
                ErrorMessage="RangeNumMsg";

            if (value is null)
                return ValidationResult.Success;

            if (value is not int
                && value is not byte
                && value is not short
                && value is not long
                && value is not sbyte
                && value is not ushort
                && value is not uint
                && value is not ulong
                && value is not float
                && value is not double
                && value is not decimal)
                throw new Exception("Value only can be Numeric.");

            if (Convert.ToDecimal(value) < _Min || Convert.ToDecimal(value)>_Max)
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
                ErrorMessage=ErrorMessage.Replace("{1}", _Min.ToString());

            if (ErrorMessage.Contains("{2}"))
                ErrorMessage=ErrorMessage.Replace("{2}", _Max.ToString());

            return ErrorMessage;
        }
    }
}
