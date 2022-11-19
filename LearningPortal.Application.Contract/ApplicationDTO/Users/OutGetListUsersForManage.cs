using LearningPortal.Framework.Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class OutGetListUsersForManage
    {
        public OutPagingData Paging { get; set; }
        public List<OutGetListUsersForManageItems> Items { get; set; }
    }

    public class OutGetListUsersForManageItems
    {
        public string Id { get; set; }
        public string ProfileImgUrl { get; set; }
        public string FullName { get; set; }
        public string AccessLevelTitle { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
    }
}
