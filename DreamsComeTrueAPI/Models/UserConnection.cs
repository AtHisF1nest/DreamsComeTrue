using DreamsComeTrueAPI.Models.Enums;

namespace DreamsComeTrueAPI.Models
{
    public class UserConnection
    {
        public int Id { get; set; }
        public User UserA { get; set; }
        public User UserB { get; set; }
        public RelationshipType RelationshipType { get; set; }
    }
}