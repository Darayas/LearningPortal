using LearningPortal.Application.Contract.ApplicationDTO.AccessLevel;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using LearningPortal.Domain.Users.AccessLevelAgg.Contracts;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.AccessLevels
{
    public class AccessLevelsApplication : IAccessLevelsApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAccessLevelsRepository _AccessLevelsRepository;
        public AccessLevelsApplication(IAccessLevelsRepository accessLevelsRepository, IServiceProvider serviceProvider)
        {
            _AccessLevelsRepository = accessLevelsRepository;
            _ServiceProvider=serviceProvider;
        }

        public async Task<OperationResult<string>> GetIdByNameAsync(InpGetIdByName Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _AccessLevelsRepository.Get.Where(a => a.Name==Input.Name)
                                                             .Select(a => a.Id.ToString())
                                                             .SingleOrDefaultAsync();

                if (qData is null)
                    return new OperationResult<string>().Failed("Name NotFound");

                return new OperationResult<string>().Succeeded(qData);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }
    }
}
