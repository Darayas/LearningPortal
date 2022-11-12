using LearningPortal.Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LearningPortal.Application.Contract.ApplicationDTO.FileServer
{
    public class InpGetServerDetails
    {
        [Display(Name = "ServerId")]
        [RequiredString]
        [GUID]
        public string ServerId { get; set; }
    }
}
