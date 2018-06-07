using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Quizzario.Extensions
{
    public class SearchingPaginationTagHelper : PaginationTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            BuildParent(output);
            AddPreviousPage(output, Info.PreviousPage.Display);

            AddPageNodes(output);

            AddNextPage(output, Info.NextPage.Display);
        }

        protected void AddPreviousPage(TagHelperOutput output, bool enabled)
        {
            var html = $@"<li class=""page-item" + (enabled ? "" : " disabled") + $@""">
                    <a class=""page-link" + (enabled ? "" : " bg-light text-secondary") + $@""" href=""{Route}&p={Info.PreviousPage.PageNumber}"" aria-label=""{PreviousPageText} page"">{PreviousPageText}</a>
                </li>";

            output.Content.SetHtmlContent(output.Content.GetContent() + html);
        }

        protected void AddNextPage(TagHelperOutput output, bool enabled)
        {
            var html = $@"<li class=""page-item" + (enabled ? "" : " disabled") + $@""">
                    <a class=""page-link" + (enabled ? "" : " bg-light text-secondary") + $@""" href=""{Route}&p={Info.NextPage.PageNumber}"" aria-label=""{NextPageText} page"">{NextPageText}</a>
                </li>";

            output.Content.SetHtmlContent(output.Content.GetContent() + html);
        }

        protected void AddPageNodes(TagHelperOutput output)
        {
            foreach (var infoPage in Info.Pages)
            {
                string html = $@"<li class=""page-item" + (infoPage.IsCurrent ? " active" : "") + $@""">
                    <a class=""page-link"" href=""{Route}&p={infoPage.PageNumber}"" aria-label=""Page {infoPage.PageNumber}"">{infoPage.PageNumber}</a>
                </li>";
                output.Content.SetHtmlContent(output.Content.GetContent() + html);
            }
        }
    }
}
