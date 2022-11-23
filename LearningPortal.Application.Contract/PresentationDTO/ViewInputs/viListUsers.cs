using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viListUsers
    {
        [Display(Name = "Email")]
        [MaxLengthString(100)]
        public string Email { get; set; }

        [Display(Name = "FullName")]
        [MaxLengthString(100)]
        public string FullName { get; set; }

        [Display(Name = "PhoneNumber")]
        [MaxLengthString(100)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Sort")]
        public int FieldSort { get; set; }
    }
}
