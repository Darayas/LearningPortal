using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Application.Contract.ApplicationDTO.Files
{
    public class InpAddFileInfo
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "FileServerId")]
        [RequiredString]
        [GUID]
        public string FileServerId { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "PathId")]
        [RequiredString]
        [GUID]
        public string PathId { get; set; }

        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "FileName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string FileName { get; set; }

        [Display(Name = "SizeOnDisk")]
        public long SizeOnDisk { get; set; }

        [Display(Name = "MimeType")]
        [RequiredString]
        [MaxLengthString(50)]
        public string MimeType { get; set; }

        [Display(Name = "FileMetaData")]
        [RequiredString]
        [MaxLengthString(600)]
        public string FileMetaData { get; set; }
    }
}
