namespace MyBudget.ViewModels.Expense
{
    public class ExpenseVM
    {
        public string? Category { get; set; }
        public bool? Status { get; set; }
        public decimal? Amout { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public Guid ExpenseId { get; set; }
    }
}
