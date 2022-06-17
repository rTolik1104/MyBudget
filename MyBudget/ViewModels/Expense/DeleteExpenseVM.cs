namespace MyBudget.ViewModels.Expense
{
    public class DeleteExpenseVM
    {
        public string? Category { get; set; }
        public decimal? Amount { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public Guid ExpenseId { get; set; }
    }
}
