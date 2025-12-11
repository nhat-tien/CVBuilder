using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVBuilder.Helper
{
    public static class HtmlExtensions
    {
        public static IHtmlContent JS(this IHtmlHelper html, string url)
        {
            return new HtmlString($"<script src=\"@Url.Content(\"~/js/{url}\")\"></script>");
        }
    }
}
