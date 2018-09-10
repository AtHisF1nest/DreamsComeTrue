using System.Collections.Generic;

namespace DreamsComeTrueAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryType CategoryType { get; set; }
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        public User Author { get; set; }
    }
}