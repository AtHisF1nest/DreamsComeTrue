using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamsComeTrueAPI.Models
{
    public class CategoryTodoItemBinding
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Category Category { get; set; }
        public virtual TodoItem TodoItem { get; set; }
    }
}