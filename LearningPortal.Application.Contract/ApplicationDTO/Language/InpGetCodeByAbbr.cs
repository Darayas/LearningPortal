using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Language
{
    public class InpGetCodeByAbbr
    {
        [Display(Name = "Abbreviation")]
        [Required]
        [MaxLength(10)]
        public string Abbreviation { get; set; }
    }
}
