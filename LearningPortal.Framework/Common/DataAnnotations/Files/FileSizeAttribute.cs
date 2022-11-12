using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Framework.Common.DataAnnotations.Files
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly long _MaxSize;
        private readonly long _MinSize;

        public FileSizeAttribute(long maxSize, long minSize)
        {
            _MaxSize=maxSize;
            _MinSize=minSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage == null)
                ErrorMessage = "FileSizeMsg";

            if (value is null)
                return ValidationResult.Success;

            if (value is not IFormFile)
                throw new ArgumentException("FileSize: value must be IFormFile");

            long _FileSize = ((IFormFile)value).Length;

            if (_FileSize < _MinSize || _FileSize > _MaxSize)
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
                ErrorMessage = ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

            if (ErrorMessage.Contains("{1}"))
                ErrorMessage = ErrorMessage.Replace("{1}", _MinSize.GetFileSizeTitle());

            if (ErrorMessage.Contains("{2}"))
                ErrorMessage = ErrorMessage.Replace("{2}", _MaxSize.GetFileSizeTitle());

            return ErrorMessage;
        }
    }
}
