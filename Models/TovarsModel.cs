using System.ComponentModel.DataAnnotations;

namespace DB_Task.Models
{
    public class TovarsModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Данное поле не должно быть пустым")]
        [RegularExpression(@"^([а-яА-ЯёЁa-zA-Z0-9\s\.\,\-]{1,50})$", ErrorMessage = "Пример: Lenovo Legion Y530")]
        public string Tittle { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        public int? CategoryId { get; set; }

        public CategoryModel? Category { get; set; }
    }
}
