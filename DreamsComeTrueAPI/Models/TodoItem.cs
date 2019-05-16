using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamsComeTrueAPI.Models.Enums;

namespace DreamsComeTrueAPI.Models
{
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Objective { get; set; }
        public string Cost { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastDone { get; set; }
        public CategoryType CategoryType { get; set; } = CategoryType.NaDzis;
        public bool IsOneTime { get; set; }
        public virtual User Author { get; set; }
        public virtual UsersPair UsersPair { get; set; }
        public int UsersPairId { get; set; }
    }
}