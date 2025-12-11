namespace CVBuilder.Areas.User.ViewModels.CV
{
    public class NewCVRequest
    {
        public int Id { get; set; } = 0;
        public int TemplateId { get; set; }
        public string FileName { get; set; } = "MyCV.pdf";
        public string Title { get; set; } = "";
        public string ThemeColor { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string Sections { get; set; } = "";
    }
}
