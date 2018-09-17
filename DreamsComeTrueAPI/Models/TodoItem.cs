using System;
using System.Collections.Generic;
using DreamsComeTrueAPI.Models.Enums;

namespace DreamsComeTrueAPI.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public string Cost { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastDone { get; set; }
        public CategoryType CategoryType { get; set; } = CategoryType.NaDzis;
        public virtual User Author { get; set; }
        public virtual UsersPair UsersPair { get; set; }
        public int UsersPairId { get; set; }
    }
}