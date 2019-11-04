namespace DreamsComeTrueAPI.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public int TodoItemId { get; set; }
        public TodoItemDto TodoItem { get; set; }
        public string PlannedFor { get; set; }
    }
}