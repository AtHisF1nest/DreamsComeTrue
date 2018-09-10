namespace DreamsComeTrueAPI.Models
{
    public class CategoryTodoItemBinding
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public TodoItem TodoItem { get; set; }
    }
}