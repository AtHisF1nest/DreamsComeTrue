using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamsComeTrueAPI.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey(nameof(TodoItem))]
        public int TodoItemId { get; set; }
        public virtual TodoItem TodoItem { get; set; }
        public DateTime PlannedFor { get; set; }
    }
}