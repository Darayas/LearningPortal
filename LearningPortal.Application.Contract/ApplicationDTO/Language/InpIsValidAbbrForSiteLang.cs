using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Language
{
    public class InpIsValidAbbrForSiteLang
    {
        [Display(Name = "Abbr")]
        [Required]
        [MaxLength(5)]
        public string Abbr { get; set; }
    }
}
