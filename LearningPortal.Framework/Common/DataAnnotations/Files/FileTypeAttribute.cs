using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Services.AntiShell;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LearningPortal.Framework.Common.DataAnnotations.Files
{
    public class FileTypeAttribute : ValidationAttribute
    {
        public string ContentTypes { get; set; }
        public FileTypeAttribute(string contentType)
        {
            ContentTypes=contentType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var _ServiceProvider = validationContext.GetService<IServiceProvider>();
            var _AntiShell = _ServiceProvider.GetService<IAntiShell>();

            if (value is List<IFormFile> || value is IFormFile[])
            {
                var _ArrayFormFile = (IFormFile[])value;
                if (_ArrayFormFile.Count() == 0)
                    return ValidationResult.Success;

                string _Message = "";
                foreach (var item in _ArrayFormFile)
                {
                    if (item != null)
                    {
                        if (!_AntiShell.ValidateFileAsync(item).Result)
                            _Message += Environment.NewLine + GetMessage(validationContext, item.FileName);
                        else
                        {
                            if (!ContentTypes.Contains(item.ContentType))
                            {
                                _Message += Environment.NewLine + GetMessage(validationContext, item.FileName);
                            }
                        }
                    }
                }

                return new ValidationResult(_Message);
            }
            else if (value is IFormFile)
            {
                string _ContentType = ((IFormFile)value).ContentType;
                var _FormFile = (IFormFile)value;

                if (!_AntiShell.ValidateFileAsync(_FormFile).Result)
                    return new ValidationResult(GetMessage(validationContext, _FormFile.FileName));
                else
                {
                    if (!ContentTypes.Contains(_FormFile.ContentType))
                    {
                        return new ValidationResult(GetMessage(validationContext, _FormFile.FileName));
                    }
                    else
                        return ValidationResult.Success;
                }
            }
            else
            {
                throw new ArgumentException("FileTypeMsg: value must be IFormFile or array");
            }
        }

        private string GetMessage(ValidationContext validationContext, string FileName)
        {
            if (ErrorMessage == null)
                ErrorMessage = "FileTypeMsg";

            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();

            ErrorMessage = _Localizer[ErrorMessage];

            if (ErrorMessage.Contains("{0}"))
                ErrorMessage = ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

            if (ErrorMessage.Contains("{1}"))
                ErrorMessage = ErrorMessage.Replace("{1}", FileName);

            if (ErrorMessage.Contains("{2}"))
                ErrorMessage = ErrorMessage.Replace("{2}", ContentTypes);

            return ErrorMessage;
        }
    }
}
