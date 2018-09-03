using System.ComponentModel.DataAnnotations;

namespace DreamsComeTrueAPI.Dtos
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "Login jest wymagany.")]
        [StringLength(24, MinimumLength = 4, ErrorMessage = "Login musi zawierać od 4 do 24 znaków.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(24, MinimumLength = 4, ErrorMessage = "Hasło musi zawierać od 4 do 24 znaków.")]
        public string Password { get; set; }
    }
}