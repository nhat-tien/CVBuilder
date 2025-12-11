using Aspose.Html.Converters;
using CVBuilder.Models;

namespace CVBuilder.Areas.Admin.Dto
{
    public class TemplateCreate
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string PreviewImageUrl { get; set; } = "https://placehold.co/300x400?text=template";
        public string PreviewImageBase64 { get; set; } = "";

        public string HtmlContent { get; set; } = "";

        public string? UserId { get; set; }
        public Models.User? User { get; set; }


        public Models.Template ToTemplate()
        {
            return new Template()
            {
                Id = this.Id,
                Name = this.Name,
                HtmlContent = this.HtmlContent,
                PreviewImageUrl = this.PreviewImageUrl,
                UserId = this.UserId
            };
        }

        public static TemplateCreate FromTemplate(Template template)
        {
            return new TemplateCreate()
            {
                Id = template.Id,
                Name = template.Name,
                HtmlContent = template.HtmlContent,
                PreviewImageUrl = template.PreviewImageUrl,
                UserId = template.UserId,
                User = template.User
            };
        }
    }
}
