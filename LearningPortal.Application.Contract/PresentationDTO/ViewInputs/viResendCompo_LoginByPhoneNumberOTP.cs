using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viResendCompo_LoginByPhoneNumberOTP
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
