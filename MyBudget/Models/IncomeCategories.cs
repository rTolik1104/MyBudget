using System.ComponentModel.DataAnnotations;

namespace MyBudget.Models
{
    public class IncomeCategories
    {
        [Key]
        public Guid categoryId { get; set; }
        public string? category_name { get; set; }
    }
}
