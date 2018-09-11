namespace DreamsComeTrueAPI.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}