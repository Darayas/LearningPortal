using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class InpLoginByDisposableLinkStep2
    {
        [Display(Name = "Token")]
        [RequiredString]
        public string Token { get; set; }
    }
}
