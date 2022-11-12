using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viResetPassword
    {
        [Display(Name = "Token")]
        [RequiredString]
        [MaxLengthString(1000)]
        public string Token { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        public string Password { get; set; }

        [Display(Name = "RetypePassword")]
        [RequiredString]
        [Compare(nameof(Password), ErrorMessage = "CompareMsg")]
        public string RetypePassword { get; set; }
    }
}
