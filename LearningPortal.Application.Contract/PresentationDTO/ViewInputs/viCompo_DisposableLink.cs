using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viCompo_DisposableLink
    {
        [Display(Name = "Email")]
        [RequiredString]
        [Email]
        public string Email { get; set; }
    }
}
