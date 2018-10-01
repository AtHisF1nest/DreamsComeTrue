using System.ComponentModel.DataAnnotations;

namespace DreamsComeTrueAPI.Dtos
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole treści jest wymagane.")]
        public string Objective { get; set; }
        public string Cost { get; set; }
        public string CategoryType { get; set; }
        public UserForPreviewDto Author { get; set; }
        public string Created { get; set; }
        public string LastDone { get; set; }
        public bool IsOneTime { get; set; }
    }
}