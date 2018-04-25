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
            if (Info.PreviousPage.Display)
                AddPreviousPage(output);

            AddPageNodes(output);

            if (Info.NextPage.Display)
                AddNextPage(output);
        }

        private static void BuildParent(TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.Add("class", "pagination");
            output.Attributes.Add("role", "navigation");
            output.Attributes.Add("aria-label", "Pagination");
        }
        
        private void AddPreviousPage(TagHelperOutput output)
        {
            var html =
$@"<li class=""pagination-previous"">
    <a href=""{Route}/Page{Info.PreviousPage.PageNumber}"" aria-label=""{PreviousPageText} page"">{PreviousPageText} <span class=""show-for-sr"">page</span></a>
</li>";

            output.Content.SetHtmlContent(output.Content.GetContent() + html);
        }
        
        private void AddNextPage(TagHelperOutput output)
        {
            var html =
$@"<li class=""pagination-next"">
    <a href=""{Route}/Page{Info.NextPage.PageNumber}"" aria-label=""{NextPageText} page"">{NextPageText} <span class=""show-for-sr"">page</span></a>
</li>";

            output.Content.SetHtmlContent(output.Content.GetContent() + html);
        }

        private void AddPageNodes(TagHelperOutput output)
        {
            foreach (var infoPage in Info.Pages)
            {
                string html;
                if (infoPage.IsCurrent)
                {
                    html = $@"<li class=""current""><span class=""show-for-sr"">You're on page</span> {infoPage.PageNumber}</li>";
                    output.Content.SetHtmlContent(output.Content.GetContent() + html);
                    continue;
                }
                html = $@"<li><a href=""{Route}/Page{infoPage.PageNumber}"" aria-label=""Page {infoPage.PageNumber}"">{infoPage.PageNumber}</a></li>";
                output.Content.SetHtmlContent(output.Content.GetContent() + html);
            }
        }
    }
}

