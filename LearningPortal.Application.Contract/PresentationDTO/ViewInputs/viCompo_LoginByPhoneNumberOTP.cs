using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viCompo_LoginByPhoneNumberOTP
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [PhoneNumber]
        public string PhoneNumber { get; set; }

        [Display(Name = "OTPCode")]
        [RequiredString]
        [MaxLengthString(5)]
        public string OTPCode { get; set; }
    }
}
