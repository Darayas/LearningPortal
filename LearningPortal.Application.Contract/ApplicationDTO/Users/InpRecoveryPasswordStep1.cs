using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class InpRecoveryPasswordStep1
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Email")]
        [RequiredString]
        [Email]
        public string Email { get; set; }

        [Display(Name = "RecoveryPageUrl")]
        [RequiredString]
        [MaxLengthString(500)]
        public string RecoveryPageUrl { get; set; }
    }
}
