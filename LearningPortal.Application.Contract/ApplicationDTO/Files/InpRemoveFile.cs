using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Files
{
    public class InpRemoveFile
    {
        [Display(Name = "FileId")]
        [RequiredString]
        [GUID]
        public string FileId { get; set; }
    }
}
