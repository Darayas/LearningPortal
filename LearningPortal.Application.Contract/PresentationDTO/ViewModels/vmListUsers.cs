using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.PresentationDTO.ViewModels
{
    public class vmListUsers
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "ProfileImgUrl")]
        public string ProfileImgUrl { get; set; }

        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Display(Name = "AccessLevelTitle")]
        public string AccessLevelTitle { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "RegisterDate")]
        public DateTime Date { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "EmailConfirm")]
        public bool EmailConfirm { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "PhoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }
    }
}
