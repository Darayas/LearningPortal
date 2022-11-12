using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.FilePath
{
    public class InpGetDirectoryPathByPathId
    {
        [Display(Name = "FilePathId")]
        [RequiredString]
        [GUID]
        public string FilePathId { get; set; }
    }
}
