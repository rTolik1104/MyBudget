using System.ComponentModel.DataAnnotations;

namespace MyBudget.ViewModels.Income
{
    public class EditIncomeVM
    {
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public Guid? Category { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public string? Comment { get; set; }
        public Guid IncomeId { get; set; }
    }
}
