using LearningPortal.Framework.Common.DataAnnotations.Files;
using LearningPortal.Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Files
{
    public class InpUploadProfileImage
    {
        [Display(Name = "FileServerId")]
        [RequiredString]
        [GUID]
        public string FileServerId { get; set; }

        [Display(Name = "UploaderUserId")]
        [RequiredString]
        [GUID]
        public string UploaderUserId { get; set; }

        [Display(Name = "FilePathId")]
        [RequiredString]
        [GUID]
        public string FilePathId { get; set; }

        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "FormFile")]
        [RequiredFile(ErrorMessage = "RequiredMsg")]
        [FileSize(104857600,10, ErrorMessage = "FileSizeMsg")]
        [FileType("image/jpg,image/png,image/jpeg")]
        public IFormFile FormFile { get; set; }
    }
}
