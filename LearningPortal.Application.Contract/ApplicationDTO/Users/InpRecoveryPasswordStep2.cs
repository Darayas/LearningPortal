using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class InpRecoveryPasswordStep2
    {
        [Display(Name = "Token")]
        [RequiredString]
        [MaxLengthString(1000)]
        public string Token { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        public string Password { get; set; }
    }
}
