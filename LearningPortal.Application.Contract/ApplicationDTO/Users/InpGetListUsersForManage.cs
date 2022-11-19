using LearningPortal.Framework.Common.DataAnnotations.Integer;

namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class InpGetListUsersForManage
    {
        [RangeNum(1, 100)]
        public int Take { get; set; }

        [RangeNum(1, int.MaxValue)]
        public int Page { get; set; }
    }
}
