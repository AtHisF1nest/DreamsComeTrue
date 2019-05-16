using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamsComeTrueAPI.Models.Enums;

namespace DreamsComeTrueAPI.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual CategoryType CategoryType { get; set; } = CategoryType.NaDzis;
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        public virtual User Author { get; set; }
        public virtual UsersPair UsersPair { get; set; }
        public int UsersPairId { get; set; }
    }
}