using LearningPortal.Framework.Common.DataAnnotations.Integer;
using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class InpGetListUsersForManage
    {
        [RangeNum(1, 100)]
        public int Take { get; set; }

        [RangeNum(1, int.MaxValue)]
        public int Page { get; set; }

        [Display(Name = "Email")]
        [MaxLengthString(100)]
        public string Email { get; set; }

        [Display(Name = "FullName")]
        [MaxLengthString(100)]
        public string FullName { get; set; }

        [Display(Name = "PhoneNumber")]
        [MaxLengthString(100)]
        public string PhoneNumber { get; set; }

        [Display(Name = "FieldSort")]
        public InpGetListUsersForManageSortingEnum Sort { get; set; }
    }

    public enum InpGetListUsersForManageSortingEnum
    {
        Date_Des = 0,
        Date_Aes = 1,
        Status_Des = 2,
        Status_Aes = 3
    }
}
