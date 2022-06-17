using System.ComponentModel.DataAnnotations;

namespace MyBudget.Models
{
    public class ExpenseCategories
    {
        [Key]
        public Guid categoryId { get; set; }
        public string? category_name { get; set; }
    }
}
