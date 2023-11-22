using System.ComponentModel.DataAnnotations;

namespace DB_Task.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Данное поле не должно быть пустым")]
        [RegularExpression(@"^([А-ЯЁ]{1}[яа-яё\-]{1,50})$", ErrorMessage = "Текст должен содержать только латинские буквы и дефис")]
        public string? Tittle { get; set; }
        public List<TovarsModel> Tovars { get; set; } = new();
    }
}
