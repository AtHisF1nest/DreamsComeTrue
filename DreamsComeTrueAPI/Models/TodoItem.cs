using System;
using System.Collections.Generic;
using DreamsComeTrueAPI.Models.Enums;

namespace DreamsComeTrueAPI.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public int Cost { get; set; }
        public DateTime Created { get; set; }
        public TodoItemStatus Status { get; set; }
        public virtual User Author { get; set; }
        public virtual UsersPair UsersPair { get; set; }
        public int UsersPairId { get; set; }
    }
}