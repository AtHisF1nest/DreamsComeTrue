namespace DreamsComeTrueAPI.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        public UserForPreviewDto Author { get; set; }
        public string CategoryType { get; set; }
        public int CategoryTypeId { get; set; }
        public int CountOfItems { get; set; }
        public bool Active { get; set; }    
    }
}