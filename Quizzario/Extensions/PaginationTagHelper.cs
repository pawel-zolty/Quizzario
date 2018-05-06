using Microsoft.AspNetCore.Razor.TagHelpers;
using Quizzario.Models;

namespace Quizzario.Extensions
{
    public class PaginationTagHelper : TagHelper
    {
        public PagingInfo Info { get; set; }
        public string PreviousPageText { get; set; } = "Previous";
        public string NextPageText { get; set; } = "Next";        
        public string Route { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            BuildParent(output);
            AddPreviousPage(output, Info.PreviousPage.Display);

            AddPageNodes(output);

            AddNextPage(output, Info.NextPage.Display);
        }

        private static void BuildParent(TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.Add("class", "pagination justify-content-center");
            output.Attributes.Add("aria-label", "Pagination");
        }
        
        private void AddPreviousPage(TagHelperOutput output, bool enabled)
        {
            var html = $@"<li class=""page-item" + (enabled?"":" disabled") + $@""">
                    <a class=""page-link" + (enabled?"":" bg-light text-secondary") + $@""" href=""{Route}/{Info.PreviousPage.PageNumber}"" aria-label=""{PreviousPageText} page"">{PreviousPageText}</a>
                </li>";

            output.Content.SetHtmlContent(output.Content.GetContent() + html);
        }
        
        private void AddNextPage(TagHelperOutput output, bool enabled)
        {
            var html = $@"<li class=""page-item" + (enabled ? "" : " disabled") + $@""">
                    <a class=""page-link" + (enabled ? "" : " bg-light text-secondary") + $@""" href=""{Route}/{Info.NextPage.PageNumber}"" aria-label=""{NextPageText} page"">{NextPageText}</a>
                </li>";

            output.Content.SetHtmlContent(output.Content.GetContent() + html);
        }

        private void AddPageNodes(TagHelperOutput output)
        {
            foreach (var infoPage in Info.Pages)
            {
                string html = $@"<li class=""page-item" + (infoPage.IsCurrent?" active":"") + $@""">
                    <a class=""page-link"" href=""{Route}/{infoPage.PageNumber}"" aria-label=""Page {infoPage.PageNumber}"">{infoPage.PageNumber}</a>
                </li>";
                output.Content.SetHtmlContent(output.Content.GetContent() + html);
            }
        }
    }
}

