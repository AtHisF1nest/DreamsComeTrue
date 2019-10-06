using Microsoft.AspNetCore.Http;

namespace DreamsComeTrueAPI.Dtos
{
    public class UserForEditDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public IFormFile Avatar { get; set; }
    }
}