using DreamsComeTrueAPI.Models.Enums;

namespace DreamsComeTrueAPI.Models
{
    public class UsersPair
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual User User2 { get; set; }
        public RelationshipType RelationshipType { get; set; }
    }
}