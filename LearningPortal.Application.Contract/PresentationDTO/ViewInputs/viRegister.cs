using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viRegister
    {
        [Display(Name = "FullName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(150)]
        [Email]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Password { get; set; }

        [Display(Name = "ReType Password")]
        [RequiredString]
        [MaxLengthString(100)]
        public string ReTypePassword { get; set; }

        [Display(Name = "I Accept Site Rules")]
        public bool AcceptRules { get; set; }
    }
}
