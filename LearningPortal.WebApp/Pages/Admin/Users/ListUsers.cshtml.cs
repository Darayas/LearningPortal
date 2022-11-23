using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LearningPortal.Application.App.User;
using LearningPortal.Application.Contract.ApplicationDTO.Users;
using LearningPortal.Application.Contract.PresentationDTO.ViewInputs;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using LearningPortal.WebApp.Authentication;
using LearningPortal.WebApp.Common.Utility.MessageBox;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.Pages.Admin.Users
{
    [Authorize(Roles = Roles.CanManageUser)]
    public class ListUsersModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;

        public ListUsersModel(ILogger logger, IMsgBox msgBox, ILocalizer localizer, IServiceProvider serviceProvider, IUserApplication userApplication)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _UserApplication=userApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                #region Validation
                {

                }
                #endregion

                var _Result = await _UserApplication.GetListUsersForManageAsync(new InpGetListUsersForManage
                {
                    Take = request.PageSize,
                    Page = request.Page,
                    Email=Input.Email,
                    FullName=Input.FullName,
                    PhoneNumber=Input.PhoneNumber,
                    Sort=(InpGetListUsersForManageSortingEnum)Input.FieldSort
                });
                if (!_Result.IsSucceeded)
                    return StatusCode(400);

                var _DataGrid = _Result.Data.Items.ToDataSourceResult(request);
                _DataGrid.Total = (int)_Result.Data.Paging.CountAllItems;
                _DataGrid.Data = _Result.Data.Items;

                return new JsonResult(_DataGrid);
            }
            catch (ArgumentInvalidException)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viListUsers Input { get; set; }
    }
}
