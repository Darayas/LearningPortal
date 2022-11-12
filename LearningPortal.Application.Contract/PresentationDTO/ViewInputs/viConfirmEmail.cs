using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viConfirmEmail
    {
        [Display(Name = "Token")]
        [RequiredString]
        [MaxLengthString(500)]
        public string Token { get; set; }
    }
}
