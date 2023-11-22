using DB_Task.Models;

namespace DB_Task.ViewModel
{
    public class TovarsViewModel
    {
        public List<CategoryModel>? CategoryModels { get; set; }
        public List<TovarsModel>? TovarsModels { get; set; }

        public int? CategoryId { get; set; }
    }
}
