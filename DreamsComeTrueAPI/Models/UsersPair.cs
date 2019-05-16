using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamsComeTrueAPI.Models.Enums;

namespace DreamsComeTrueAPI.Models
{
    public class UsersPair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual User User2 { get; set; }
        public RelationshipType RelationshipType { get; set; }
    }
}