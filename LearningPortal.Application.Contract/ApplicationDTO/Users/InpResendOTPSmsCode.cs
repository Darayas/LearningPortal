using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class InpResendOTPSmsCode
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
