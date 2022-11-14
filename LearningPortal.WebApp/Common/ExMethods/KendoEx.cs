using Kendo.Mvc.UI.Fluent;
using LearningPortal.Framework.Contracts;

namespace LearningPortal.WebApp.Common.ExMethods
{
    public static class KendoEx
    {
        public static GridBuilder<T> DefaultSettings<T>(this GridBuilder<T> builder, ILocalizer Localizer) where T : class
        {
            return builder.Pageable(a =>
            {
                a.Messages(msg =>
                {
                    msg.Display(Localizer["PageableMsg"]);
                    msg.Empty(Localizer["GridIsEmpty"]);
                    msg.ItemsPerPage(Localizer["ItemsPerPage"]);
                    msg.Of(Localizer["KendoOf"]);
                    msg.MorePages(Localizer["MorePages"]);
                    msg.Refresh(Localizer["Refresh"]);
                    msg.Previous(Localizer["Previous"]);
                    msg.Next(Localizer["Next"]);
                    msg.Last(Localizer["Last"]);
                    msg.First(Localizer["First"]);
                    msg.Page(Localizer["Page"]);
                });
                a.AlwaysVisible(true);
                a.ButtonCount(5);
                a.Input(false);
                a.PreviousNext(true);
                a.Responsive(true);
            });
        }
    }
}
