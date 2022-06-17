using System.ComponentModel.DataAnnotations;

namespace MyBudget.ViewModels.Expense
{
    public class EditExpenseVM
    {
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public Guid? Category { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public string? Comment { get; set; }
        public Guid ExpenseId { get; set; }
    }
}
