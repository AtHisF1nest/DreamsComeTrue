using Microsoft.AspNetCore.Http;

namespace DreamsComeTrueAPI.Dtos
{
    public class PhotoForCreationDto
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        public IFormFile Photo { get; set; }
    }
}