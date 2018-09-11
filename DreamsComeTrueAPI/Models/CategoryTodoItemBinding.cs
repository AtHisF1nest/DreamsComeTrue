namespace DreamsComeTrueAPI.Models
{
    public class CategoryTodoItemBinding
    {
        public int Id { get; set; }
        public virtual Category Category { get; set; }
        public virtual TodoItem TodoItem { get; set; }
    }
}