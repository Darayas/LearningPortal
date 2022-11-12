using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viCompo_LoginByEmailPassword
    {
        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
