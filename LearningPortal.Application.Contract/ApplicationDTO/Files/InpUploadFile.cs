using LearningPortal.Framework.Common.DataAnnotations.Files;
using LearningPortal.Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Files
{
    public class InpUploadFile
    {
        [Display(Name = "FileServerId")]
        [RequiredString]
        [GUID]
        public string FileServerId { get; set; }

        [Display(Name = "UploaderUserId")]
        [GUID]
        public string UploaderUserId { get; set; }

        [Display(Name = "FilePathId")]
        [RequiredString]
        [GUID]
        public string FilePathId { get; set; }

        [Display(Name = "FileName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string FileName { get; set; }

        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "File")]
        [RequiredFile]
        [FileSize(536870912,10)]
        [FileType("image/png,image/jpg,image/jpeg,application/rar,application/zip,video/mp4,video/mp3")]
        public IFormFile Files { get; set; }
    }
}
