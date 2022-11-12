using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class InpLoginByDisposableLinkStep1
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Email")]
        [RequiredString]
        [Email]
        public string Email { get; set; }

        [Display(Name = "EmailLinkTemplate")]
        [RequiredString]
        [MaxLengthString(500)]
        public string EmailLinkTemplate { get; set; }
    }
}
