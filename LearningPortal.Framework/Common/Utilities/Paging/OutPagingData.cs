namespace LearningPortal.Framework.Common.Utilities.Paging
{
    public class OutPagingData
    {
        public long CountAllItems { get; set; }
        public int CountAllPage { get; set; }
        public int Page { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
