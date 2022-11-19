using System;

namespace LearningPortal.Framework.Common.Utilities.Paging
{
    public static class PagingData
    {
        public static OutPagingData Calc(long CountAllItem, int Page, int Take)
        {
            try
            {
                int _Skip = 0;
                int _CountPages = 5;

                Page = Page <= 0 ? 1 : Page;
                if (CountAllItem == 0)
                    return new OutPagingData
                    {
                        CountAllItems = 0,
                        CountAllPage = 1,
                        Take=1,
                        Page = 1,
                        Skip = 0,
                        StartPage = 1,
                        EndPage = 1
                    };

                if (CountAllItem < Take)
                    Take = (int)CountAllItem;

                int _CountAllPage = (int)Math.Ceiling((decimal)CountAllItem / Take);

                if (Page > _CountAllPage)
                    Page = _CountAllPage;

                _Skip = (Take * Page) - Take;
                if (_Skip < 0)
                    _Skip = 0;

                int _StartPage = Page - _CountPages<=0 ? Page : Page - _CountPages;
                int _EndPage = Page + _CountPages > _CountAllPage ? _CountAllPage : Page + _CountPages;

                return new OutPagingData
                {
                    CountAllItems = CountAllItem,
                    CountAllPage = _CountAllPage,
                    Take=Take,
                    Page = Page,
                    Skip = _Skip,
                    StartPage = _StartPage,
                    EndPage = _EndPage
                };
            }
            catch
            {
                return new OutPagingData
                {
                    CountAllItems = 0,
                    CountAllPage = 1,
                    Take=1,
                    Page = 1,
                    Skip = 0,
                    StartPage = 1,
                    EndPage = 1
                };
            }
        }
    }
}
