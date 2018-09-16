using System;

namespace DreamsComeTrueAPI.Models
{
    public class History
    {
        public int Id { get; set; }
        public TodoItem TodoItem { get; set; }
        public DateTime Done { get; set; }
    }
}