using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.FilePath
{
    public class InpGetProfileImagePathId
    {
        [Display(Name = "FileServerId")]
        [RequiredString]
        [GUID]
        public string FileServerId { get; set; }
    }
}
