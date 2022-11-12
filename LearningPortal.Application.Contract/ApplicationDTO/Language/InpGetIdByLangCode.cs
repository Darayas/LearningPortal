using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Language
{
    public class InpGetIdByLangCode
    {
        [Display(Name = "LangCode")]
        [RequiredString]
        [MaxLengthString(100)]
        public string LangCode { get; set; }
    }
}
