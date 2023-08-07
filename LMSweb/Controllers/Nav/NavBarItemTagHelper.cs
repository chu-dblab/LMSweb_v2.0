using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Web.Mvc;

namespace LMSweb.Controllers.Nav
{
    [HtmlTargetElement("nav-item")]
    public class NavItemTagHelper : TagHelper
    {
        public string Url { get; set; }
        public string Display { get; set; }
        public bool IsActive { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.Attributes.Add("class", "nav-item");

            var linkTag = new TagBuilder("a");
            linkTag.Attributes.Add("class", IsActive ? "text-white nav-link active" : "text-white nav-link");
            linkTag.Attributes.Add("aria-current", "page");
            linkTag.Attributes.Add("href", Url);
            linkTag.SetInnerText(Display);

            output.Content.AppendHtml(linkTag.InnerHtml);
        }
    }
}
