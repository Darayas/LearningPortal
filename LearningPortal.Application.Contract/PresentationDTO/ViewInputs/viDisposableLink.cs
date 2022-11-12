using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viDisposableLink
    {
        [Display(Name = "Token")]
        [RequiredString]
        public string Token { get; set; }
    }
}
