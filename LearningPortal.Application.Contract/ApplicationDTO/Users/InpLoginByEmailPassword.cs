using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class InpLoginByEmailPassword
    {
        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Password { get; set; }
    }
}
