using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace LearningPortal.Framework.Common.ExMethod
{
    public static class ObjectEx
    {
        private static string GetName(PropertyInfo Input)
        {
            object _attr = Input.GetCustomAttributes(true).Where(a => a.GetType().Name == "DisplayAttribute").FirstOrDefault();

            if (_attr == null)
                return "";

            return (string)_attr.GetType().GetProperty("Name").GetValue(_attr);
        }
        private static List<ValidationResult> Check<T>(T Input, IServiceProvider serviceProvider, string sectionName = "")
        {
            if (Input == null)
                throw new ArgumentInvalidException($"{nameof(Input)} cannot be null");

            var _localizer = serviceProvider.GetService<ILocalizer>();

            var _validationResult = new List<ValidationResult>();

            #region Check Lists Property
            {
                foreach (var item in Input.GetType().GetProperties())
                {
                    if (item.PropertyType.GetInterfaces().Contains(typeof(IList)))
                    {
                        var lstVals = (IEnumerable)item.GetValue(Input);

                        if (lstVals != null)
                            foreach (var itemLst in lstVals)
                                _validationResult.AddRange(Check(itemLst, serviceProvider, _localizer[GetName(item)]));
                    }
                }
            }
            #endregion

            #region Check Model State Validation
            {
                var _validationContext = new ValidationContext(Input);
                _validationContext.InitializeServiceProvider(t => serviceProvider);

                Validator.TryValidateObject(Input, _validationContext, _validationResult, true);
            }
            #endregion

            return _validationResult.Select(a => new ValidationResult((sectionName=="" ? "" : sectionName + ": ") + a.ErrorMessage, a.MemberNames)).ToList();
        }
        public static void CheckModelState<T>(this T Input, IServiceProvider serviceProvider) where T : class
        {
            var errors = Check(Input, serviceProvider);

            if (errors != null)
                if (errors.Count > 0)
                    throw new ArgumentInvalidException(string.Join(" , ", errors.Select(a => a.ErrorMessage)));
        }
    }
}