namespace DreamsComeTrueAPI.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public int Cost { get; set; }
        public virtual User Author { get; set; }
    }
}