using LearningPortal.Framework.Common.DataAnnotations.Files;
using LearningPortal.Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewInputs
{
    public class viEditProfile
    {
        [Display(Name = "FullName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [RequiredString]
        [Email]
        public string Email { get; set; }

        //[Display(Name = "PhoneNumber")]
        //[RequiredString]
        //[PhoneNumber]
        //public string PhoneNumber { get; set; }

        [Display(Name = "Bio")]
        [RequiredString]
        [MaxLengthString(500)]
        public string Bio { get; set; }

        [Display(Name = "ProfileImage")]
        [FileSize(512000, 10240)]
        [FileType("image/jpg,image/png")]
        public IFormFile Image { get; set; }

        public string ImgUrl { get; set; }
    }
}
