namespace DreamsComeTrueAPI.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        public UserForPreviewDto Author { get; set; }
    }
}