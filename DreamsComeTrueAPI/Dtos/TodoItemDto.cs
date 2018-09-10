namespace DreamsComeTrueAPI.Dtos
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public int Cost { get; set; }
        public UserForPreviewDto Author { get; set; }
    }
}