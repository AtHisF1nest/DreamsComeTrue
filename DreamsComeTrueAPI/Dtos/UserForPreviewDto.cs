namespace DreamsComeTrueAPI.Dtos
{
    public class UserForPreviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsInvited { get; set; }
        public bool InvitedYou { get; set; }
    }
}